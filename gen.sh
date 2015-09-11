#!/bin/bash

set -x #echo on

startPath=`pwd`

find . -name .DS_Store -exec rm -rf {} \;

cd source/abacus/gen/main

# tabs to spaces
find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \;

rm -rf ../../src/main/cs/*

# Generate Abacus.cs
mono ../../../../packages/Mono.TextTransform.1.0.0/tools/TextTransform.exe Abacus.Single.tt -o ../../src/main/cs/Abacus.Single.cs
mono ../../../../packages/Mono.TextTransform.1.0.0/tools/TextTransform.exe Abacus.Double.tt -o ../../src/main/cs/Abacus.Double.cs
mono ../../../../packages/Mono.TextTransform.1.0.0/tools/TextTransform.exe Abacus.Fixed32.tt -o ../../src/main/cs/Abacus.Fixed32.cs

cd $startPath

cd source/abacus/gen/test

# tabs to spaces
find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \;

rm -rf ../../src/test/cs/*

# Generate Tests.cs
mono ../../../../packages/Mono.TextTransform.1.0.0/tools/TextTransform.exe Abacus.Single.Tests.tt -o ../../src/test/cs/Abacus.Single.Tests.cs
mono ../../../../packages/Mono.TextTransform.1.0.0/tools/TextTransform.exe Abacus.Double.Tests.tt -o ../../src/test/cs/Abacus.Double.Tests.cs
#mono ../../../../packages/Mono.TextTransform.1.0.0/tools/TextTransform.exe Abacus.Fixed32.Tests.tt -o ../../src/test/cs/Abacus.Fixed32.Tests.cs

cd $startPath
