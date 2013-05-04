#!/bin/bash

startPath=`pwd`

# Generate Abacus.cs
cd ../generate/source
mono-t4 Abacus.tt -o ../../source/Abacus.cs
cd $startPath

# Generate Tests.cs
cd ../generate/tests
mono-t4 Tests.tt -o ../../source/Tests.cs
cd $startPath