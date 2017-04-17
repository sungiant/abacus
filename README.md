## Abacus

[![Join the chat at https://gitter.im/sungiant/abacus](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/sungiant/abacus?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![Build Status](https://travis-ci.org/sungiant/abacus.png?branch=master)](https://travis-ci.org/sungiant/abacus)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://raw.githubusercontent.com/sungiant/abacus/master/LICENSE)
[![Nuget Version](https://img.shields.io/nuget/v/Abacus.svg)](https://www.nuget.org/packages/Abacus)


## Overview

Abacus is a 3D maths library for .NET and Mono, built with a primary focus on performance and efficiency.  Abacus is ideal for use developing real-time 3D applications and deterministic network simulations.

## Why?

Remember [XNA][xna]?  It was a legendary framework for building games and 3D applications; part of the framework consisted of an excellent little maths library for use with floating point numbers.

On a number of occasions I found myself wanting to use that XNA maths library, perhaps I was solving a challenge on [HackerRank][hacker], writing a software ray tracer, or building a network simulation, however XNA had a number of dependencies and more often than not I wanted to work with something other than 32 bit floating point numbers.  Abacus aims to provide all of the cool features of the XNA maths library across multiple levels of precision; all in a single stand-alone package.

Not only does Abacus consistently support mathematical operations and data types for `float` and `double` precision numbers, but it also provides it's own implementations of various fixed point number formats too, consistently supports mathematical operations and data types for them also.

## So, what types are actually supported?

Abacus provides implementations of the following data types:

* `Vector2`
* `Vector3`
* `Vector4`
* `Matrix44`
* `Quaternion`

consistently across the following precisions:

* `Single` (`float`)
* `Double` (`double`)
* `Fixed32` (under test)
* `Fixed64` (coming soon)
* `Fixed128` (coming soon)
* `Half` (coming soon)

## About the name

On face value "Abacus" seems like a pretty obvious choice for a name of a maths library, however the roots of the name run a little deeper; Abacus is, in point of fact, named after a maths library I worked with in the past when I was a programmer at [Black Rock Studio][br].  The name is especially appropriate given that this Abacus is fairly similar to the maths library in the XNA framework, which in-turn was fairly similar to Black Rock's Abacus; all of which stands to reason given that both XNA maths and Black Rock Abacus were partially written by [Shawn Hargreaves][sh].

## Getting started

Abacus is available as a stand-alone library via [nuget][abacus_nuget].  Here's an example nuget `packages.config` file that pulls in Abacus:

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Abacus" version="0.9.2" targetFramework="net45" />
</packages>
```

Alternatively, given that each supported precision level generates a single source file, it is easy to simply copy the [Abacus.*.cs][sources] file for the precision you need straight into your project.

## Example usage

```cs
/**
 * Choose which level of precision to use simply by
 * commenting out all but the appropriate using
 * statement:
 */

// using Abacus.SinglePrecision;
using Abacus.DoublePrecision;
// using Abacus.Fixed32Precision;

class Program
{
    public static void Main (string[] args)
    {
        var a = new Vector2 (-100, +50);
        var b = new Vector2 (+90, -70);

        var c = new Vector2 (-20, +5);
        var d = new Vector2 (+30, -5);
        
        Vector2 result;
        double amount = 0.4;
        
        Vector2.CatmullRom (ref a, ref b, ref c, ref d, ref amount, out result);
        
        System.Console.WriteLine ("result: " + result);
    }
}

```

## License

Abacus is licensed under the **[MIT License][mit]**; you may not use this software except in compliance with the License.

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

[mit]: https://raw.githubusercontent.com/sungiant/abacus/master/LICENSE
[abacus_nuget]: https://www.nuget.org/packages/Abacus/
[sources]: https://github.com/sungiant/abacus/tree/master/source/abacus/src/main/cs
[xna]: https://en.wikipedia.org/wiki/Microsoft_XNA
[hacker]: https://www.hackerrank.com
[sh]: http://www.shawnhargreaves.com
[br]: https://en.wikipedia.org/wiki/Black_Rock_Studio
