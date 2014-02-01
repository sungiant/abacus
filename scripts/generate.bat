cd ../generate/

:: Abacus
:: ------
cd Abacus/
TextTransform.exe Abacus.tt -o ../../source/Abacus.cs -r System.Core.dll
cd ../

:: Abacus.Tests
:: ------------
cd Abacus.Tests/
TextTransform.exe Abacus.Tests.tt -o ../../source/Abacus.Tests.cs -r System.Core.dll
cd ../

cd ../