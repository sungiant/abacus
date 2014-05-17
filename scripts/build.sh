#!/bin/bash

startPath=`pwd`

cd ../build/xs.mono40/Abacus.Tests/

xbuild "Abacus.Tests (xs.mono40).csproj" /p:Configuration=Debug

cd $startPath
