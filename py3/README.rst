==========================================================================================
bisos.capSpecs: CBM Facilities for BISOS Capabilities Implemented Outside bisos.capability
==========================================================================================


.. contents::
   :depth: 3
..

Overview
========

*bisos.capSpecs* holds the CBM (Capability Bundle Materialization)
facilities of BISOS capabilities that are not implemented in a package
of their own, but that reside in BISOS and need to be materialized on a
platform.

The CBM machinery itself (the ``cbmProc.spcs`` leaves, the control and
status gate, the ``cbm_*`` verbs and ``sysCbmManage.cs``) is in
`bisos.capability <https://github.com/bisos-pip/capability>`__. A
package that implements a capability (airflow, kafka, grafana, …) ships
its own CBS (Capability Bundle Specification). What is left over are
capabilities that nobody implements as a package: they are made of
existing BISOS parts, git repos, pip packages and platform steps.
bisos.capSpecs is where their specifications — CBSs, sboms and assemble
steps — live, so that a CBM leaf can point at them.

The first of these is the **developer platform**: turning a freshly
installed rawBisos platform, which is all read-only repos and non-local
pip packages, into one where the repos are writable and the pip packages
are installed from their local repos. That is ``pltfrmDevelBecome``,
today a hand-run script (``pltfrmDevelBecome.sh``) that predates sboms
and gitist.

Package Documentation At Github
===============================

The information below is a subset of the full of documentation for this
bisos-pip package. More complete documentation is available at:
https://github.com/bisos-pip/capSpecs

.. _table-of-contents:

Table of Contents TOC
=====================

-  `Overview <#overview>`__
-  `Package Documentation At
   Github <#package-documentation-at-github>`__
-  `Part of BISOS — ByStar Internet Services Operating
   System <#part-of-bisos-----bystar-internet-services-operating-system>`__
-  `bisos.capSpecs is a Command-Only PyCS
   Facility <#bisoscapspecs-is-a-command-only-pycs-facility>`__
-  `Capabilities Specified Here <#capabilities-specified-here>`__

   -  `pltfrmDevelCreds <#pltfrmdevelcreds>`__
   -  `pltfrmDevelBecome <#pltfrmdevelbecome>`__
   -  `pltfrmDevelUnBecome <#pltfrmdevelunbecome>`__
   -  `emacsSrcBuild
      (emacs31SrcBuild) <#emacssrcbuild-emacs31srcbuild>`__
   -  `bleeBuild (blee31Build) <#bleebuild-blee31build>`__

-  `How a capSpecs Capability Is
   Materialized <#how-a-capspecs-capability-is-materialized>`__
-  `Installation <#installation>`__
-  `Key Files <#key-files>`__
-  `Documentation and Blee-Panels <#documentation-and-blee-panels>`__
-  `Support <#support>`__
-  `Planned Improvements <#planned-improvements>`__

Part of BISOS — ByStar Internet Services Operating System
=========================================================

Layered on top of Debian, **BISOS** (By\* Internet Services Operating
System) is a unified and universal framework for developing both
internet services and software-service continuums that use internet
services. See `Bootstrapping ByStar, BISOS and
Blee <https://github.com/bxGenesis/start>`__ for information about
getting started with BISOS.

**BISOS** is a foundation for **The Libre-Halaal ByStar Digital
Ecosystem** which is described as a cure for losses of autonomy and
privacy in a book titled: `Nature of
Polyexistentials <https://github.com/bxplpc/120033>`__

*bisos.capSpecs* is part of BISOS. It builds on
`bisos.capability <https://github.com/bisos-pip/capability>`__, and is
meant to be used on a BISOS platform.

bisos.capSpecs is a Command-Only PyCS Facility
==============================================

bisos.capSpecs is a set of command-line tools. They are PyCS command
services. PyCS is a framework that converges development of CLI tools
and services. PyCS is an alternative to FastAPI, Typer and Click.

bisos.capSpecs uses the PyCS-Framework to:

#. Specify capabilities that are not implemented as packages of their
   own (CBSs).
#. Materialize those capabilities on a platform, idempotently, through
   ``bisos.capability``.
#. Report what was materialized, and what failed.

The core of PyCS-Framework is the *bisos.b* package (the
PyCS-Foundation). See `bisos.b <https://github.com/bisos-pip/b>`__ for
an overview.

Capabilities Specified Here
===========================

All of these capabilities are of capabilityType ``induct`` (non-service
software induction), in the ``collective`` realm, so their CBM leaves
are planted under ``sys/cbm/collective/induct/``.

pltfrmDevelCreds
----------------

Prepares the credentials that the developer platform needs: the ssh Host
alias for the git server (for example ``mb2_github`` in
``~/.ssh/config``) and a check that it works. It is separate from
pltfrmDevelBecome, and it is a prerequisite of it. *Planned: not yet
implemented.*

pltfrmDevelBecome
-----------------

After a rawBisos install, and with the credentials in place, becomes a
developer platform:

#. Repos: ``anon`` to ``auth``. All repos under
   ``/bisos/git/auth/bxRepos`` (the ``bx-gitReposBases`` base, and the
   repos that ``bxRepos-gitist.pcs`` adds), and the
   ``/bisos/git/bxRepos`` symlink pointing there.
#. Pip: PyPI to local. The ``dev-bisos3`` virtual environment, with
   every ``bisos-pip/*/py3`` package installed editable, and the
   packages of ``rawBisos-sbom.pcs`` present.
#. Verification: ``from bisos import b`` works in ``dev-bisos3``,
   ``rawBisos-sbom.pcs`` runs, and every package that should be editable
   is.

It is the slow, idempotent build, and each step skips what is already
done. The files are ``pltfrmDevelBecome-cbs.pcs``,
``pltfrmDevelBecome-sbom.pcs`` (no packages),
``pltfrmDevelBecome-assemble.cs`` and the leaf pointer
``cbmProc-pltfrmDevelBecome.spcs``. The leaf is
``sys/cbm/collective/induct/pltfrmDevelBecome/``, planted ``available``:
enable it with ``cbm_enable``.

.. code:: bash

   pltfrmDevelBecome-assemble.cs -i fullCheck      # read-only, offline: one summary table
   pltfrmDevelBecome-assemble.cs -i gitAliasCheck  # the ssh alias authenticates (network)
   pltfrmDevelBecome-assemble.cs -i fullUpdate     # the steps below, then fullCheck

+-------------------------+-------------------------------------------+
| Command                 | Step                                      |
+=========================+===========================================+
| ``gitAliasCheck``       | The precondition: the ssh Host alias      |
|                         | resolves and authenticates                |
+-------------------------+-------------------------------------------+
| ``gitReposCheck``       | Read-only: every repo cloned in ``auth``  |
|                         | and writable (origin ``git@<alias>:``)    |
+-------------------------+-------------------------------------------+
| ``gitAnonToAuth``       | ``pbdUpdate`` and the gitist clones, then |
|                         | the check                                 |
+-------------------------+-------------------------------------------+
| ``gitSymlinkSwitch``    | ``/bisos/git/bxRepos`` to ``auth``;       |
|                         | refuses unless the repos are complete     |
+-------------------------+-------------------------------------------+
| ``pipCheck``            | Read-only: every package editable from    |
|                         | ``auth``, and ``from bisos import b``     |
+-------------------------+-------------------------------------------+
| ``pipVenvEnsure``       | Create ``dev-bisos3`` when absent         |
+-------------------------+-------------------------------------------+
| ``pipEditableInstalls`` | ``pkgReInstall edit`` for each package    |
|                         | not yet editable                          |
+-------------------------+-------------------------------------------+
| ``rawBisosPipCheck``    | Read-only: the ``pipPkgs_list`` of        |
|                         | ``rawBisos-sbom.pcs`` is in               |
|                         | ``dev-bisos3``                            |
+-------------------------+-------------------------------------------+
| ``rawBisosPipInstalls`` | PyPI install of an entry that has no      |
|                         | clone (never runs the sbom)               |
+-------------------------+-------------------------------------------+
| ``fullCheck``           | The summary of all of the above, offline  |
+-------------------------+-------------------------------------------+
| ``fullUpdate``          | The steps, then ``fullCheck``             |
+-------------------------+-------------------------------------------+

The alias is a parameter: an argument of ``gitAliasCheck``, else
``PLTFRM_DEVEL_GIT_ALIAS``, else ``mb2_github``. The four packages that
do not install editable (``lcnt``, ``pycs``, ``qmailSvc``, ``gnome``)
are skipped and reported. The verdict is the text
``pltfrmDevelBecome: complete`` or ``NOT complete, failed: ...``:
``g_csMain`` exits 0 on failure, so do not trust the exit code. *Not yet
run on a rawBisos VM.*

pltfrmDevelUnBecome
-------------------

A fast switch between anon mode and devel mode on a platform that has
become a developer platform. It installs and removes nothing: the
``dev-bisos3`` venv, its ``devbin`` scripts and the ``auth`` clones all
stay. UnBecome switches to anon mode (the ``/bisos/git/bxRepos`` symlink
back to ``anon``, the regular ``bisos3`` venv in use); its own
unMaterialize switches back to devel, after a quick check that the devel
platform is still complete. It is not the unMaterialize of
pltfrmDevelBecome, which is the slow build. *Planned: how the mode
reaches PATH is still open.*

emacsSrcBuild (emacs31SrcBuild)
-------------------------------

Builds Emacs from source and makes it the default ``emacs``. The leaf is
generic, ``sys/cbm/collective/induct/emacsSrcBuild/``; the version is
chosen by the symlink ``emacsSrcBuild-cbs.pcs``, which today points to
``emacs31SrcBuild-cbs.pcs``: the release tag ``emacs-31.1``. Moving to
another version is a new ``emacsNNSrcBuild-*`` set and a repointed
symlink.

The build is that of ``/bisos/blee/bin/lcaEmacsSrcBinsPrep.sh``
(tree-sitter, AOT native compilation, gtk3), used as it is, one step at
a time. The result is in ``/usr/local/bin`` (``emacs-31``,
``emacsclient-31``), and ``/usr/local/bin/emacs`` points to it, ahead of
Debian's ``/bin/emacs``, which stays. The same on a container, a VM and
a box. Precondition, only checked: ``deb-src`` in the apt sources
(``apt-get build-dep emacs`` needs it); enabling it belongs to the
rawBisos install.

.. code:: bash

   emacs31SrcBuild-assemble.cs -i fullCheck          # read-only: one summary table
   emacs31SrcBuild-assemble.cs -i fullUpdate         # the steps unless 31.1 is installed; then fullCheck
   emacs31SrcBuild-assemble.cs -i fullUpdate force   # rebuild and reinstall
   emacs31SrcBuild-assemble.cs -i stepRun srcBuild   # one step, to resume after a failure

The steps are ``srcEnvSetup``, ``srcPkgObtain`` (skipped when the
sources are there), ``srcBuild``, ``srcBinInstall``, ``postInstall`` and
``setAsDefault``, with one log each in ``/tmp/emacs31SrcBuild/``.
``fullCheck`` requires ``deb-src``, the sources, ``emacs-31`` and
``emacsclient-31`` reporting 31.1, native compilation and tree-sitter
available, ``/usr/local/bin/emacs`` leading to ``emacs-31.1``, and
``/usr/local/bin/emacsclient`` absent or an emacsclient. The verdict is
the text ``emacs31SrcBuild: complete`` or ``NOT complete``. If the clone
fails, ``fullUpdate`` stops; run it again. Materialized on a container
(2026-11-12, about 10 minutes on 96 cores); not yet on a VM or a box.

bleeBuild (blee31Build)
-----------------------

Builds Blee (the ``doom-blee3`` profile) for Emacs 31,
``/usr/local/bin/emacs-31`` (installed by ``emacsSrcBuild``), whether or
not it is the default Emacs. As with ``emacsSrcBuild``, the files are
version-specific (``blee31Build-*``) and the leaf
``sys/cbm/collective/induct/bleeBuild/`` is generic: it points to the
symlink ``bleeBuild-cbs.pcs``, which today points to
``blee31Build-cbs.pcs``. The doom commit is the one pinned for Emacs 31
in ``/bisos/blee/bin/bleeLib.sh``. The build is that of
``/bisos/blee/bin/bleeDoomsManage.sh``, used as it is:
``doomFrameworkPrep`` (the doomemacs clone for Emacs 31, at its pin),
then ``buildInstall`` (``doom install`` into
``/bisos/blee/emacsVers/<version>/doom-run-blee3``; network).
Precondition, only checked: ``emacs-31`` is a ``/usr/local`` build.

It only builds. Each user's chemacs2 ``~/.emacs-profiles.el`` is not
changed, so a running Blee keeps its Emacs; switch with
``blee -p emacs=31 -i run doom-blee3``.

.. code:: bash

   blee31Build-assemble.cs -i fullCheck                    # read-only: one summary table
   blee31Build-assemble.cs -i fullUpdate                   # framework, build unless built, then fullCheck
   blee31Build-assemble.cs -i fullUpdate force             # reBuild: delete the run base, build again
   blee31Build-assemble.cs -i stepRun doomFrameworkPrep    # one step, to resume after a failure

``fullCheck`` requires ``emacs-31`` to be a ``/usr/local`` build, the
framework at its pin, the run base built for that Emacs,
``bin/doom version`` reporting that Emacs and the pin, ``config.el``
being the with-Blee one, and the blee repo's config files unchanged.

How a capSpecs Capability Is Materialized
=========================================

The layering is that of bisos.capability: a CBM sits on top of a CBS; it
does not duplicate it.

#. A ``cbmProc.spcs`` leaf in
   ``sys/cbm/collective/induct/<capabilityName>/`` holds a pointer
   (``capabilityName`` and ``cbsSpec``) to the CBS in this package.
#. The file ``cbmProc.control`` gates it: ``available``, ``enabled`` or
   ``disabled``.
#. ``sysCbmManage.cs -i fullyMaterialize``, or a single leaf's
   ``cbm_materialize``, runs the CBS, which runs its sboms and assemble
   steps.
#. ``cbmProc.status`` records what happened.

Each step has to be idempotent, so that ``fullyMaterialize`` can be
re-run, and has to report its own failure: neither
``pypiProc.sh -i pkgReInstall`` nor the CBM walker reports a failed
install as one.

Installation
============

The sources for the bisos.capSpecs pip package are maintained at:
https://github.com/bisos-pip/capSpecs.

You can install bisos.capSpecs with pip or pipx.

.. code:: bash

   pip install bisos.capSpecs

.. code:: bash

   pipx install bisos.capSpecs

On a developer platform it is installed editable, from this repo, like
all the bisos-pip packages:

.. code:: bash

   cd py3 && ./pypiProc.sh -i pkgReInstall edit /bisos/venv/dev-bisos3

Key Files
=========

-  `file:./bin/pltfrmDevelBecome-assemble.cs <./bin/pltfrmDevelBecome-assemble.cs>`__
   — the steps and checks of the developer platform (``-cbs.pcs``,
   ``-sbom.pcs`` and ``cbmProc-pltfrmDevelBecome.spcs`` are beside it).
-  `file:./bisos/capSpecs/pltfrmDevelBecomeInfo.py <./bisos/capSpecs/pltfrmDevelBecomeInfo.py>`__
   — its pure facts: alias, repo roots, venv, the known failures, and
   the checks.
-  `file:./bin/emacs31SrcBuild-assemble.cs <./bin/emacs31SrcBuild-assemble.cs>`__
   — the steps and the check of the Emacs 31 build (``-cbs.pcs``,
   ``-sbom.pcs``, the symlink ``emacsSrcBuild-cbs.pcs`` and
   ``cbmProc-emacsSrcBuild.spcs`` are beside it); its facts are in
   `file:./bisos/capSpecs/emacs31SrcBuildInfo.py <./bisos/capSpecs/emacs31SrcBuildInfo.py>`__.
-  `file:./bin/blee31Build-assemble.cs <./bin/blee31Build-assemble.cs>`__
   — the steps and the check of the Blee build for Emacs 31
   (``-cbs.pcs``, ``-sbom.pcs``, the symlink ``bleeBuild-cbs.pcs`` and
   ``cbmProc-bleeBuild.spcs`` are beside it); its facts are in
   `file:./bisos/capSpecs/blee31BuildInfo.py <./bisos/capSpecs/blee31BuildInfo.py>`__.
-  `file:./bin/null-cbs-induct.cs <./bin/null-cbs-induct.cs>`__ — the
   placeholder CBS for an induct capability, to be replaced by one per
   capability.
-  `file:./bin/null-assemble.cs <./bin/null-assemble.cs>`__ — the
   placeholder assemble step.
-  `file:./bin/null-binsPrep.cs <./bin/null-binsPrep.cs>`__ — the
   placeholder binsPrep step.
-  `py3\ bisos/capSpecs <./bisos/capSpecs/>`__ — the Python modules of
   the CBSs and their sboms.
-  `file:./AI-WorkPlan.org <./AI-WorkPlan.org>`__ — the stages that
   drive this package.

Documentation and Blee-Panels
=============================

bisos.capSpecs is part of ByStar Digital Ecosystem
http://www.by-star.net.

This module's primary documentation is in the form of Blee-Panels. They
are in the ``./panels`` directory. From within Blee and BISOS these
panels are accessible under the Blee "Panels" menu.

See
`file:./panels/bisos.capSpecs/\_nodeBase\_/fullUsagePanel-en.org <./panels/bisos.capSpecs/_nodeBase_/fullUsagePanel-en.org>`__
for a starting point.

Support
=======

| For support, criticism, comments and questions; please contact the
  author/maintainer
| `Mohsen Banan <http://mohsen.1.banan.byname.net>`__ at:
  http://mohsen.1.banan.byname.net/contact

Planned Improvements
====================

-  TODO pltfrmDevelCreds: specify and implement the credentials CBM.
-  TODO pltfrmDevelBecome: run it on a rawBisos VM, enable the leaf, and
   settle the known failures.
-  TODO pltfrmDevelUnBecome: specify and implement the fast anon/devel
   switch.
-  TODO Replace the ``null-*`` placeholders with real CBSs, sboms and
   assemble steps.
-  TODO Add further capabilities that live in BISOS but have no package
   of their own.
