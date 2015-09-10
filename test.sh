#!/bin/bash

set -x #echo on

mono packages/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/abacus.test.dll
