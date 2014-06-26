cd ../generate/

:: Abacus
:: ------
cd Abacus/
TextTransform.exe Abacus.Single.tt -o ../../source/Abacus.Single.cs -r System.Core.dll
TextTransform.exe Abacus.Double.tt -o ../../source/Abacus.Double.cs -r System.Core.dll
TextTransform.exe Abacus.Fixed32.tt -o ../../source/Abacus.Fixed32.cs -r System.Core.dll
cd ../

:: Abacus.Tests
:: ------------
cd Abacus.Tests/
TextTransform.exe Abacus.Single.Tests.tt -o ../../source/Abacus.Single.Tests.cs -r System.Core.dll
TextTransform.exe Abacus.Double.Tests.tt -o ../../source/Abacus.Double.Tests.cs -r System.Core.dll
TextTransform.exe Abacus.Fixed32.Tests.tt -o ../../source/Abacus.Fixed32.Tests.cs -r System.Core.dll
cd ../

cd ../
