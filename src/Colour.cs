using System;
using System.Text.RegularExpressions;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Static class for providing utility methods for working with colours.
            /// </summary>
            public static class Colour
            {
                /// <summary>
                /// Parses a colour string and returns a colour object.
                /// </summary>
                /// <param name="Colour">The string containing the colour information.
                /// <list type="table">
                /// <listheader>
                /// <term>Format</term>
                /// <term>Description</term>
                /// <term>Example</term>
                /// </listheader>
                /// <item>
                /// <term>rgb</term>
                /// <term>Specifies a colour in rgb format</term>
                /// <term>rgb(255,0,0)</term>
                /// </item>
                /// <item>
                /// <term>rgba</term>
                /// <term>Specifies a colour in rgb format with an alpha component</term>
                /// <term>rgba(255,0,0,128)</term>
                /// </item>
                /// </list>
                /// </param>
                /// <returns>The colour object initialized to colour information specified in <paramref name="Colour"/>.</returns>
                /// <remarks>This method is used primarily in conjunction with XML-based layouts.</remarks>
                /// <example>
                /// Generate a colour object from a string containing a red colour:
                /// <code>UnityEngine.Color c = Colour.Parse("rgb(255,0,0)");</code>
                /// Generate a colour object from a string containing a red colour with half transparency:
                /// <code>UnityEngine.Color c = Colour.Parse("rgba(255,0,0,128)");</code>
                /// </example>
                public static UnityEngine.Color? Parse(string Colour)
                {
                    UnityEngine.Color? rgba;

                    if (Colour != null && Colour != "")
                    {
                        // Colour in the form of a filename or an rgb(a) component
                        MatchCollection mc = Regex.Matches(Colour, @"rgb?a?\s*\(\s*(?<red>\d+)\s*,\s*(?<green>\d+)\s*,\s*(?<blue>\d+)\s*\s*,?\s*(?<alpha>\d+)?\)");

                        if (mc.Count != 1) throw new Exception("Error parsing colour string, unknown pattern");

                        // r, g and b components are compulsory, a is optional
                        if (mc[0].Groups["red"].Value == "" || mc[0].Groups["green"].Value == "" || mc[0].Groups["blue"].Value == "") throw new Exception("Error parsing colour string, r, g or b components empty");

                        float r, g, b, a;

                        r = uint.Parse(mc[0].Groups["red"].Value) / 255.0f;
                        g = uint.Parse(mc[0].Groups["green"].Value) / 255.0f;
                        b = uint.Parse(mc[0].Groups["blue"].Value) / 255.0f;
                        a = mc[0].Groups["alpha"].Value == "" ? 1.0f : uint.Parse(mc[0].Groups["alpha"].Value) / 255.0f;

                        rgba = new UnityEngine.Color(r, g, b, a);
                    }
                    else rgba = null;

                    return rgba;
                }
            }
        }
    } 
}