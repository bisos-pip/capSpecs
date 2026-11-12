# -*- coding: utf-8 -*-
""" #+begin_org
* ~[Summary]~ :: The single source of facts of the emacs31SrcBuild capability: the pinned tag, the build script
and its steps, where the sources and the binaries are, and the read-only probes of the result. Pure: no side
effects at import.
#+end_org """

import glob
import os
import re
import subprocess
import typing

#
# capSpecs AI-WorkPlan.org, Stage 5 (5.1, decided with the user 2026-11-12):
#   - Emacs 31 is built from the release tag emacs-31.1 by /bisos/blee/bin/lcaEmacsSrcBinsPrep.sh (not edited),
#     with the script's own options and its tree-sitter (v0.27.0 into /usr/local). Same on Container, VM, Box.
#   - When 31 is installed it is the default: /usr/local/bin/emacs points to it. Debian's /bin/emacs stays.
#   - deb-src is the rawBisos install's to enable; here it is only checked (AI-Outputs ledger, 2026-11-12).
#

selector = "emacs31"        # the script's srcPkgSelector; its _git form is the release tag
gitTag = "emacs-31.1"
majorVersion = "31"
fullVersion = "31.1"

buildScript = "lcaEmacsSrcBinsPrep.sh"
buildScriptOverrideName = "EMACS_SRC_BUILD_SCRIPT"     # for tests only
binDirOverrideName = "EMACS_SRC_BUILD_BINDIR"          # for tests only
srcDirOverrideName = "EMACS_SRC_BUILD_SRCDIR"          # for tests only
aptDirOverrideName = "EMACS_SRC_BUILD_APTDIR"          # for tests only
logDirOverrideName = "EMACS_SRC_BUILD_LOGDIR"

binDirDefault = "/usr/local/bin"
srcObtainDirDefault = f"/bisos/var/srcPkgs/{gitTag}"   # the script clones into it; the tree is emacs/
aptDirDefault = "/etc/apt"
logDirDefault = "/tmp/emacs31SrcBuild"

# The script's steps, in order. Its srcFullBuild is not used: it pauses for Enter before each step.
steps: typing.List[str] = [
    "srcEnvSetup",      # apt build-dep emacs and the Debian-generation packages (needs deb-src)
    "srcPkgObtain",     # git clone --depth 1 -b emacs-31.1; skipped when the tree exists (a 2nd clone fails)
    "srcBuild",         # tree-sitter, autogen, configure, make bootstrap
    "srcBinInstall",    # sudo make install
    "postInstall",      # emacs-31, emacsclient-31; removes /usr/local/bin/emacs
    "setAsDefault",     # /usr/local/bin/emacs -> emacs-31.1, then bleeclient (needs a running Blee)
]


def scriptPath() -> str:
    return os.environ.get(buildScriptOverrideName) or buildScript


def binDir() -> str:
    return os.environ.get(binDirOverrideName) or binDirDefault


def srcObtainDir() -> str:
    return os.environ.get(srcDirOverrideName) or srcObtainDirDefault


def srcTreeDir() -> str:
    return os.path.join(srcObtainDir(), "emacs")


def logDir() -> str:
    return os.environ.get(logDirOverrideName) or logDirDefault


def stepCommand(step: str) -> typing.List[str]:
    return [scriptPath(), "-v", "-n", "showRun", "-i", step, selector]


def sourcesPresent() -> bool:
    return os.path.isfile(os.path.join(srcTreeDir(), "configure.ac"))


def debSrcEnabled() -> bool:
    """A deb-src source is configured: a deb-src line in a .list file, or Types with deb-src in a deb822
    .sources file. Read-only."""
    aptDir = os.environ.get(aptDirOverrideName) or aptDirDefault
    for path in [os.path.join(aptDir, "sources.list")] + sorted(glob.glob(os.path.join(aptDir, "sources.list.d", "*.list"))):
        if os.path.isfile(path):
            with open(path) as f:
                if any(line.startswith("deb-src ") for line in f):
                    return True
    for path in sorted(glob.glob(os.path.join(aptDir, "sources.list.d", "*.sources"))):
        with open(path) as f:
            for line in f:
                if re.match(r"^Types:.*\bdeb-src\b", line):
                    return True
    return False


def emacsPath() -> str:
    return os.path.join(binDir(), f"emacs-{majorVersion}")


def emacsclientPath() -> str:
    return os.path.join(binDir(), f"emacsclient-{majorVersion}")


def _run(args: typing.List[str], timeout: int = 60) -> str:
    try:
        out = subprocess.run(args, check=False, capture_output=True, text=True, timeout=timeout)
    except (OSError, subprocess.TimeoutExpired):
        return ""
    return (out.stdout or "") + (out.stderr or "")


def programVersion(path: str) -> str:
    """The version a program reports in its first --version line ("GNU Emacs 31.1", "emacsclient 31.1"); ""
    when it does not run."""
    if not os.path.isfile(path):
        return ""
    lines = _run([path, "--version"]).splitlines()
    match = re.search(r"\s(\d+\.\d+(\.\d+)*)\s*$", lines[0]) if lines else None
    return match.group(1) if match else ""


def emacsEval(expr: str) -> str:
    """What the installed emacs-31 prints for (princ <expr>) in batch mode; "" when it does not run."""
    if not os.path.isfile(emacsPath()):
        return ""
    return _run([emacsPath(), "-Q", "--batch", "--eval", f"(princ {expr})"]).strip()


def installed() -> bool:
    """emacs-31 and emacsclient-31 are there and report 31.1. The quick test that makes fullUpdate skip."""
    return programVersion(emacsPath()) == fullVersion and programVersion(emacsclientPath()) == fullVersion


def defaultTarget() -> str:
    """Where binDir/emacs leads (followed to the end), "" when absent."""
    path = os.path.join(binDir(), "emacs")
    return os.path.realpath(path) if os.path.lexists(path) else ""


def clientLinkTarget() -> str:
    """Where binDir/emacsclient leads (followed to the end), "" when absent."""
    path = os.path.join(binDir(), "emacsclient")
    return os.path.realpath(path) if os.path.lexists(path) else ""


def clientLinkOk() -> bool:
    """binDir/emacsclient is absent (PATH falls through to Debian's) or leads to an emacsclient. The script's
    setAsDefault ends with bleeclient emacsClientMatchingSet, which, when not run from a shell inside Emacs,
    takes another program for the Emacs server: seen 2026-11-12, emacsclient -> /usr/bin/xfce4-terminal."""
    target = clientLinkTarget()
    return not target or os.path.basename(target).startswith("emacsclient")


def defaultIsThis() -> bool:
    return os.path.basename(defaultTarget()).startswith(f"emacs-{fullVersion}")
