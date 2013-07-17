#!/bin/bash

startPath=`pwd`

# Generate Abacus.cs
cd ../generate/Abacus
mono-t4 Abacus.tt -o ../../source/Abacus.cs
cd $startPath

# Generate Tests.cs
cd ../generate/Abacus.Tests
mono-t4 Abacus.Tests.tt -o ../../source/Abacus.Tests.cs
cd $startPath