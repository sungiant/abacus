
:: Abacus
:: ------
cd generate/Abacus/
../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Single.tt -o ../../source/Abacus.Single.cs -r System.Core.dll
../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Double.tt -o ../../source/Abacus.Double.cs -r System.Core.dll
../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Fixed32.tt -o ../../source/Abacus.Fixed32.cs -r System.Core.dll
cd ../../

:: Abacus.Tests
:: ------------
cd generate/Abacus.Tests/
../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Single.Tests.tt -o ../../source/Abacus.Single.Tests.cs -r System.Core.dll
../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Double.Tests.tt -o ../../source/Abacus.Double.Tests.cs -r System.Core.dll
../../libs/MonoDevelop.TextTemplating/TextTransform.exe Abacus.Fixed32.Tests.tt -o ../../source/Abacus.Fixed32.Tests.cs -r System.Core.dll
cd ../../

