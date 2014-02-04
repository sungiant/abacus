#!/bin/bash

startPath=`pwd`

cd ../build_examples/xs.mono40/Abacus/

xbuild "Abacus (xs.mono40).csproj" /p:Configuration=Debug
xbuild "Abacus (xs.mono40).csproj" /p:Configuration=Release

cd $startPath
