![Abacus](/misc/logo.png)

##Project Status: Pre-Alpha [![Build Status](https://travis-ci.org/abacus3D/abacus.png?branch=development)](https://travis-ci.org/abacus3D/abacus) [![Coverage Status](https://coveralls.io/repos/abacus3D/abacus/badge.png)](https://coveralls.io/r/abacus3D/abacus) [![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/abacus3D/abacus/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

Abacus is a 3D maths library for the [CLI](http://en.wikipedia.org/wiki/Common_Language_Infrastructure), built with a primary focus on performance and efficiency.  Abacus is ideal for use developing real-time 3D applications and deterministic network simulations.

Abacus supports similar features to the mathematical portions of the XNA framework, however, Abacus is written to support more than just single precision floating point calculations.  Abacus currently supports Single, Double and Fixed32 precision real number calculations.

![Abacus](/misc/bead1.png) **Project Status**

Right now Abacus is in development, currently the focus of the project is to reach 100% test coverage across all of the libary's features.  The next goal will be to add missing types (Rays, Planes) and add more precision types (Fixed64 and Fixed128).  When testing is complete and all types are in the first Alpha build will be tagged and released.

![Abacus](/misc/bead2.png) **Using Abacus**

The source code for Abacus can be easily included in any C# project, simply copy and compile [Abacus.cs](https://raw.github.com/abacus3D/abacus/development/source/Abacus.cs) along with the rest of you code.

![Abacus](/misc/bead3.png) **Contributing**

The project structure is unusual in that all of the source code is generated using the T4 Text Templating engine.  Do not directly modify any of the files in the source folder, instead modify those found in the generate folder, and then run the generate script to regenerate the source.

*Workflow:*

* Modify the template code in the /generate folder using the text editor of your choice.  The code in the /generate folder is authoratative.

* Once templates have been modified they need to be regenerated
