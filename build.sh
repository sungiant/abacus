#!/bin/bash

set -x #echo on

rm -rf bin/
mkdir bin/

cp packages/NUnit.2.6.4/lib/nunit.framework.dll bin/

mcs \
-unsafe \
-debug \
-define:DEBUG \
-define:VARIANTS_ENABLED \
-out:bin/abacus.dll \
-target:library \
-recurse:source/abacus/src/main/cs/*.cs \
-lib:bin/

mcs \
-unsafe \
-debug \
-define:DEBUG \
-out:bin/abacus.test.dll \
-target:library \
-recurse:source/abacus/src/test/cs/*.cs \
-lib:bin/ \
-lib:packages/NUnit.2.6.4/lib \
-reference:abacus.dll \
-reference:nunit.framework.dll
