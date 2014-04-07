Build Examples
==============

This folder contains EXAMPLE build configurations for Abacus and its Tests for a varity of targets.  The best option, in almost every scenario will be to create your own build configuration for your specific environment, using the examples as reference.  Another option is to simply copy Abacus.cs into your codebase.

Example Targets
===============

* Visual Studio (.NET 4.0)
* Visual Studio (XNA 4: x86)
* Xamarin Studio (Mono 4.0)
* Xamarin Studio (Xamarin iOS)
* Xamarin Studio (Xamarin Android)
* Xamarin Studio (MonoMac/XamarinMac)
* PlayStation Mobile Studio (Mono 4.0)
* MS Build (.NET 4.0)

Text Templates
==============

Visual Studio provides good integration with t4 Text Templates, however it doesn't work well with the Abacus project structure:

http://stackoverflow.com/questions/21352411/custom-output-file-path-for-t4-template

If you want to regenerate the cs files use the generate scripts:

Windows:
    cd scripts/
    generate.bat

Posix:
    cd scripts/
    ./generate.sh
