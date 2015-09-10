#!/usr/bin/python

import os, shutil, subprocess, sys


# If on windows you must have the following in your PATH in both the user and system variables:
# C:\python27\;C:\Program Files (x86)\Mono\bin
# Also build with Windows Powershell, not the Command Prompt.

print "OS:" + sys.platform

v_build_platform_api_monomac_app = False
v_build_platform_api_xamarin_ios_app = False
v_build_platform_api_opentk_game = True
v_build_platform_api_wpf_xna4_control = False
v_build_platform_api_wpf_opentk_control = False
v_build_platform_api_xna4_game = False

c_osx_xamarin_monomac_path = '/Applications/Xamarin Studio.app/Contents/Resources/lib/MonoDevelop/AddIns/MonoDevelop.MonoMac/'
c_osx_xamarin_ios_path = '/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/2.1/'

if sys.platform == 'darwin':
  if os.path.exists (c_osx_xamarin_monomac_path) == False:
    raise Exception('The MonoMac SDK must be installed.  Expected to find it here:\n' + c_osx_xamarin_monomac_path)
  elif os.path.exists (c_osx_xamarin_ios_path) == False:
    raise Exception('The Xamarin iOS SDK must be installed.  Expected to find it here:\n' + c_osx_xamarin_ios_path)
  else:
    v_build_platform_api_monomac_app = True
    v_build_platform_api_xamarin_ios_app = True


class bcolors:
    if sys.platform == 'win32':
      HEADER = ''
      OKBLUE = ''
      OKGREEN = ''
      WARNING = ''
      FAIL = ''
      ENDC = ''
      BOLD = ''
      UNDERLINE = ''
    else:
      HEADER = '\033[95m'
      OKBLUE = '\033[94m'
      OKGREEN = '\033[92m'
      WARNING = '\033[93m'
      FAIL = '\033[91m'
      ENDC = '\033[0m'
      BOLD = '\033[1m'
      UNDERLINE = '\033[4m'

class Project:
  pass

abacus = Project ()
abacus.out = 'abacus'
abacus.target = 'library'
abacus.path = 'source/abacus/src/main/cs/'
abacus.defines = []
abacus.references = []
abacus.additional_references = []
abacus.additional_search_paths = []
abacus.additional_sources = []


abacus_test = Project ()
abacus_test.out = 'abacus.test'
abacus_test.target = 'library'
abacus_test.path = 'source/abacus/src/test/cs/'
abacus_test.defines = []
abacus_test.references = [abacus]
abacus_test.additional_references = ['nunit.framework']
abacus_test.additional_search_paths = ['packages/NUnit.2.6.4/lib']
abacus_test.additional_sources = []

################################################################################

projects = [abacus, abacus_test]

################################################################################


try:
  shutil.rmtree('bin')
except:
  pass

os.mkdir ('bin')

fail_count = 0

shutil.copyfile('packages/NUnit.2.6.4/lib/nunit.framework.dll', 'bin/nunit.framework.dll')

for project in projects:
  print ''

  if project.target == 'exe':
    _output = project.out + '.exe'
  else:
    _output = project.out + '.dll'

  print 'COMPILING: '+ _output

  _out = '-out:bin/' + _output
  _target = '-target:' + project.target
  _recurse = '-recurse:' + project.path + '*.cs'

  _lib = ['-lib:bin/']
  _lib.extend (map (lambda x: '-lib:' + x, project.additional_search_paths))
  _define = []
  _reference = []

  if len (project.defines) > 0:
    _define = map (lambda x: '-define:' + x, project.defines)
  if len (project.references) > 0:
    _reference.extend (map (lambda x: '-reference:' + x.out + '.dll', project.references))
  if len (project.additional_references) > 0:
    _reference.extend (map (lambda x: '-reference:' + x + '.dll', project.additional_references))

  if sys.platform == 'win32':
    _cmd = ['mcs.bat']
  else:
    _cmd = ['mcs']

  _cmd.append('-unsafe')
  _cmd.append('-debug')
  _cmd.append('-define:DEBUG')
  _cmd.append(_out)
  _cmd.append(_target)
  _cmd.append(_recurse)
  _cmd.extend(map (lambda x: '-recurse:' + x, project.additional_sources))
  _cmd.extend(_lib)
  _cmd.extend(_define)
  _cmd.extend(_reference)

  print " ".join (_cmd )

  _ret = subprocess.call (_cmd)

  if _ret == 0:
    print bcolors.OKGREEN + 'SUCCESS' + bcolors.ENDC
  else:
    print bcolors.FAIL + 'FAILURE' + bcolors.ENDC
    fail_count = fail_count + 1

print ''
print '--------------------------------------'
print ''

if fail_count == 0:
  print bcolors.OKGREEN + 'BUILD SUCCEEDED' + bcolors.ENDC
else:
  print bcolors.FAIL + 'BUILD FAILED: ' + str(fail_count) + '/' + str (len (projects)) + bcolors.ENDC

sys.exit (fail_count)

