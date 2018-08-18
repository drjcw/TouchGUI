using System;
using System.Text.RegularExpressions;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Defines a unit of measurment in either pixels, percentages (of parent rects) or virtual units.
            /// </summary>
            public class Unit
            {
                /// <summary>
                /// Defines the unit type.
                /// </summary>
                /// <remarks>
                /// <list type="table">
                /// <listheader>
                /// <term>Type</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>Virtual</term>
                /// <term>Used in conjunction with a scaling factor to map virtual units to pixel units</term>
                /// </item>
                /// <item>
                /// <term>Pixel</term>
                /// <term>Maps directly to screen units</term>
                /// </item>
                /// <item>
                /// <term>Percentage</term>
                /// <term>Specified as a percentage of a given parent dimension</term>
                /// </item>
                /// </list>
                /// </remarks>
                public enum Type
                {
                    Virtual,
                    Pixel,
                    Percentage
                };

                /// <summary>
                /// Initializes the unit to the properties of an existing unit.
                /// </summary>
                /// <param name="V">The value of the unit.</param>
                /// <param name="T">The type of the unit.</param>
                /// <example>
                /// Instantiate a unit object with a virtual value of 86:
                /// <code>
                /// Unit u = new Unit(86, Unit.Type.Virtual);
                /// </code>
                /// </example>
                public Unit(float? V, Type? T)
                {
                    type = T;
                    value = V;
                }

                /// <summary>
                /// Initializes the unit to the value defined by a string.
                /// </summary>
                /// <param name="V">The string containing the unit value followed by the type suffix:
                /// <list type="table">
                /// <listheader>
                /// <term>Unit</term>
                /// <term>Suffix</term>
                /// </listheader>
                /// <item>
                /// <term>Virtual</term>
                /// <term>none</term>
                /// </item>
                /// <item>
                /// <term>Pixel</term>
                /// <term>px</term>
                /// </item>
                /// <item>
                /// <term>Percentage</term>
                /// <term>%</term>
                /// </item>
                /// </list>
                /// </param>
                /// <remarks>If the string value is null or empty, the unit properties will be null</remarks>
                /// <example>
                /// Instantiate a unit object with a virtual value of 140:
                /// <code>Unit u = new Unit("140");</code>
                /// Instantiate a unit object with a pixel value of 96:
                /// <code>Unit u = new Unit("96px");</code>
                /// Instantiate a unit object with a perdentage value of 10:
                /// <code>Unit u = new Unit("20%");</code>
                /// </example>
                public Unit(string V)
                {
                    if (V == null || V == "")
                    {
                        type = null;
                        value = null;
                    }
                    else
                    {
                        MatchCollection mc = Regex.Matches(V, @"^([-+]?[0-9]*\.?[0-9]+)(px|%)?$");

                        if (mc.Count != 1) throw new Exception("Error parsing unit string, unknown pattern");

                        value = float.Parse(mc[0].Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture);

                        switch (mc[0].Groups[2].Value)
                        {
                            case "%":

                                type = Type.Percentage;
                                break;

                            case "px":

                                type = Type.Pixel;
                                break;

                            case "":

                                type = Type.Virtual;
                                break;

                            default: throw new Exception("Error parsing unit string, unknown unit type");
                        }
                    }
                }

                /// <summary>
                /// Converts the unit value to pixels.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if unit type is virtual.</param>
                /// <param name="ParentPx">The parent dimension (in pixels) if unit type is percentage.</param>
                /// <returns>The value of the unit in pixels.</returns>
                /// <remarks>Both type and value must not be null.</remarks>
                /// <example>
                /// Convert the unit value to a pixel value:
                /// <code>
                /// float w = u.ToPixel(vScale, wParent);
                /// </code>
                /// </example>
                public float ToPixel(float VirtualScale, float ParentPx)
                {
                    if (value == null) throw new Exception("Cannot convert from unit with null value");
                    if (type == null) throw new Exception("Cannot convert from unit with null type");

                    switch (type)
                    {
                        case Type.Percentage:

                            return ParentPx * ((float)value / 100.0f);

                        case Type.Pixel:

                            return (float)value;

                        case Type.Virtual:

                            return (float)value * VirtualScale;
                    }

                    throw new Exception("Unexpected unit type");
                }

                /// <summary>
                /// The type of unit.
                /// </summary>
                public Type? type;

                /// <summary>
                /// The value of this unit.
                /// </summary>
                public float? value;
            }
        }
    } 
}