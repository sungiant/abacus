## Abacus

[![Build Status](https://travis-ci.org/sungiant/abacus.png?branch=master)](https://travis-ci.org/sungiant/abacus)
[![Gitter](https://img.shields.io/badge/gitter-join%20chat-green.svg)](https://gitter.im/sungiant/abacus?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://raw.githubusercontent.com/sungiant/abacus/master/LICENSE)
[![Nuget Version](https://img.shields.io/nuget/v/Abacus.svg)](https://www.nuget.org/packages/Abacus)
[![Nuget Downloads](https://img.shields.io/nuget/dt/Abacus.svg)](https://www.nuget.org/packages/Abacus)

## Overview

Abacus is a 3D maths library for .NET and Mono, built with a primary focus on performance and efficiency.  Abacus is ideal for use developing real-time 3D applications and deterministic network simulations.

Abacus provides the following data types:

* `Vector2`
* `Vector3`
* `Vector4`
* `Matrix`
* `Quaternion`

consistently across the following precisions:

* `Single`
* `Double`
* `Fixed32`

## Getting Started

Abacus is available as a stand-alone library via **[nuget][abacus_nuget]**.  Here's an example nuget `packages.config` file that pulls in Abacus:

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Abacus" version="0.9.2" targetFramework="net45" />
</packages>
```

Alternatively, given that each supported precision level generates a single source file, it is easy to simply copy the [Abacus.*.cs][sources] file for the precision you need straight into your project.


## Why?

Why not?

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
