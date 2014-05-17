#!/bin/bash

startPath=`pwd`

mono ../libs/NUnit-2.6.3/bin/nunit-console.exe ../build/xs.mono40/Abacus.Tests/bin/Debug/Abacus.Tests.dll

cd $startPath
