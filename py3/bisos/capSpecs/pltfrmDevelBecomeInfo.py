# -*- coding: utf-8 -*-
""" #+begin_org
* ~[Summary]~ :: The single source of facts of the pltfrmDevelBecome capability: the git alias, the repo
roots, the dev venv and the packages known not to install. Pure: no side effects at import.
#+end_org """

import ast
import json
import os
import re
import subprocess
import typing

#
# capSpecs AI-WorkPlan.org, Stage 2 (2.2). The CBM turns a rawBisos platform into a developer platform:
# git from anon to auth, pip from PyPI to editable installs. The ssh Host alias is a precondition (Stage 3,
# pltfrmDevelCreds), only checked here; nothing here writes ~/.ssh/config.
#

aliasOverrideName = "PLTFRM_DEVEL_GIT_ALIAS"
defaultAlias = "mb2_github"

bxReposLink = "/bisos/git/bxRepos"
bxReposLinkOverrideName = "PLTFRM_DEVEL_BXREPOS_LINK"   # for tests only
authBxRepos = "/bisos/git/auth/bxRepos"
anonBxRepos = "/bisos/git/anon/bxRepos"

devVenv = "/bisos/venv/py3/dev-bisos3"
devVenvOverrideName = "PLTFRM_DEVEL_VENV"                # for tests only
packagesRootOverrideName = "PLTFRM_DEVEL_PACKAGES_ROOT"  # for tests only
pypiProcOverrideName = "PLTFRM_DEVEL_PYPIPROC"           # for tests only
rawBisosSbomOverrideName = "PLTFRM_DEVEL_RAWBISOS_SBOM"   # for tests only
rawBisosSbom = "rawBisos-sbom.pcs"
firstPackages = ["namespace", "b"]                       # installed first, in this order

# Do not install editable (capSpecs WorkPlan 2.8): skipped explicitly and reported, never failing the run.
knownNotInstalling: typing.List[str] = ["lcnt", "pycs", "qmailSvc", "gnome"]


def aliasName() -> str:
    """The override (PLTFRM_DEVEL_GIT_ALIAS) if set, else the default alias."""
    return os.environ.get(aliasOverrideName) or defaultAlias


def bxReposLinkPath() -> str:
    return os.environ.get(bxReposLinkOverrideName) or bxReposLink


def bxReposMode() -> str:
    """Where /bisos/git/bxRepos points: auth (devel mode), anon, other:<target>, notALink or absent."""
    link = bxReposLinkPath()
    if not os.path.lexists(link):
        return "absent"
    if not os.path.islink(link):
        return "notALink"
    target = os.readlink(link)
    if target == authBxRepos:
        return "auth"
    if target == anonBxRepos:
        return "anon"
    return f"other:{target}"


def devVenvPath() -> str:
    return os.environ.get(devVenvOverrideName) or devVenv


def devVenvPresent() -> bool:
    return os.path.isfile(os.path.join(devVenvPath(), "bin", "python"))


def packagesRoot() -> str:
    return os.environ.get(packagesRootOverrideName) or os.path.join(authBxRepos, "bisos-pip")


def aliasState(alias: str = "", timeout: int = 15) -> typing.Tuple[str, str]:
    """(state, detail) of the ssh Host alias of the git server. Read-only: it never writes ~/.ssh/config.
    State is one of: ok, notDefined, authFailed, hostKeyUnknown, unreachable, noSsh, other.
    notDefined: ssh -G has nothing for the alias (its hostname is the alias itself)."""
    alias = alias or aliasName()
    try:
        conf = subprocess.run(["ssh", "-G", alias], check=False, capture_output=True, text=True, timeout=timeout)
    except FileNotFoundError:
        return ("noSsh", "ssh is not installed")
    except subprocess.TimeoutExpired:
        return ("other", "ssh -G timed out")
    hostname = ""
    for line in conf.stdout.splitlines():
        if line.lower().startswith("hostname "):
            hostname = line.split(None, 1)[1].strip()
            break
    if not hostname or hostname.lower() == alias.lower():
        return ("notDefined", f"no Host {alias} in the ssh configuration")
    try:
        auth = subprocess.run(
            ["ssh", "-T", "-o", "BatchMode=yes", "-o", f"ConnectTimeout={timeout}", f"git@{alias}"],
            check=False, capture_output=True, text=True, timeout=timeout + 10)
    except subprocess.TimeoutExpired:
        return ("unreachable", f"{hostname}: timed out")
    out = ((auth.stdout or "") + (auth.stderr or "")).strip()
    first = out.splitlines()[0] if out else ""
    if "successfully authenticated" in out or auth.returncode == 0:
        return ("ok", first or hostname)
    if "Permission denied" in out:
        return ("authFailed", first)
    if "Host key verification failed" in out:
        return ("hostKeyUnknown", first)
    if "Could not resolve" in out or "timed out" in out or "Connection" in out or "No route" in out:
        return ("unreachable", first)
    return ("other", first)


def logDir() -> str:
    return os.environ.get("PLTFRM_DEVEL_LOGDIR") or "/tmp/pltfrmDevelBecome"


def pbdVerifyRepos(alias: str = "") -> typing.Tuple[typing.List[str], typing.List[str]]:
    """(paths, missing) of the bxReposRoot registry in auth mode, from bx-gitReposBases -i pbdVerify (offline).
    A path is missing when pbdVerify prints '<path> Missing -- Un-Expected'. Raises ValueError if it lists nothing."""
    alias = alias or aliasName()
    run = subprocess.run(
        ["bx-gitReposBases", "-v", "20", "-i", "pbdVerify", f"--baseDir={authBxRepos}", "--pbdName=bxReposRoot",
         "--vcMode=auth", f"--gitLabel={alias}", "all"],
        check=False, capture_output=True, text=True, cwd="/tmp")
    paths: typing.List[str] = []
    missing: typing.List[str] = []
    for line in (run.stdout + run.stderr).splitlines():
        if not line.startswith(authBxRepos + "/"):
            continue
        path = line.split()[0]
        if "Missing" in line:
            missing.append(path)
        elif path not in paths:
            paths.append(path)
    if not paths and not missing:
        raise ValueError("bx-gitReposBases pbdVerify listed no repos")
    return (paths, missing)


def gitistEnv(alias: str = "") -> typing.Dict[str, str]:
    return dict(os.environ, vcMode="auth", bxReposBaseDir=authBxRepos, gitLabel=alias or aliasName())


def gitistRepos(alias: str = "") -> typing.List[str]:
    """The <org>/<repo> list of bxRepos-gitist.pcs (repos outside bisos.common's registry), in auth mode."""
    run = subprocess.run(["bxRepos-gitist.pcs"], check=False, capture_output=True, text=True,
                         env=gitistEnv(alias), cwd="/tmp")
    repos = re.findall(r'gitist_clone\s+--destBaseDir="[^"]*"\s+(\S+)', run.stdout)
    if not repos:
        raise ValueError("bxRepos-gitist.pcs listed no repos")
    return repos


def originOf(path: str) -> str:
    run = subprocess.run(["git", "-C", path, "remote", "get-url", "origin"], check=False,
                         capture_output=True, text=True)
    return run.stdout.strip()


def reposProblems(alias: str = "") -> typing.Tuple[typing.List[str], int]:
    """(problems, number of repos checked): every repo of the registry and of bxRepos-gitist.pcs must be
    cloned in auth, and writable: origin is git@<alias>:... (not https). Offline."""
    alias = alias or aliasName()
    paths, missing = pbdVerifyRepos(alias)
    problems = [f"missing clone: {each}" for each in missing]
    gitistPaths = [os.path.join(authBxRepos, repo) for repo in gitistRepos(alias)]
    checked = 0
    for path in paths + [each for each in gitistPaths if each not in paths]:
        if not os.path.exists(os.path.join(path, ".git")):
            if path in paths and os.path.isdir(path):
                continue    # a plain directory of the registry (an overview dir, a parent): not a repo
            problems.append(f"missing clone: {path}")
            continue
        checked += 1
        origin = originOf(path)
        if not origin.startswith(f"git@{alias}:"):
            problems.append(f"not writable: {path} origin={origin or 'none'}")
    return (problems, checked)


def bxReposSwitch(mode: str, problems: typing.Optional[typing.List[str]] = None) -> typing.Tuple[bool, str]:
    """Point the bxRepos link at auth or anon, atomically, and verify with readlink. Refuses to switch to
    auth when problems (from reposProblems) is not empty. Idempotent. Never replaces a real directory."""
    if mode not in ("auth", "anon"):
        return (False, f"mode must be auth or anon, not {mode}")
    target = authBxRepos if mode == "auth" else anonBxRepos
    link = bxReposLinkPath()
    if not os.path.isdir(target):
        return (False, f"{target} does not exist")
    if mode == "auth" and problems:
        return (False, f"refusing to switch to auth: {len(problems)} problem(s), first: {problems[0]}")
    if os.path.lexists(link) and not os.path.islink(link):
        return (False, f"{link} is not a link; not replacing it")
    if os.path.islink(link) and os.readlink(link) == target:
        return (True, f"{link} already -> {target}")
    tmp = f"{link}.new"
    try:
        if os.path.lexists(tmp):
            os.unlink(tmp)
        os.symlink(target, tmp)
        os.replace(tmp, link)
    except OSError as err:
        return (False, f"cannot switch {link}: {err}")
    if os.readlink(link) != target:
        return (False, f"{link} does not point to {target} after the switch")
    return (True, f"{link} -> {target}")


def packageDirs() -> typing.List[str]:
    """The packages that must be editable: every <packagesRoot>/*/py3 with a pypiProc.sh. firstPackages first."""
    root = packagesRoot()
    names = sorted(each for each in os.listdir(root) if os.path.isfile(os.path.join(root, each, "py3", "pypiProc.sh")))
    return [each for each in firstPackages if each in names] + [each for each in names if each not in firstPackages]


def packagePath(name: str) -> str:
    return os.path.join(packagesRoot(), name, "py3")


def pypiProcCommand(name: str) -> typing.List[str]:
    return [os.environ.get(pypiProcOverrideName) or os.path.join(packagePath(name), "pypiProc.sh"),
            "-i", "pkgReInstall", "edit", devVenvPath()]


def editableLocations() -> typing.Dict[str, str]:
    """{editable project location: package name} of the dev venv, from pip list -e."""
    run = subprocess.run([os.path.join(devVenvPath(), "bin", "pip"), "list", "-e", "--format=json"],
                         check=False, capture_output=True, text=True)
    if run.returncode != 0:
        raise ValueError(f"pip list -e failed in {devVenvPath()}: {run.stderr.strip()[:200]}")
    return {each.get("editable_project_location", ""): each["name"] for each in json.loads(run.stdout)}


def importBOk() -> bool:
    run = subprocess.run([os.path.join(devVenvPath(), "bin", "python"), "-c", "from bisos import b"],
                         check=False, capture_output=True, text=True)
    return run.returncode == 0


def pipRows() -> typing.List[typing.Tuple[str, str]]:
    """(package dir, state) for each package that must be editable: editable, skipped (known), NOT EDITABLE."""
    locations = editableLocations()
    rows = []
    for name in packageDirs():
        if packagePath(name) in locations:
            rows.append((name, "editable"))
        elif name in knownNotInstalling:
            rows.append((name, "skipped (known not installing)"))
        else:
            rows.append((name, "NOT EDITABLE"))
    return rows


def rawBisosPipEntries() -> typing.List[str]:
    """The pip list of rawBisos-sbom.pcs, as it says it: rawBisos-sbom.pcs -i pipPkgs_list (read-only)."""
    run = subprocess.run([os.environ.get(rawBisosSbomOverrideName) or rawBisosSbom, "-i", "pipPkgs_list"],
                         check=False, capture_output=True, text=True, cwd="/tmp")
    for line in run.stdout.splitlines():
        if line.startswith("["):
            return list(ast.literal_eval(line))
    raise ValueError(f"{rawBisosSbom} -i pipPkgs_list printed no list")


def entryPipName(entry: str) -> str:
    """The pip name of an entry: bisos.x as is; bisos-pip/x (a repo path, a defect in the sbom) is bisos.x."""
    return "bisos." + entry.split("/", 1)[1] if entry.startswith("bisos-pip/") else entry


def entryDir(entry: str) -> typing.Optional[str]:
    """The bisos-pip directory of an entry (matched case-insensitively), or None when there is no clone."""
    base = entryPipName(entry)
    base = base[len("bisos."):] if base.startswith("bisos.") else base
    for name in packageDirs():
        if name.lower() == base.lower():
            return name
    return None


def pipShowOk(pipName: str) -> bool:
    run = subprocess.run([os.path.join(devVenvPath(), "bin", "pip"), "show", pipName],
                         check=False, capture_output=True, text=True)
    return run.returncode == 0


def rawBisosPipRows() -> typing.List[typing.Tuple[str, str, str]]:
    """(entry, state, pip name) for each pip entry of rawBisos-sbom.pcs: editable (from the clone, as 2.5 makes
    it), NOT EDITABLE (has a clone, is not editable), from PyPI (no clone, installed), NOT INSTALLED."""
    locations = editableLocations()
    rows = []
    for entry in rawBisosPipEntries():
        name = entryDir(entry)
        pipName = entryPipName(entry)
        if name:
            rows.append((entry, "editable" if packagePath(name) in locations else "NOT EDITABLE", pipName))
        else:
            rows.append((entry, "from PyPI" if pipShowOk(pipName) else "NOT INSTALLED", pipName))
    return rows
