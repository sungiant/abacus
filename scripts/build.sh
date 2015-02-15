#!/bin/bash

startPath=`pwd`

cd ../build/xamarin_studio.mono40/Abacus.Tests/

xbuild "Abacus.Tests (xamarin_studio.mono40).csproj" /p:Configuration=Debug

cd $startPath
