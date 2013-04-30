// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │ Abacus - Fast, efficient, cross precision, maths library               │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Brought to you by:                                                     │ \\
// │          _________                    .__               __             │ \\
// │         /   _____/__ __  ____    ____ |__|____    _____/  |_           │ \\
// │         \_____  \|  |  \/    \  / ___\|  \__  \  /    \   __\          │ \\
// │         /        \  |  /   |  \/ /_/  >  |/ __ \|   |  \  |            │ \\
// │        /_______  /____/|___|  /\___  /|__(____  /___|  /__|            │ \\
// │                \/           \//_____/         \/     \/                │ \\
// │                                                                        │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Copyright © 2013 A.J.Pook (http://sungiant.github.com)                 │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Permission is hereby granted, free of charge, to any person obtaining  │ \\
// │ a copy of this software and associated documentation files (the        │ \\
// │ "Software"), to deal in the Software without restriction, including    │ \\
// │ without limitation the rights to use, copy, modify, merge, publish,    │ \\
// │ distribute, sublicense, and/or sellcopies of the Software, and to      │ \\
// │ permit persons to whom the Software is furnished to do so, subject to  │ \\
// │ the following conditions:                                              │ \\
// │                                                                        │ \\
// │ The above copyright notice and this permission notice shall be         │ \\
// │ included in all copies or substantial portions of the Software.        │ \\
// │                                                                        │ \\
// │ THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,        │ \\
// │ EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF     │ \\
// │ MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. │ \\
// │ IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY   │ \\
// │ CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,   │ \\
// │ TORT OR OTHERWISE, ARISING FROM,OUT OF OR IN CONNECTION WITH THE       │ \\
// │ SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                 │ \\
// └────────────────────────────────────────────────────────────────────────┘ \\

using System;
using System.Runtime.InteropServices;
using Debug = System.Diagnostics.Debug;

namespace Sungiant.Abacus.Packed
{
	public partial struct Rgba32
	{
		public static Rgba32 Transparent {
			get {
				return new Rgba32 (0);
			}
		}
		
		public static Rgba32 AliceBlue {
			get {
				return new Rgba32 (0xfffff8f0);
			}
		}
		
		public static Rgba32 AntiqueWhite {
			get {
				return new Rgba32 (0xffd7ebfa);
			}
		}
		
		public static Rgba32 Aqua {
			get {
				return new Rgba32 (0xffffff00);
			}
		}
		
		public static Rgba32 Aquamarine {
			get {
				return new Rgba32 (0xffd4ff7f);
			}
		}
		
		public static Rgba32 Azure {
			get {
				return new Rgba32 (0xfffffff0);
			}
		}
		
		public static Rgba32 Beige {
			get {
				return new Rgba32 (0xffdcf5f5);
			}
		}
		
		public static Rgba32 Bisque {
			get {
				return new Rgba32 (0xffc4e4ff);
			}
		}
		
		public static Rgba32 Black {
			get {
				return new Rgba32 (0xff000000);
			}
		}
		
		public static Rgba32 BlanchedAlmond {
			get {
				return new Rgba32 (0xffcdebff);
			}
		}
		
		public static Rgba32 Blue {
			get {
				return new Rgba32 (0xffff0000);
			}
		}
		
		public static Rgba32 BlueViolet {
			get {
				return new Rgba32 (0xffe22b8a);
			}
		}
		
		public static Rgba32 Brown {
			get {
				return new Rgba32 (0xff2a2aa5);
			}
		}
		
		public static Rgba32 BurlyWood {
			get {
				return new Rgba32 (0xff87b8de);
			}
		}
		
		public static Rgba32 CadetBlue {
			get {
				return new Rgba32 (0xffa09e5f);
			}
		}
		
		public static Rgba32 Chartreuse {
			get {
				return new Rgba32 (0xff00ff7f);
			}
		}
		
		public static Rgba32 Chocolate {
			get {
				return new Rgba32 (0xff1e69d2);
			}
		}
		
		public static Rgba32 Coral {
			get {
				return new Rgba32 (0xff507fff);
			}
		}
		
		public static Rgba32 CornflowerBlue {
			get {
				return new Rgba32 (0xffed9564);
			}
		}
		
		public static Rgba32 Cornsilk {
			get {
				return new Rgba32 (0xffdcf8ff);
			}
		}
		
		public static Rgba32 Crimson {
			get {
				return new Rgba32 (0xff3c14dc);
			}
		}
		
		public static Rgba32 Cyan {
			get {
				return new Rgba32 (0xffffff00);
			}
		}
		
		public static Rgba32 DarkBlue {
			get {
				return new Rgba32 (0xff8b0000);
			}
		}
		
		public static Rgba32 DarkCyan {
			get {
				return new Rgba32 (0xff8b8b00);
			}
		}
		
		public static Rgba32 DarkGoldenrod {
			get {
				return new Rgba32 (0xff0b86b8);
			}
		}
		
		public static Rgba32 DarkGray {
			get {
				return new Rgba32 (0xffa9a9a9);
			}
		}
		
		public static Rgba32 DarkGreen {
			get {
				return new Rgba32 (0xff006400);
			}
		}
		
		public static Rgba32 DarkKhaki {
			get {
				return new Rgba32 (0xff6bb7bd);
			}
		}
		
		public static Rgba32 DarkMagenta {
			get {
				return new Rgba32 (0xff8b008b);
			}
		}
		
		public static Rgba32 DarkOliveGreen {
			get {
				return new Rgba32 (0xff2f6b55);
			}
		}
		
		public static Rgba32 DarkOrange {
			get {
				return new Rgba32 (0xff008cff);
			}
		}
		
		public static Rgba32 DarkOrchid {
			get {
				return new Rgba32 (0xffcc3299);
			}
		}
		
		public static Rgba32 DarkRed {
			get {
				return new Rgba32 (0xff00008b);
			}
		}
		
		public static Rgba32 DarkSalmon {
			get {
				return new Rgba32 (0xff7a96e9);
			}
		}
		
		public static Rgba32 DarkSeaGreen {
			get {
				return new Rgba32 (0xff8bbc8f);
			}
		}
		
		public static Rgba32 DarkSlateBlue {
			get {
				return new Rgba32 (0xff8b3d48);
			}
		}
		
		public static Rgba32 DarkSlateGray {
			get {
				return new Rgba32 (0xff4f4f2f);
			}
		}
		
		public static Rgba32 DarkTurquoise {
			get {
				return new Rgba32 (0xffd1ce00);
			}
		}
		
		public static Rgba32 DarkViolet {
			get {
				return new Rgba32 (0xffd30094);
			}
		}
		
		public static Rgba32 DeepPink {
			get {
				return new Rgba32 (0xff9314ff);
			}
		}
		
		public static Rgba32 DeepSkyBlue {
			get {
				return new Rgba32 (0xffffbf00);
			}
		}
		
		public static Rgba32 DimGray {
			get {
				return new Rgba32 (0xff696969);
			}
		}
		
		public static Rgba32 DodgerBlue {
			get {
				return new Rgba32 (0xffff901e);
			}
		}
		
		public static Rgba32 Firebrick {
			get {
				return new Rgba32 (0xff2222b2);
			}
		}
		
		public static Rgba32 FloralWhite {
			get {
				return new Rgba32 (0xfff0faff);
			}
		}
		
		public static Rgba32 ForestGreen {
			get {
				return new Rgba32 (0xff228b22);
			}
		}
		
		public static Rgba32 Fuchsia {
			get {
				return new Rgba32 (0xffff00ff);
			}
		}
		
		public static Rgba32 Gainsboro {
			get {
				return new Rgba32 (0xffdcdcdc);
			}
		}
		
		public static Rgba32 GhostWhite {
			get {
				return new Rgba32 (0xfffff8f8);
			}
		}
		
		public static Rgba32 Gold {
			get {
				return new Rgba32 (0xff00d7ff);
			}
		}
		
		public static Rgba32 Goldenrod {
			get {
				return new Rgba32 (0xff20a5da);
			}
		}
		
		public static Rgba32 Gray {
			get {
				return new Rgba32 (0xff808080);
			}
		}
		
		public static Rgba32 Green {
			get {
				return new Rgba32 (0xff008000);
			}
		}
		
		public static Rgba32 GreenYellow {
			get {
				return new Rgba32 (0xff2fffad);
			}
		}
		
		public static Rgba32 Honeydew {
			get {
				return new Rgba32 (0xfff0fff0);
			}
		}
		
		public static Rgba32 HotPink {
			get {
				return new Rgba32 (0xffb469ff);
			}
		}
		
		public static Rgba32 IndianRed {
			get {
				return new Rgba32 (0xff5c5ccd);
			}
		}
		
		public static Rgba32 Indigo {
			get {
				return new Rgba32 (0xff82004b);
			}
		}
		
		public static Rgba32 Ivory {
			get {
				return new Rgba32 (0xfff0ffff);
			}
		}
		
		public static Rgba32 Khaki {
			get {
				return new Rgba32 (0xff8ce6f0);
			}
		}
		
		public static Rgba32 Lavender {
			get {
				return new Rgba32 (0xfffae6e6);
			}
		}
		
		public static Rgba32 LavenderBlush {
			get {
				return new Rgba32 (0xfff5f0ff);
			}
		}
		
		public static Rgba32 LawnGreen {
			get {
				return new Rgba32 (0xff00fc7c);
			}
		}
		
		public static Rgba32 LemonChiffon {
			get {
				return new Rgba32 (0xffcdfaff);
			}
		}
		
		public static Rgba32 LightBlue {
			get {
				return new Rgba32 (0xffe6d8ad);
			}
		}
		
		public static Rgba32 LightCoral {
			get {
				return new Rgba32 (0xff8080f0);
			}
		}
		
		public static Rgba32 LightCyan {
			get {
				return new Rgba32 (0xffffffe0);
			}
		}
		
		public static Rgba32 LightGoldenrodYellow {
			get {
				return new Rgba32 (0xffd2fafa);
			}
		}
		
		public static Rgba32 LightGreen {
			get {
				return new Rgba32 (0xff90ee90);
			}
		}
		
		public static Rgba32 LightGray {
			get {
				return new Rgba32 (0xffd3d3d3);
			}
		}
		
		public static Rgba32 LightPink {
			get {
				return new Rgba32 (0xffc1b6ff);
			}
		}
		
		public static Rgba32 LightSalmon {
			get {
				return new Rgba32 (0xff7aa0ff);
			}
		}
		
		public static Rgba32 LightSeaGreen {
			get {
				return new Rgba32 (0xffaab220);
			}
		}
		
		public static Rgba32 LightSkyBlue {
			get {
				return new Rgba32 (0xffface87);
			}
		}
		
		public static Rgba32 LightSlateGray {
			get {
				return new Rgba32 (0xff998877);
			}
		}
		
		public static Rgba32 LightSteelBlue {
			get {
				return new Rgba32 (0xffdec4b0);
			}
		}
		
		public static Rgba32 LightYellow {
			get {
				return new Rgba32 (0xffe0ffff);
			}
		}
		
		public static Rgba32 Lime {
			get {
				return new Rgba32 (0xff00ff00);
			}
		}
		
		public static Rgba32 LimeGreen {
			get {
				return new Rgba32 (0xff32cd32);
			}
		}
		
		public static Rgba32 Linen {
			get {
				return new Rgba32 (0xffe6f0fa);
			}
		}
		
		public static Rgba32 Magenta {
			get {
				return new Rgba32 (0xffff00ff);
			}
		}
		
		public static Rgba32 Maroon {
			get {
				return new Rgba32 (0xff000080);
			}
		}
		
		public static Rgba32 MediumAquamarine {
			get {
				return new Rgba32 (0xffaacd66);
			}
		}
		
		public static Rgba32 MediumBlue {
			get {
				return new Rgba32 (0xffcd0000);
			}
		}
		
		public static Rgba32 MediumOrchid {
			get {
				return new Rgba32 (0xffd355ba);
			}
		}
		
		public static Rgba32 MediumPurple {
			get {
				return new Rgba32 (0xffdb7093);
			}
		}
		
		public static Rgba32 MediumSeaGreen {
			get {
				return new Rgba32 (0xff71b33c);
			}
		}
		
		public static Rgba32 MediumSlateBlue {
			get {
				return new Rgba32 (0xffee687b);
			}
		}
		
		public static Rgba32 MediumSpringGreen {
			get {
				return new Rgba32 (0xff9afa00);
			}
		}
		
		public static Rgba32 MediumTurquoise {
			get {
				return new Rgba32 (0xffccd148);
			}
		}
		
		public static Rgba32 MediumVioletRed {
			get {
				return new Rgba32 (0xff8515c7);
			}
		}
		
		public static Rgba32 MidnightBlue {
			get {
				return new Rgba32 (0xff701919);
			}
		}
		
		public static Rgba32 MintCream {
			get {
				return new Rgba32 (0xfffafff5);
			}
		}
		
		public static Rgba32 MistyRose {
			get {
				return new Rgba32 (0xffe1e4ff);
			}
		}
		
		public static Rgba32 Moccasin {
			get {
				return new Rgba32 (0xffb5e4ff);
			}
		}
		
		public static Rgba32 NavajoWhite {
			get {
				return new Rgba32 (0xffaddeff);
			}
		}
		
		public static Rgba32 Navy {
			get {
				return new Rgba32 (0xff800000);
			}
		}
		
		public static Rgba32 OldLace {
			get {
				return new Rgba32 (0xffe6f5fd);
			}
		}
		
		public static Rgba32 Olive {
			get {
				return new Rgba32 (0xff008080);
			}
		}
		
		public static Rgba32 OliveDrab {
			get {
				return new Rgba32 (0xff238e6b);
			}
		}
		
		public static Rgba32 Orange {
			get {
				return new Rgba32 (0xff00a5ff);
			}
		}
		
		public static Rgba32 OrangeRed {
			get {
				return new Rgba32 (0xff0045ff);
			}
		}
		
		public static Rgba32 Orchid {
			get {
				return new Rgba32 (0xffd670da);
			}
		}
		
		public static Rgba32 PaleGoldenrod {
			get {
				return new Rgba32 (0xffaae8ee);
			}
		}
		
		public static Rgba32 PaleGreen {
			get {
				return new Rgba32 (0xff98fb98);
			}
		}
		
		public static Rgba32 PaleTurquoise {
			get {
				return new Rgba32 (0xffeeeeaf);
			}
		}
		
		public static Rgba32 PaleVioletRed {
			get {
				return new Rgba32 (0xff9370db);
			}
		}
		
		public static Rgba32 PapayaWhip {
			get {
				return new Rgba32 (0xffd5efff);
			}
		}
		
		public static Rgba32 PeachPuff {
			get {
				return new Rgba32 (0xffb9daff);
			}
		}
		
		public static Rgba32 Peru {
			get {
				return new Rgba32 (0xff3f85cd);
			}
		}
		
		public static Rgba32 Pink {
			get {
				return new Rgba32 (0xffcbc0ff);
			}
		}
		
		public static Rgba32 Plum {
			get {
				return new Rgba32 (0xffdda0dd);
			}
		}
		
		public static Rgba32 PowderBlue {
			get {
				return new Rgba32 (0xffe6e0b0);
			}
		}
		
		public static Rgba32 Purple {
			get {
				return new Rgba32 (0xff800080);
			}
		}
		
		public static Rgba32 Red {
			get {
				return new Rgba32 (0xff0000ff);
			}
		}
		
		public static Rgba32 RosyBrown {
			get {
				return new Rgba32 (0xff8f8fbc);
			}
		}
		
		public static Rgba32 RoyalBlue {
			get {
				return new Rgba32 (0xffe16941);
			}
		}
		
		public static Rgba32 SaddleBrown {
			get {
				return new Rgba32 (0xff13458b);
			}
		}
		
		public static Rgba32 Salmon {
			get {
				return new Rgba32 (0xff7280fa);
			}
		}
		
		public static Rgba32 SandyBrown {
			get {
				return new Rgba32 (0xff60a4f4);
			}
		}
		
		public static Rgba32 SeaGreen {
			get {
				return new Rgba32 (0xff578b2e);
			}
		}
		
		public static Rgba32 SeaShell {
			get {
				return new Rgba32 (0xffeef5ff);
			}
		}
		
		public static Rgba32 Sienna {
			get {
				return new Rgba32 (0xff2d52a0);
			}
		}
		
		public static Rgba32 Silver {
			get {
				return new Rgba32 (0xffc0c0c0);
			}
		}
		
		public static Rgba32 SkyBlue {
			get {
				return new Rgba32 (0xffebce87);
			}
		}
		
		public static Rgba32 SlateBlue {
			get {
				return new Rgba32 (0xffcd5a6a);
			}
		}
		
		public static Rgba32 SlateGray {
			get {
				return new Rgba32 (0xff908070);
			}
		}
		
		public static Rgba32 Snow {
			get {
				return new Rgba32 (0xfffafaff);
			}
		}
		
		public static Rgba32 SpringGreen {
			get {
				return new Rgba32 (0xff7fff00);
			}
		}
		
		public static Rgba32 SteelBlue {
			get {
				return new Rgba32 (0xffb48246);
			}
		}
		
		public static Rgba32 Tan {
			get {
				return new Rgba32 (0xff8cb4d2);
			}
		}
		
		public static Rgba32 Teal {
			get {
				return new Rgba32 (0xff808000);
			}
		}
		
		public static Rgba32 Thistle {
			get {
				return new Rgba32 (0xffd8bfd8);
			}
		}
		
		public static Rgba32 Tomato {
			get {
				return new Rgba32 (0xff4763ff);
			}
		}
		
		public static Rgba32 Turquoise {
			get {
				return new Rgba32 (0xffd0e040);
			}
		}
		
		public static Rgba32 Violet {
			get {
				return new Rgba32 (0xffee82ee);
			}
		}
		
		public static Rgba32 Wheat {
			get {
				return new Rgba32 (0xffb3def5);
			}
		}
		
		public static Rgba32 White {
			get {
				return new Rgba32 (UInt32.MaxValue);
			}
		}
		
		public static Rgba32 WhiteSmoke {
			get {
				return new Rgba32 (0xfff5f5f5);
			}
		}
		
		public static Rgba32 Yellow {
			get {
				return new Rgba32 (0xff00ffff);
			}
		}
		
		public static Rgba32 YellowGreen {
			get {
				return new Rgba32 (0xff32cd9a);
			}
		}
	}
}

