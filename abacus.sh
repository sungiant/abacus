#!/bin/bash

set -e

if [ $# -eq 0 ]; then
    echo "No arguments provided, options are:"
    echo " --nu    : Initilise Nuget dependencies."
    echo " --clean : Remove generated code."
    echo " --gen   : Run T4 code generation."
    echo " --build : Compile generated code."
    echo " --test  : Run tests for compiled code."
    echo " --all   : Runs all of the above."
    echo " --docs  : Generate Doxygen docs (CI Only)."
fi

while test $# -gt 0
do
    case "$1" in
        --nu)
            task_nu=1
            ;;
        --clean)
            task_clean=1
            ;;
        --gen)
            task_gen=1
            ;;
        --build)
            task_build=1
            ;;
        --test)
            task_test=1
            ;;
        --docs)
            task_docs=1
            ;;
        --all)
            task_nu=1
            task_clean=1
            task_gen=1
            task_build=1
            task_test=1
            ;;
    esac
    shift
done

if [ "$task_nu" == 1 ]; then
    echo "Initilising Nuget dependencies."
    set -x
    rm -rf packages/
    nuget install -o packages/
    set +x
fi

if [ "$task_clean" == 1 ]; then
    echo "Removing generated code"
    set -x
    rm -rf ../../src/main/*
    rm -rf ../../src/test/*
    set +x
fi

if [ "$task_gen" == 1 ]; then
    echo "Running T4 code generation."
    set -x
    startPath=`pwd`
    find . -name .DS_Store -exec rm -rf {} \;
    cd source/abacus/gen/main
    find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \; # tabs to spaces
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Single.tt -o ../../src/main/Abacus.Single.cs
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Double.tt -o ../../src/main/Abacus.Double.cs
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Fixed32.tt -o ../../src/main/Abacus.Fixed32.cs
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Fixed64.tt -o ../../src/main/Abacus.Fixed64.cs
    cd $startPath
    cd source/abacus/gen/test
    find ./ ! -type d ! -name _tmp_ -exec sh -c 'expand -t 4 {} > _tmp_ && mv _tmp_ {}' \; # tabs to spaces
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Single.Tests.tt -o ../../src/test/Abacus.Single.Tests.cs
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Double.Tests.tt -o ../../src/test/Abacus.Double.Tests.cs
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Fixed32.Tests.tt -o ../../src/test/Abacus.Fixed32.Tests.cs
    mono ../../../../packages/Mono.TextTransform.*/tools/TextTransform.exe -r 'System.Core' Abacus.Fixed64.Tests.tt -o ../../src/test/Abacus.Fixed64.Tests.cs
    cd $startPath
    set +x
fi

if [ "$task_build" == 1 ]; then
    echo "Compiling generated code."
    set -x
    rm -rf bin/
    mkdir bin/
    cp packages/NUnit.*/lib/nunit.framework.dll bin/
    mcs \
        -unsafe \
        -define:FUNCTION_VARIANTS \
        -out:bin/abacus.dll \
        -target:library \
        -recurse:source/abacus/src/main/*.cs \
        /doc:bin/abacus.xml \
        -lib:bin/ \
        -reference:System.Numerics.dll
    mcs \
        -unsafe \
        -out:bin/abacus.min.dll \
        -target:library \
        -recurse:source/abacus/src/main/*.cs \
        -lib:bin/ \
        -reference:System.Numerics.dll
    mcs \
        -unsafe \
        -out:bin/abacus.test.dll \
        -target:library \
        -recurse:source/abacus/src/test/*.cs \
        -lib:bin/ \
        -reference:abacus.min.dll \
        -reference:nunit.framework.dll
    set +x
fi

if [ "$task_test" == 1 ]; then
    echo "Running tests for compiled code."
    set -x
    mono packages/NUnit.Runners.*/tools/nunit-console.exe bin/abacus.test.dll
    set +x
fi

if [ "$task_docs" == 1 ]; then
    echo "Generating Doxygen docs."
    set -x
    doxygen doxygen.conf
    ls -lah docs/
    git clone -b gh-pages https://git@$GH_REPO_REF temp
    cd temp
    git config --global push.default simple
    git config user.name "Travis CI"
    git config user.email "travis@travis-ci.org"
    rm -rf *
    echo "" > .nojekyll
    cd ../
    mv docs/html/* temp/
    cd temp
    if [ -n "$(git status --porcelain)" ] && [ -f "index.html" ]; then
        echo 'Uploading documentation to the gh-pages branch...'
        git add --all
        git commit -m "Deploy code docs to GitHub Pages Travis build: ${TRAVIS_BUILD_NUMBER}" -m "Commit: ${TRAVIS_COMMIT}"
        git push --force "https://${GH_REPO_TOKEN}@${GH_REPO_REF}" > /dev/null 2>&1
    else
        echo '' >&2
        echo 'Warning: No documentation (html) files have been found!' >&2
        echo 'Warning: Not going to push the documentation to GitHub!' >&2
        exit 1
    fi
    set +x
fi

exit 0