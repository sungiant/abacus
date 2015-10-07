#!/bin/bash

set -x #echo on

mono packages/NUnit.Runners.*/tools/nunit-console.exe bin/abacus.test.dll
