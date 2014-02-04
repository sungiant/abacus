#!/bin/bash

startPath=`pwd`

mono ../libs/NUnit-2.6.3/bin/nunit-console.exe ../build_examples/xs.mono40/Abacus/bin/Debug/Abacus.dll

cd $startPath
