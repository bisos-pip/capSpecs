#!/usr/bin/env python
# -*- coding: utf-8 -*-

""" #+begin_org
* ~[Summary]~ :: A =CmndSvc= for assembling the developer platform: git anon to auth, pip PyPI to editable; fullCheck is read-only.
#+end_org """

####+BEGIN: b:py3:cs:file/dblockControls :classification "cs-mu"
""" #+begin_org
* [[elisp:(org-cycle)][| /Control Parameters Of This File/ |]] :: dblk ctrls classifications=cs-mu
#+BEGIN_SRC emacs-lisp
(setq-local b:dblockControls t) ; (setq-local b:dblockControls nil)
(put 'b:dblockControls 'py3:cs:Classification "cs-mu") ; one of cs-mu, cs-u, cs-lib, bpf-lib, pyLibPure
#+END_SRC
#+RESULTS:
: cs-mu
#+end_org """
####+END:

####+BEGIN: b:prog:file/proclamations :outLevel 1
""" #+begin_org
* *[[elisp:(org-cycle)][| Proclamations |]]* :: Libre-Halaal Software --- Part Of BISOS ---  Poly-COMEEGA Format.
** This is Libre-Halaal Software. © Neda Communications, Inc. Subject to AGPL.
** It is part of BISOS (ByStar Internet Services OS)
** Best read and edited  with Blee in Poly-COMEEGA (Polymode Colaborative Org-Mode Enhance Emacs Generalized Authorship)
#+end_org """
####+END:

####+BEGIN: b:prog:file/particulars :authors ("./inserts/authors-mb.org")
""" #+begin_org
* *[[elisp:(org-cycle)][| Particulars |]]* :: Authors, version
** This File: /bisos/git/bxRepos/bisos-pip/capSpecs/py3/bin/pltfrmDevelBecome-assemble.cs
** Authors: Mohsen BANAN, http://mohsen.banan.1.byname.net/contact
#+end_org """
####+END:

####+BEGIN: b:py3:file/particulars-csInfo :status "inUse"
""" #+begin_org
* *[[elisp:(org-cycle)][| Particulars-csInfo |]]*
#+end_org """
import typing
csInfo: typing.Dict[str, typing.Any] = { 'moduleName': ['pltfrmDevelBecome-assemble'], }
csInfo['version'] = '202409222627'
csInfo['status']  = 'inUse'
csInfo['panel'] = 'pltfrmDevelBecome-assemble-Panel.org'
csInfo['groupingType'] = 'IcmGroupingType-pkged'
csInfo['cmndParts'] = 'IcmCmndParts[common] IcmCmndParts[param]'
####+END:

""" #+begin_org
* [[elisp:(org-cycle)][| ~Description~ |]] :: [[file:/bisos/git/auth/bxRepos/blee-binders/bisos-core/PyFwrk/bisos-pip/bisos.cs/_nodeBase_/fullUsagePanel-en.org][BISOS CmndSvcs Panel]]   [[elisp:(org-cycle)][| ]]

This is the assemble step of pltfrmDevelBecome-cbs.pcs: the slow, idempotent build of a developer platform
(capSpecs AI-WorkPlan.org Stage 2). Its steps are 2.3 alias precondition (checked, never written), 2.4 git anon
to auth, 2.5 dev-bisos3 and editable installs, 2.6 the packages of rawBisos-sbom.pcs, 2.7 verification. This is
fullCheck (2.7) is the read-only, offline verification; fullUpdate runs the steps and ends with it. Neither this
step nor the CBM walker report a failure as one, so a failure here is loud and exits non-zero. The fast switch is pltfrmDevelUnBecome.
** Status: Steps 2.3 to 2.7 implemented; not run on a rawBisos VM
#+end_org """

####+BEGIN: b:prog:file/orgTopControls :outLevel 1
""" #+begin_org
* [[elisp:(org-cycle)][| Controls |]] :: [[elisp:(delete-other-windows)][(1)]] | [[elisp:(show-all)][Show-All]]  [[elisp:(org-shifttab)][Overview]]  [[elisp:(progn (org-shifttab) (org-content))][Content]] | [[file:Panel.org][Panel]] | [[elisp:(blee:ppmm:org-mode-toggle)][Nat]] | [[elisp:(bx:org:run-me)][Run]] | [[elisp:(bx:org:run-me-eml)][RunEml]] | [[elisp:(progn (save-buffer) (kill-buffer))][S&Q]]  [[elisp:(save-buffer)][Save]]  [[elisp:(kill-buffer)][Quit]] [[elisp:(org-cycle)][| ]]
** /Version Control/ ::  [[elisp:(call-interactively (quote cvs-update))][cvs-update]]  [[elisp:(vc-update)][vc-update]] | [[elisp:(bx:org:agenda:this-file-otherWin)][Agenda-List]]  [[elisp:(bx:org:todo:this-file-otherWin)][ToDo-List]]

#+end_org """
####+END:

####+BEGIN: b:py3:file/workbench :outLevel 1
""" #+begin_org
* [[elisp:(org-cycle)][| Workbench |]] :: [[elisp:(python-check (format "/bisos/venv/py3/bisos3/bin/python -m pyclbr %s" (bx:buf-fname))))][pyclbr]] || [[elisp:(python-check (format "/bisos/venv/py3/bisos3/bin/python -m pydoc ./%s" (bx:buf-fname))))][pydoc]] || [[elisp:(python-check (format "/bisos/pipx/bin/pyflakes %s" (bx:buf-fname)))][pyflakes]] | [[elisp:(python-check (format "/bisos/pipx/bin/pychecker %s" (bx:buf-fname))))][pychecker (executes)]] | [[elisp:(python-check (format "/bisos/pipx/bin/pycodestyle %s" (bx:buf-fname))))][pycodestyle]] | [[elisp:(python-check (format "/bisos/pipx/bin/flake8 %s" (bx:buf-fname))))][flake8]] | [[elisp:(python-check (format "/bisos/pipx/bin/pylint %s" (bx:buf-fname))))][pylint]]  [[elisp:(org-cycle)][| ]]
#+end_org """
####+END:

####+BEGIN: b:py3:cs:framework/imports :basedOn "classification"
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CsFrmWrk   [[elisp:(outline-show-subtree+toggle)][||]] *Imports* =Based on Classification=cs-mu=
#+end_org """
from bisos import b
from bisos.b import cs
from bisos.b import b_io
from bisos.common import csParam

import collections
import os
import subprocess
import pathlib

from bisos.capSpecs import pltfrmDevelBecomeInfo as info
####+END:

""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CsFrmWrk   [[elisp:(outline-show-subtree+toggle)][||]] ~csuList emacs-list Specifications~  [[elisp:(blee:org:code-block/above-run)][ /Eval Below/ ]] [[elisp:(org-cycle)][| ]]
#+BEGIN_SRC emacs-lisp
(setq  b:py:cs:csuList
  (list
   "bisos.b.cs.ro"
   "bisos.csPlayer.bleep"
 ))
#+END_SRC
#+RESULTS:
| bisos.b.cs.ro | bisos.csPlayer.bleep |
#+end_org """

####+BEGIN: b:py3:cs:framework/csuListProc :pyImports t :csuImports t :csuParams t :csmuParams nil
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CsFrmWrk   [[elisp:(outline-show-subtree+toggle)][||]] =Process CSU List= with /2/ in csuList pyImports=t csuImports=t csuParams=t
#+end_org """

from bisos.b.cs import ro
from bisos.csPlayer import bleep


csuList = [ 'bisos.b.cs.ro', 'bisos.csPlayer.bleep', ]

g_importedCmndsModules = cs.csuList_importedModules(csuList)

def g_extraParams():
    csParams = cs.param.CmndParamDict()
    cs.csuList_commonParamsSpecify(csuList, csParams)
    cs.argsparseBasedOnCsParams(csParams)

####+END:

####+BEGIN: b:py3:cs:main/exposedSymbols :classes ()
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CsFrmWrk   [[elisp:(outline-show-subtree+toggle)][||]] ~CS Controls and Exposed Symbols List Specification~ with /0/ in Classes List
#+end_org """
####+END:

cs.invOutcomeReportControl(cmnd=True, ro=True)

####+BEGIN: blee:bxPanel:foldingSection :outLevel 0 :sep nil :title "CmndSvcs" :anchor ""  :extraInfo "Command Services Section"
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*     [[elisp:(outline-show-subtree+toggle)][| _CmndSvcs_: |]]  Command Services Section  [[elisp:(org-shifttab)][<)]] E|
#+end_org """
####+END:


def _aliasOk(alias: str) -> bool:
    """Print the state of the git alias; True only if it authenticates (WorkPlan 2.3)."""
    state, detail = info.aliasState(alias)
    print(f"{'git alias':14} {alias}: {state}  {detail}")
    if state == "ok":
        return True
    hints = {
        "notDefined": "add the Host entry (pltfrmDevelCreds, Stage 3); this step does not write ~/.ssh/config",
        "authFailed": "the key is not accepted: fix the credentials (pltfrmDevelCreds, Stage 3)",
        "hostKeyUnknown": "the host key is not known: connect once by hand and accept it",
        "unreachable": "the git server cannot be reached from this host",
        "noSsh": "install the ssh client",
    }
    print(f"pltfrmDevelBecome: FAILED: the git alias {alias} is not usable: {hints.get(state, detail)}")
    b_io.eh.problem_usageError(f"git alias {alias}: {state}: {detail}")
    return False


def _reposOk(alias: str) -> bool:
    """Every repo of the registry and of bxRepos-gitist.pcs is cloned in auth and writable (WorkPlan 2.4)."""
    try:
        problems, checked = info.reposProblems(alias)
    except ValueError as err:
        print(f"pltfrmDevelBecome: FAILED: cannot list the repos: {err}")
        return False
    for each in problems:
        print(f"  {each}")
    print(f"{'git repos':14} {checked} clone(s) checked in {info.authBxRepos}: "
          f"{'complete and writable' if not problems else str(len(problems)) + ' PROBLEM(S)'}")
    if problems:
        b_io.eh.problem_usageError(f"git repos are not complete in auth: {len(problems)} problem(s)")
    return not problems


def _anonToAuth(alias: str) -> bool:
    """WorkPlan 2.4: alias precondition, pbdUpdate, gitist clones, then the check. Never the symlink."""
    if not _aliasOk(alias):
        return False
    os.makedirs(info.logDir(), exist_ok=True)
    steps = [
        ("pbdUpdate", ["bx-gitReposBases", "-v", "20", f"--baseDir={info.authBxRepos}", "--pbdName=bxReposRoot",
                       "--vcMode=auth", f"--gitLabel={alias}", "-i", "pbdUpdate", "all"], None),
        ("gitist", ["bxRepos-gitist.pcs", "-i", "csCmndEval", "all"], info.gitistEnv(alias)),
    ]
    for name, args, env in steps:
        log = os.path.join(info.logDir(), f"{name}.log")
        print(f"pltfrmDevelBecome: {name}: {' '.join(args)}   (log: {log})")
        with open(log, "w") as out:
            rc = subprocess.run(args, check=False, stdout=out, stderr=subprocess.STDOUT, env=env, cwd="/tmp").returncode
        print(f"pltfrmDevelBecome: {name}: exit {rc} (not trusted: the check below decides)")
    return _reposOk(alias)


def _switchOk(mode: str) -> bool:
    """WorkPlan 2.4: the bxRepos link. To auth only when the repos are complete and writable."""
    problems = None
    if mode == "auth":
        try:
            problems, _ = info.reposProblems()
        except ValueError as err:
            problems = [f"cannot list the repos: {err}"]
    ok, message = info.bxReposSwitch(mode, problems)
    print(f"pltfrmDevelBecome: {'switched: ' if ok else 'FAILED: '}{message}")
    if not ok:
        b_io.eh.problem_usageError(message)
    return ok


def _pipOk(verbose: bool = True) -> bool:
    """Every package is editable in dev-bisos3 from auth (the known four skipped), and bisos.b imports (WorkPlan 2.5)."""
    if not info.devVenvPresent():
        print(f"pltfrmDevelBecome: FAILED: {info.devVenvPath()} is absent")
        b_io.eh.problem_usageError(f"{info.devVenvPath()} is absent")
        return False
    try:
        rows = info.pipRows()
    except ValueError as err:
        print(f"pltfrmDevelBecome: FAILED: {err}")
        return False
    bad = [name for name, state in rows if state == "NOT EDITABLE"]
    if verbose:
        for name, state in rows:
            if state != "editable":
                print(f"  {name:26} {state}")
    nEditable = len([each for each in rows if each[1] == "editable"])
    skipped = [name for name, state in rows if state.startswith("skipped")]
    importOk = info.importBOk()
    print(f"{'pip':14} {nEditable} of {len(rows)} editable from {info.packagesRoot()}; "
          f"skipped: {', '.join(skipped) or 'none'}; NOT EDITABLE: {', '.join(bad) or 'none'}; "
          f"from bisos import b: {'ok' if importOk else 'FAILS'}")
    if bad or not importOk:
        b_io.eh.problem_usageError(f"pip is not complete in {info.devVenvPath()}: not editable {bad}, import b ok={importOk}")
    return not bad and importOk


def _venvEnsure() -> bool:
    """Create dev-bisos3 when absent (WorkPlan 2.5); nothing when present."""
    if info.devVenvPresent():
        print(f"{'venv':14} {info.devVenvPath()}: present")
        return True
    os.makedirs(info.logDir(), exist_ok=True)
    log = os.path.join(info.logDir(), "venvSetup.log")
    args = ["bisosPyVenvSetup.sh", "-i", "pyVenv_DevSetup"]
    print(f"pltfrmDevelBecome: venv: {' '.join(args)}   (log: {log})")
    with open(log, "w") as out:
        rc = subprocess.run(args, check=False, stdout=out, stderr=subprocess.STDOUT, cwd="/tmp").returncode
    print(f"pltfrmDevelBecome: venv: exit {rc} (not trusted: the venv must exist)")
    if not info.devVenvPresent():
        print(f"pltfrmDevelBecome: FAILED: {info.devVenvPath()} was not created; see {log}")
        b_io.eh.problem_usageError(f"{info.devVenvPath()} was not created")
        return False
    return True


def _pipInstalls() -> bool:
    """WorkPlan 2.5: the venv, then every package editable from auth. Already editable: skipped. Known failures:
    skipped and reported. Any other package that does not end up editable fails the step."""
    if not _venvEnsure():
        return False
    os.makedirs(info.logDir(), exist_ok=True)
    try:
        before = info.editableLocations()
    except ValueError as err:
        print(f"pltfrmDevelBecome: FAILED: {err}")
        return False
    for name in info.packageDirs():
        if info.packagePath(name) in before:
            continue
        if name in info.knownNotInstalling:
            print(f"pltfrmDevelBecome: pip: {name}: skipped (known not installing)")
            continue
        log = os.path.join(info.logDir(), f"pip-{name}.log")
        args = info.pypiProcCommand(name)
        print(f"pltfrmDevelBecome: pip: {name}: {' '.join(args)}   (log: {log})")
        with open(log, "w") as out:
            rc = subprocess.run(args, check=False, stdout=out, stderr=subprocess.STDOUT, cwd=info.packagePath(name)).returncode
        print(f"pltfrmDevelBecome: pip: {name}: exit {rc} (not trusted: pip list -e decides)")
    return _pipOk()


def _rawBisosOk() -> bool:
    """Every pip entry of rawBisos-sbom.pcs is in dev-bisos3: editable from its clone, else from PyPI (WorkPlan 2.6)."""
    try:
        rows = info.rawBisosPipRows()
    except ValueError as err:
        print(f"pltfrmDevelBecome: FAILED: {err}")
        b_io.eh.problem_usageError(str(err))
        return False
    bad = [entry for entry, state, _ in rows if state.startswith("NOT")]
    odd = [entry for entry, _, _ in rows if info.entryDir(entry) and entry.startswith("bisos-pip/")]
    for entry, state, _ in rows:
        if state != "editable":
            print(f"  {entry:30} {state}")
    print(f"{'rawBisos sbom':14} {len(rows)} pip entries: "
          f"{len([each for each in rows if each[1] == 'editable'])} editable, "
          f"{len([each for each in rows if each[1] == 'from PyPI'])} from PyPI; NOT OK: {', '.join(bad) or 'none'}")
    if odd:
        print(f"{'':14} note: {len(odd)} entries are repo paths (bisos-pip/<x>), not pip names: a defect in rawBisos-sbom.pcs")
    if bad:
        b_io.eh.problem_usageError(f"rawBisos-sbom.pcs pip entries not in {info.devVenvPath()}: {bad}")
    return not bad


def _rawBisosInstalls() -> bool:
    """WorkPlan 2.6: an entry with no clone is installed from PyPI. The sbom itself is never run: its pip is
    taken from PATH and an update could replace an editable install with a PyPI copy."""
    try:
        rows = info.rawBisosPipRows()
    except ValueError as err:
        print(f"pltfrmDevelBecome: FAILED: {err}")
        b_io.eh.problem_usageError(str(err))
        return False
    os.makedirs(info.logDir(), exist_ok=True)
    for entry, state, pipName in rows:
        if state != "NOT INSTALLED":
            continue
        log = os.path.join(info.logDir(), f"rawBisos-{pipName}.log")
        args = [os.path.join(info.devVenvPath(), "bin", "pip"), "install", pipName]
        print(f"pltfrmDevelBecome: rawBisos sbom: {entry}: {' '.join(args)}   (log: {log})")
        with open(log, "w") as out:
            rc = subprocess.run(args, check=False, stdout=out, stderr=subprocess.STDOUT, cwd="/tmp").returncode
        print(f"pltfrmDevelBecome: rawBisos sbom: {entry}: exit {rc} (not trusted: pip show decides)")
    return _rawBisosOk()


def _fullCheck() -> bool:
    """WorkPlan 2.7: read-only, offline verification of the whole developer platform; one summary table.
    The git alias is not a row (network, credentials): gitAliasCheck does that, and fullUpdate runs it first."""
    alias = info.aliasName()
    mode = info.bxReposMode()
    venvOk = info.devVenvPresent()
    results = []

    def row(name: str, ok: bool, detail: str) -> None:
        results.append((name, ok, detail))

    row("bxRepos mode", mode == "auth", f"{info.bxReposLinkPath()} is {mode} (auth wanted)")
    row("git repos", _reposOk(alias), f"every repo cloned in {info.authBxRepos}, origin git@{alias}:")
    row("venv", venvOk, f"{info.devVenvPath()} {'present' if venvOk else 'ABSENT'}")
    row("pip", venvOk and _pipOk(), "every package editable from auth (known failures skipped)")
    row("rawBisos sbom", venvOk and _rawBisosOk(), "its pipPkgs_list is in dev-bisos3")
    row("import b", venvOk and info.importBOk(), "from bisos import b, in dev-bisos3")

    print("pltfrmDevelBecome: summary")
    for name, ok, detail in results:
        print(f"  {name:14} {'OK  ' if ok else 'FAIL'} {detail}")
    failedRows = [name for name, ok, _ in results if not ok]
    if failedRows:
        print(f"pltfrmDevelBecome: NOT complete, failed: {', '.join(failedRows)}")
        print(f"pltfrmDevelBecome verify: fail {', '.join(failedRows)}")   # read by bisos.capability cbm_verify
        b_io.eh.problem_usageError(f"pltfrmDevelBecome is not complete: failed {failedRows}")
        return False
    print("pltfrmDevelBecome: complete")
    print("pltfrmDevelBecome verify: ok")   # read by bisos.capability cbm_verify
    return True


####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "examples" :extent "verify" :ro "noCli" :comment "FrameWrk: CS-Main-Examples" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<examples>>  *FrameWrk: CS-Main-Examples*  =verify= ro=noCli   [[elisp:(org-cycle)][| ]]
#+end_org """
class examples(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}
    rtInvConstraints = cs.rtInvoker.RtInvoker.new_noRo() # NO RO From CLI

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:
        """FrameWrk: CS-Main-Examples"""
        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        self.cmndDocStr(f""" #+begin_org
***** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  Conventional top level example.
        #+end_org """)

        cs.examples.myName(cs.G.icmMyName(), cs.G.icmMyFullName())
        cs.examples.commonBrief()

        cmnd = cs.examples.cmndEnter

        cs.examples.menuChapter('=Verification (read-only)=')
        cmnd('fullCheck', comment=" # the whole developer platform, one summary table (offline)")
        cs.examples.menuChapter('=Precondition (read-only, uses the network)=')
        cmnd('gitAliasCheck', comment=" # the ssh Host alias of pltfrmDevelCreds resolves and authenticates")
        cmnd('gitAliasCheck', args="mb2_github", comment=" # an alias other than the default or PLTFRM_DEVEL_GIT_ALIAS")
        cs.examples.menuChapter('=Git: anon to auth=')
        cmnd('gitReposCheck', comment=" # read-only, offline: every repo cloned in auth and writable")
        cmnd('gitAnonToAuth', comment=" # network: alias check, pbdUpdate, gitist clones, then the check")
        cmnd('gitSymlinkSwitch', args="auth", comment=" # /bisos/git/bxRepos -> auth; refuses unless gitReposCheck is clean")
        cs.examples.menuChapter('=Pip: PyPI to editable=')
        cmnd('pipCheck', comment=" # read-only: every package editable in dev-bisos3 from auth; bisos.b imports")
        cmnd('pipVenvEnsure', comment=" # creates dev-bisos3 when absent")
        cmnd('pipEditableInstalls', comment=" # pkgReInstall edit for each package not yet editable (slow on a fresh platform)")
        cs.examples.menuChapter('=rawBisos-sbom.pcs pip packages=')
        cmnd('rawBisosPipCheck', comment=" # read-only: its pipPkgs_list, each editable from its clone or from PyPI")
        cmnd('rawBisosPipInstalls', comment=" # PyPI install of an entry that has no clone; never runs the sbom")
        cs.examples.menuChapter('=Full Update=')
        cmnd('fullUpdate', comment=" # git, pip and rawBisos steps, then fullCheck; exits clean only if it passes")

        return(cmndOutcome)

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "fullCheck" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<fullCheck>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class fullCheck(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(f""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  Read-only, offline (WorkPlan 2.7): the whole developer platform in one summary table.
bxRepos mode is auth, every repo cloned writable in auth, dev-bisos3, every package editable from auth,
the pip packages of rawBisos-sbom.pcs, and from bisos import b. Fails if any row fails. The git alias is
not a row (network, credentials): gitAliasCheck.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i fullCheck
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _fullCheck():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "gitAliasCheck" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 1 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<gitAliasCheck>>  =verify= argsMax=1 ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class gitAliasCheck(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 1,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
             argsList: typing.Optional[list[str]]=None,  # CsArgs
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, argsList).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(f""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  The precondition (WorkPlan 2.3): the ssh Host alias of the git server resolves and authenticates.
The alias is the argument, else PLTFRM_DEVEL_GIT_ALIAS, else mb2_github. Never writes ~/.ssh/config:
pltfrmDevelCreds (Stage 3) prepares the credentials, this only checks them. Fails with a clear message.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i gitAliasCheck
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        alias = argsList[0] if argsList else info.aliasName()
        if not _aliasOk(alias):
            return failed(cmndOutcome)

        print(f"pltfrmDevelBecome: the git alias {alias} authenticates")
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "gitReposCheck" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<gitReposCheck>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class gitReposCheck(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  Read-only, offline (WorkPlan 2.4): every repo of the registry and of bxRepos-gitist.pcs is cloned in auth
and writable (origin git@<alias>:). Fails listing each gap.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i gitReposCheck
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _reposOk(info.aliasName()):
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "gitAnonToAuth" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<gitAnonToAuth>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class gitAnonToAuth(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  WorkPlan 2.4: the alias precondition, then pbdUpdate and the gitist clones (network, slow, idempotent), then
the check. Never touches the bxRepos symlink. The exit codes of the tools are not trusted: the check decides.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i gitAnonToAuth
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _anonToAuth(info.aliasName()):
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "gitSymlinkSwitch" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 1 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<gitSymlinkSwitch>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class gitSymlinkSwitch(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 1,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
             argsList: typing.Optional[list[str]]=None,  # CsArgs
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, argsList).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  WorkPlan 2.4: point /bisos/git/bxRepos at auth (default) or anon, atomically; to auth only when the repos are
complete and writable (refuses otherwise). Idempotent; never replaces a real directory.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i gitSymlinkSwitch
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        mode = argsList[0] if argsList else "auth"
        if not _switchOk(mode):
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "pipCheck" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<pipCheck>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class pipCheck(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  Read-only (WorkPlan 2.5): every bisos-pip/*/py3 is editable in dev-bisos3 from auth (lcnt, pycs, qmailSvc,
gnome skipped when not installing), and bisos.b imports. Fails listing each gap.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i pipCheck
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _pipOk():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "pipVenvEnsure" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<pipVenvEnsure>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class pipVenvEnsure(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  WorkPlan 2.5: create dev-bisos3 with bisosPyVenvSetup.sh -i pyVenv_DevSetup when absent; nothing when present.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i pipVenvEnsure
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _venvEnsure():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "pipEditableInstalls" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<pipEditableInstalls>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class pipEditableInstalls(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  WorkPlan 2.5: the venv, then pypiProc.sh -i pkgReInstall edit for each package not yet editable from auth
(namespace, b first; one log each). Known failures are skipped and reported. The exit codes are not trusted:
pip list -e decides, and any other package that is not editable fails the step.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i pipEditableInstalls
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _pipInstalls():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "rawBisosPipCheck" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<rawBisosPipCheck>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class rawBisosPipCheck(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  Read-only (WorkPlan 2.6): every pip entry of rawBisos-sbom.pcs (its own pipPkgs_list) is in dev-bisos3, editable from
its clone, else installed from PyPI. Fails listing each gap.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i rawBisosPipCheck
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _rawBisosOk():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "rawBisosPipInstalls" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<rawBisosPipInstalls>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class rawBisosPipInstalls(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  WorkPlan 2.6: install from PyPI each pip entry of rawBisos-sbom.pcs that has no clone and is not installed. The sbom
itself is never run (its pip comes from PATH; an update could replace an editable install).
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i rawBisosPipInstalls
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _rawBisosInstalls():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "fullUpdate" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 0 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<fullUpdate>>  =verify= ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class fullUpdate(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 0, 'Max': 0,}

    @cs.track(fnLoc=True, fnEntry=True, fnExit=True)
    def cmnd(self,
             rtInv: cs.RtInvoker,
             cmndOutcome: b.op.Outcome,
    ) -> b.op.Outcome:

        failed = b_io.eh.badOutcome
        callParamsDict = {}
        if self.invocationValidate(rtInv, cmndOutcome, callParamsDict, None).isProblematic():
            return failed(cmndOutcome)
####+END:
        if self.cmndDocStr(f""" #+begin_org
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  The git (2.3, 2.4), pip (2.5) and rawBisos (2.6) steps, then fullCheck (2.7): clean only if it passes.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  pltfrmDevelBecome-assemble.cs -i fullUpdate
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        alias = info.aliasName()
        if not _anonToAuth(alias):
            return failed(cmndOutcome)
        if not _switchOk("auth"):
            return failed(cmndOutcome)
        if not _pipInstalls():
            return failed(cmndOutcome)
        if not _rawBisosInstalls():
            return failed(cmndOutcome)
        if not _fullCheck():
            return failed(cmndOutcome)

    ####+BEGIN: blee:bxPanel:foldingSection :outLevel 0 :sep nil :title "Main" :anchor ""  :extraInfo "Framework DBlock"
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*     [[elisp:(outline-show-subtree+toggle)][| _Main_: |]]  Framework DBlock  [[elisp:(org-shifttab)][<)]] E|
#+end_org """
####+END:

####+BEGIN: b:py3:cs:framework/main :csInfo "csInfo" :noCmndEntry "examples" :extraParamsHook "g_extraParams" :importedCmndsModules "g_importedCmndsModules"
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CsFrmWrk   [[elisp:(outline-show-subtree+toggle)][||]] =g_csMain= (csInfo, _examples_, g_extraParams, g_importedCmndsModules)
#+end_org """

if __name__ == '__main__':
    cs.main.g_csMain(
        csInfo=csInfo,
        noCmndEntry=examples,  # specify a Cmnd name
        extraParamsHook=g_extraParams,
        importedCmndsModules=g_importedCmndsModules,
    )

####+END:

####+BEGIN: b:py3:cs:framework/endOfFile :basedOn "classification"
""" #+begin_org
* [[elisp:(org-cycle)][| *End-Of-Editable-Text* |]] :: emacs and org variables and control parameters
#+end_org """

#+STARTUP: showall

### local variables:
### no-byte-compile: t
### end:
####+END:
