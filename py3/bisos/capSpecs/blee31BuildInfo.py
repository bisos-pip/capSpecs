# -*- coding: utf-8 -*-
""" #+begin_org
* ~[Summary]~ :: The single source of facts of the blee31Build capability: Blee built for Emacs 31, the doom
pin, the paths of the doom framework and of the run base, and the read-only probes of the result. Pure: no
side effects at import.
#+end_org """

import os
import re
import subprocess
import typing

#
# capSpecs AI-WorkPlan.org, Stage 6 (6.1, decided with the user 2026-11-12; the version, 6.3):
#   - Version-specific, as emacs31SrcBuild: the Emacs major is fixed here and the files are blee31Build-*.
#     The CBM leaf bleeBuild stays generic through the symlink bleeBuild-cbs.pcs -> blee31Build-cbs.pcs.
#     Blee (profile doom-blee3) is built for /usr/local/bin/emacs-31 (emacsSrcBuild), default or not.
#   - /bisos/blee/bin/bleeDoomsManage.sh is used unedited; the doom pin is its (doomPinForEmacsMajor, bleeLib.sh).
#   - Only builds, system-wide. The user's chemacs2 ~/.emacs-profiles.el is not switched here.
#

majorVersion = "31"
profile = "doom-blee3"
buildScript = "bleeDoomsManage.sh"
bleeLib = "/bisos/blee/bin/bleeLib.sh"

buildScriptOverrideName = "BLEE_BUILD_SCRIPT"        # for tests only
binDirOverrideName = "BLEE_BUILD_BINDIR"             # for tests only
bleeBaseOverrideName = "BLEE_BUILD_BLEEBASE"         # for tests only
bleeLibOverrideName = "BLEE_BUILD_BLEELIB"           # for tests only
logDirOverrideName = "BLEE_BUILD_LOGDIR"

binDirDefault = "/usr/local/bin"
bleeBaseDefault = "/bisos/blee"
logDirDefault = "/tmp/blee31Build"

steps: typing.List[str] = ["doomFrameworkPrep", "buildInstall", "reBuild"]

# The config files of doom-base-blee3 that buildInstall rewrites (sans-Blee, then with-Blee); tracked in blee.
configFiles: typing.List[str] = ["config.el", "configSansBlee.el", "configWithBlee.el", "extras.el"]


def scriptPath() -> str:
    return os.environ.get(buildScriptOverrideName) or buildScript


def binDir() -> str:
    return os.environ.get(binDirOverrideName) or binDirDefault


def bleeBase() -> str:
    return os.environ.get(bleeBaseOverrideName) or bleeBaseDefault


def logDir() -> str:
    return os.environ.get(logDirOverrideName) or logDirDefault


def _run(args: typing.List[str], env: typing.Optional[dict] = None, timeout: int = 60) -> str:
    try:
        out = subprocess.run(args, check=False, capture_output=True, text=True, timeout=timeout, env=env, cwd="/tmp")
    except (OSError, subprocess.TimeoutExpired):
        return ""
    return (out.stdout or "") + (out.stderr or "")


def majorEmacsPath() -> str:
    """binDir/emacs-31, which bleeDoomsManage.sh -p emacs=31 uses."""
    return os.path.join(binDir(), f"emacs-{majorVersion}")


def targetEmacs() -> str:
    """Where binDir/emacs-31 leads (followed to the end), "" when absent."""
    path = majorEmacsPath()
    return os.path.realpath(path) if os.path.lexists(path) else ""


def emacsVersion() -> str:
    """The version of Emacs 31, from the binary's name (emacs-31.1); "" when it is not a /usr/local build named
    emacs-31.<minor>, or does not run."""
    target = targetEmacs()
    match = re.fullmatch(r"emacs-(\d+\.\d+(\.\d+)*)", os.path.basename(target))
    if not match or match.group(1).split(".")[0] != majorVersion or not os.path.isfile(target):
        return ""
    lines = _run([target, "--version"]).splitlines()
    return match.group(1) if lines and lines[0].endswith(" " + match.group(1)) else ""


def emacsMajor() -> str:
    return majorVersion


def doomPin(major: str = "") -> str:
    """The doom commit pinned for an Emacs major in bleeLib.sh's doomPinForEmacsMajor, read as text: a sha, or
    "latest" when the major has no entry (bring-up mode), or "" when bleeLib.sh cannot be read."""
    major = major or emacsMajor()
    path = os.environ.get(bleeLibOverrideName) or bleeLib
    try:
        with open(path) as f:
            text = f.read()
    except OSError:
        return ""
    body = re.search(r"function doomPinForEmacsMajor \{(.*?)\n\}", text, re.S)
    if not body:
        return ""
    for case in re.finditer(r"^\s*([0-9*]+)\)(.*?);;", body.group(1), re.S | re.M):
        if case.group(1) in (major, "*"):
            echo = re.search(r'echo "([0-9a-f]{7,40}|latest)"', case.group(2))
            return echo.group(1) if echo else ""
    return ""


def frameworkLink() -> str:
    return os.path.join(bleeBase(), "dooms", f"doomemacs-{emacsMajor()}")


def frameworkHead() -> str:
    """The full sha of the framework clone's HEAD, "" when absent."""
    if not os.path.isdir(frameworkLink()):
        return ""
    out = _run(["git", "-C", frameworkLink(), "rev-parse", "HEAD"]).strip()
    return out if re.fullmatch(r"[0-9a-f]{40}", out) else ""


def doomDirBase() -> str:
    return os.path.join(bleeBase(), "dooms", "doom-base-blee3")


def runBase() -> str:
    return os.path.join(bleeBase(), "emacsVers", emacsVersion(), "doom-run-blee3")


def built() -> bool:
    """The run base has straight's build directory for this Emacs version, with packages in it."""
    buildDir = os.path.join(runBase(), ".local", "straight", f"build-{emacsVersion()}")
    return bool(emacsVersion()) and os.path.isdir(buildDir) and bool(os.listdir(buildDir))


def doomVersion() -> typing.Tuple[str, str]:
    """(emacs version, doom commit) that bin/doom version reports under the default Emacs; ("", "") when it
    does not run."""
    doom = os.path.join(runBase(), "bin", "doom")
    if not os.path.isfile(doom):
        return ("", "")
    env = dict(os.environ, EMACS=majorEmacsPath(), DOOMDIR=doomDirBase())
    out = _run([doom, "version"], env=env)
    emacsV = re.search(r"^emacs\s+v(\S+)", out, re.M)
    doomV = re.search(r"^doom\s+v\S+\s+([0-9a-f]{7,40})", out, re.M)
    return (emacsV.group(1) if emacsV else "", doomV.group(1) if doomV else "")


def configIsWithBlee() -> bool:
    base = doomDirBase()
    try:
        with open(os.path.join(base, "config.el"), "rb") as a, open(os.path.join(base, "configWithBlee.el"), "rb") as b:
            return a.read() == b.read()
    except OSError:
        return False


def configChanged() -> typing.List[str]:
    """The config files of doom-base-blee3 that differ from the blee repo (git status), [] when clean."""
    out = _run(["git", "-C", os.path.realpath(doomDirBase()), "status", "--porcelain", "--"] + configFiles)
    return [line[3:] for line in out.splitlines() if line.strip()]


def stepCommand(step: str) -> typing.List[str]:
    args = [scriptPath(), "-v", "-n", "showRun", "-p", f"emacs={emacsMajor()}"]
    if step != "doomFrameworkPrep":
        args += ["-p", f"profile={profile}"]
    return args + ["-i", step]
