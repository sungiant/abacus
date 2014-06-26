#!/bin/bash

startPath=`pwd`

cd ../

find . -name .DS_Store -exec rm -rf {} \;

cd generate/Abacus

# tabs to spaces
find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \;

# Generate Abacus.cs
mono ../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Single.tt -o ../../source/Abacus.Single.cs
mono ../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Double.tt -o ../../source/Abacus.Double.cs
mono ../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Fixed32.tt -o ../../source/Abacus.Fixed32.cs

cd $startPath

cd ../generate/Abacus.Tests

# tabs to spaces
find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \;

# Generate Tests.cs
mono ../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Single.Tests.tt -o ../../source/Abacus.Single.Tests.cs
mono ../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Double.Tests.tt -o ../../source/Abacus.Double.Tests.cs
mono ../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Fixed32.Tests.tt -o ../../source/Abacus.Fixed32.Tests.cs

cd $startPath
