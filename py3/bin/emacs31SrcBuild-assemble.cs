#!/usr/bin/env python
# -*- coding: utf-8 -*-

""" #+begin_org
* ~[Summary]~ :: A =CmndSvc= for assembling Emacs 31 from source (tag emacs-31.1) and making it the default; fullCheck is read-only.
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
** This File: /bisos/git/bxRepos/bisos-pip/capSpecs/py3/bin/emacs31SrcBuild-assemble.cs
** Authors: Mohsen BANAN, http://mohsen.banan.1.byname.net/contact
#+end_org """
####+END:

####+BEGIN: b:py3:file/particulars-csInfo :status "inUse"
""" #+begin_org
* *[[elisp:(org-cycle)][| Particulars-csInfo |]]*
#+end_org """
import typing
csInfo: typing.Dict[str, typing.Any] = { 'moduleName': ['emacs31SrcBuild-assemble'], }
csInfo['version'] = '202611121930'
csInfo['status']  = 'inUse'
csInfo['panel'] = 'emacs31SrcBuild-assemble-Panel.org'
csInfo['groupingType'] = 'IcmGroupingType-pkged'
csInfo['cmndParts'] = 'IcmCmndParts[common] IcmCmndParts[param]'
####+END:

""" #+begin_org
* [[elisp:(org-cycle)][| ~Description~ |]] :: [[file:/bisos/git/auth/bxRepos/blee-binders/bisos-core/PyFwrk/bisos-pip/bisos.cs/_nodeBase_/fullUsagePanel-en.org][BISOS CmndSvcs Panel]]   [[elisp:(org-cycle)][| ]]

This is the assemble step of emacs31SrcBuild-cbs.pcs (capSpecs AI-WorkPlan.org Stage 5): Emacs 31 built from
the release tag emacs-31.1 by /bisos/blee/bin/lcaEmacsSrcBinsPrep.sh, which is used as it is, one step at a time
(its srcFullBuild pauses for Enter). The result is the default: /usr/local/bin/emacs points to it. deb-src is a
precondition, only checked. fullCheck is the read-only verification; fullUpdate runs the steps and ends with it.
Neither the script nor g_csMain report a failure as one, so the verdict is the text: complete or NOT complete.
** Status: Implemented (WorkPlan 5.2)
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

import os
import subprocess

from bisos.capSpecs import emacs31SrcBuildInfo as info
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


def _debSrcOk() -> bool:
    """The precondition: deb-src is configured (the rawBisos install's to enable; only checked here)."""
    if info.debSrcEnabled():
        return True
    print("emacs31SrcBuild: FAILED: no deb-src in the apt sources; apt-get build-dep emacs needs it. Enable it "
          "(Debian 13: Types: deb deb-src in /etc/apt/sources.list.d/debian.sources), then apt-get update")
    b_io.eh.problem_usageError("deb-src is not enabled")
    return False


def _stepRun(step: str) -> bool:
    """Run one step of the script, with a log. Its exit code is printed, not trusted: fullCheck decides."""
    if step not in info.steps:
        print(f"emacs31SrcBuild: FAILED: unknown step {step}; one of: {' '.join(info.steps)}")
        b_io.eh.problem_usageError(f"unknown step {step}")
        return False
    if step == "srcPkgObtain" and info.sourcesPresent():
        print(f"emacs31SrcBuild: srcPkgObtain: skipped, the sources are in {info.srcTreeDir()}")
        return True
    os.makedirs(info.logDir(), exist_ok=True)
    log = os.path.join(info.logDir(), f"{step}.log")
    args = info.stepCommand(step)
    print(f"emacs31SrcBuild: {step}: {' '.join(args)}   (log: {log})")
    with open(log, "w") as out:
        rc = subprocess.run(args, check=False, stdout=out, stderr=subprocess.STDOUT,
                            stdin=subprocess.DEVNULL, cwd="/tmp").returncode
    print(f"emacs31SrcBuild: {step}: exit {rc} (not trusted: fullCheck decides)")
    return True


def _fullCheck() -> bool:
    """Read-only: one summary table, the verdict as text."""
    results = []

    def row(name: str, ok: bool, detail: str) -> None:
        results.append((name, ok, detail))

    emacsVer = info.programVersion(info.emacsPath())
    clientVer = info.programVersion(info.emacsclientPath())
    runs = emacsVer == info.fullVersion
    nativeComp = info.emacsEval("(native-comp-available-p)") if runs else ""
    treesit = info.emacsEval("(treesit-available-p)") if runs else ""

    row("deb-src", info.debSrcEnabled(), "configured in the apt sources (needed by srcEnvSetup)")
    row("sources", info.sourcesPresent(), f"{info.srcTreeDir()} (tag {info.gitTag})")
    row("emacs", runs, f"{info.emacsPath()} reports {emacsVer or 'nothing'} ({info.fullVersion} wanted)")
    row("emacsclient", clientVer == info.fullVersion,
        f"{info.emacsclientPath()} reports {clientVer or 'nothing'} ({info.fullVersion} wanted)")
    row("native-comp", nativeComp == "t", f"(native-comp-available-p) is {nativeComp or 'not run'}")
    row("tree-sitter", treesit == "t", f"(treesit-available-p) is {treesit or 'not run'}")
    row("default", info.defaultIsThis(),
        f"{os.path.join(info.binDir(), 'emacs')} -> {info.defaultTarget() or 'ABSENT'} (emacs-{info.fullVersion} wanted)")
    row("client link", info.clientLinkOk(),
        f"{os.path.join(info.binDir(), 'emacsclient')} -> {info.clientLinkTarget() or 'absent'} (absent or an emacsclient)")

    print("emacs31SrcBuild: summary")
    for name, ok, detail in results:
        print(f"  {name:12} {'OK  ' if ok else 'FAIL'} {detail}")
    failedRows = [name for name, ok, _ in results if not ok]
    if failedRows:
        print(f"emacs31SrcBuild: NOT complete, failed: {', '.join(failedRows)}")
        b_io.eh.problem_usageError(f"emacs31SrcBuild is not complete: failed {failedRows}")
        return False
    print("emacs31SrcBuild: complete")
    return True


def _fullUpdate(force: bool) -> bool:
    """deb-src first; then every step unless 31.1 is installed (force rebuilds); the default is always set;
    then fullCheck."""
    if not _debSrcOk():
        return False
    if info.installed() and not force:
        print(f"emacs31SrcBuild: emacs {info.fullVersion} is installed: build skipped (force rebuilds)")
        steps = ["setAsDefault"] if not info.defaultIsThis() else []
    else:
        steps = info.steps
    for step in steps:
        _stepRun(step)
        if step == "srcPkgObtain" and not info.sourcesPresent():
            # the script reports a failed clone as done, and every later step would fail on the missing tree
            print(f"emacs31SrcBuild: FAILED: no sources in {info.srcTreeDir()} after srcPkgObtain; see its log. "
                  "The later steps are not run; retry with -i fullUpdate (or -i stepRun srcPkgObtain)")
            break
    return _fullCheck()


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
        cmnd('fullCheck', comment=" # deb-src, sources, emacs-31 and emacsclient-31 at 31.1, native-comp, tree-sitter, default")
        cs.examples.menuChapter('=One Step of lcaEmacsSrcBinsPrep.sh emacs31=')
        for step in info.steps:
            cmnd('stepRun', args=step, comment=f" # one step, log in {info.logDir()}/{step}.log")
        cs.examples.menuChapter('=Full Update=')
        cmnd('fullUpdate', comment=" # every step unless 31.1 is installed; the default; then fullCheck")
        cmnd('fullUpdate', args="force", comment=" # rebuild and reinstall even when 31.1 is installed")

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
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  Read-only (WorkPlan 5.2): one summary table. deb-src configured, the emacs-31.1 sources,
emacs-31 and emacsclient-31 reporting 31.1, native compilation and tree-sitter available, and /usr/local/bin/emacs
leading to emacs-31.1. Prints complete or NOT complete; fails if any row fails.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  emacs31SrcBuild-assemble.cs -i fullCheck
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _fullCheck():
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "stepRun" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 1 :argsMax 1 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<stepRun>>  =verify= argsMin=1 argsMax=1 ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class stepRun(cs.Cmnd):
    cmndParamsMandatory = [ ]
    cmndParamsOptional = [ ]
    cmndArgsLen = {'Min': 1, 'Max': 1,}

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
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  One step of lcaEmacsSrcBinsPrep.sh for emacs31 (srcEnvSetup, srcPkgObtain, srcBuild,
srcBinInstall, postInstall, setAsDefault), with its log. For resuming after a failure; fullCheck says what is left.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  emacs31SrcBuild-assemble.cs -i stepRun srcPkgObtain
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        if not _stepRun(argsList[0]):
            return failed(cmndOutcome)
        return cmndOutcome

####+BEGIN: b:py3:cs:cmnd/classHead :cmndName "fullUpdate" :comment "" :extent "verify" :ro "cli" :parsMand "" :parsOpt "" :argsMin 0 :argsMax 1 :pyInv ""
""" #+begin_org
*  _[[elisp:(blee:menu-sel:outline:popupMenu)][±]]_ _[[elisp:(blee:menu-sel:navigation:popupMenu)][Ξ]]_ [[elisp:(outline-show-branches+toggle)][|=]] [[elisp:(bx:orgm:indirectBufOther)][|>]] *[[elisp:(blee:ppmm:org-mode-toggle)][|N]]*  CmndSvc-   [[elisp:(outline-show-subtree+toggle)][||]] <<fullUpdate>>  =verify= argsMax=1 ro=cli   [[elisp:(org-cycle)][| ]]
#+end_org """
class fullUpdate(cs.Cmnd):
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
** [[elisp:(org-cycle)][| *CmndDesc:* | ]]  deb-src checked first; then every step of the script, unless 31.1 is installed (the argument
force rebuilds); /usr/local/bin/emacs is set to it in any case; then fullCheck, which is the verdict.
        #+end_org """): return(cmndOutcome)

        self.captureRunStr(""" #+begin_org
#+begin_src sh :results output :session shared
  emacs31SrcBuild-assemble.cs -i fullUpdate
#+end_src
#+RESULTS:
#+begin_example
#+end_example
        #+end_org """)

        force = bool(argsList) and argsList[0] == "force"
        if argsList and not force:
            b_io.eh.problem_usageError(f"unknown argument {argsList[0]}; only: force")
            return failed(cmndOutcome)
        if not _fullUpdate(force):
            return failed(cmndOutcome)
        return cmndOutcome

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
