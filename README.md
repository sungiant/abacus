# Abacus

[![Build Status](https://img.shields.io/travis/sungiant/abacus)][status]
[![License](https://img.shields.io/github/license/sungiant/abacus)][mit]
[![Nuget Version](https://img.shields.io/nuget/v/Abacus)][nuget]
[![Nuget Downloads](https://img.shields.io/nuget/dt/Abacus)][nuget]

## Overview

Abacus is a zero-dependency cross-precision 3D maths library for .NET, Mono and .NET Core.  Abacus provides implementations for 32 bit and 64 bit [fixed point Q numbers][qnumbers] and provides common 3D maths types consistently across both fixed and floating point precisions.

The rationale around building Abacus is rooted in the development of deterministic game simulations.

Documentation for Abacus can be found [here][docs].

## Supported types

Abacus provides implementations of the following common 3D maths data types:

* `Vector2`
* `Vector3`
* `Vector4`
* `Matrix44`
* `Quaternion`

consistently across the following precisions:

* `Single` (`float`)
* `Double` (`double`)
* `Fixed32` (`Q16.16`)
* `Fixed32` (`Q40.24`)


## Getting started

Abacus is available as a stand-alone library via [nuget][nuget].

Alternatively, all code associated with each supported level of precision is generated into a single C# source file - this makes it easy to simply copy the appropriate [Abacus.###.cs][sources] file.

## Example usage

Abacus makes it easy to switch/test the precision used by a block of code as the API is consitent across all supported precisions - simply use the appropriate namespace:

```cs
//using Abacus.SinglePrecision;
//using Abacus.DoublePrecision;
using Abacus.Fixed32Precision; // selected precision
//using Abacus.Fixed64Precision;

class Program {
    public static void Main (string[] args) {
        var a = new Vector2 (-100, +50);
        var b = new Vector2 (+90, -70);
        var c = new Vector2 (-20, +5);
        var d = new Vector2 (+30, -5);
        Vector2 result = Vector2.CatmullRom (a, b, c, d, 0.4);
    }
}

```

## Details

Abacus has been built using [T4 text templatization][t4] as there is limited support for generic type constraint expression in C#. 

## Contributing

If you find a bug or have an issue please reach out via the GitHub Issue tracking system.  Feel feel to submit PRs if you've have an improvements.

## License

Abacus is licensed under the **[MIT License][mit]**; you may not use this software except in compliance with the License.

[mit]: https://raw.githubusercontent.com/sungiant/abacus/master/LICENSE
[nuget]: https://www.nuget.org/packages/Abacus/
[sources]: https://github.com/sungiant/abacus/tree/master/source/abacus/src/main
[status]: https://travis-ci.org/sungiant/abacus
[docs]: http://sungiant.github.io/abacus/annotated.html
[qnumbers]: https://en.wikipedia.org/wiki/Q_(number_format)
[t4]: https://en.wikipedia.org/wiki/Text_Template_Transformation_Toolkit

