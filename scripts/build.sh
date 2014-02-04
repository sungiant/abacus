#!/bin/bash

startPath=`pwd`

cd ../build_examples/xs.mono40/Abacus/

xbuild "Abacus (xs.mono40).csproj" /p:Configuration=Debug && exit 1
xbuild "Abacus (xs.mono40).csproj" /p:Configuration=Release && exit 1

cd $startPath
