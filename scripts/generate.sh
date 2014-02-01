#!/bin/bash

startPath=`pwd`

cd ../generate/Abacus

# tabs to spaces
find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \;

# Generate Abacus.cs
sh ../../scripts/t4.sh Abacus.tt -o ../../source/Abacus.cs

cd $startPath

cd ../generate/Abacus.Tests

# tabs to spaces
find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \;

# Generate Tests.cs
sh ../../scripts/t4.sh  Abacus.Tests.tt -o ../../source/Abacus.Tests.cs

cd $startPath