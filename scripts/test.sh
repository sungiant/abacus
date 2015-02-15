#!/bin/bash

startPath=`pwd`

mono ../libs/NUnit-2.6.3/bin/nunit-console.exe ../build/xamarin_studio.mono40/Abacus.Tests/bin/Debug/Abacus.Tests.dll

cd $startPath
