using System;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Specifies a font for use in text rendering.
            /// </summary>
            public class Font : Debugable
            {
                /// <summary>
                /// Initializes the components of the font to the values specified by the strings. 
                /// </summary>
                /// <param name="FontFace">The path to the font face to load for this font.</param>
                /// <param name="FontSize">The integer non-zero size of this font.</param>
                /// <remarks>This method is used primarily in conjunction with XML-based layouts.</remarks>
                /// <example>
                /// Instantiate a font object with a green colour, a font face of Roboto and a font size of 16:
                /// <code>Font f = new Font("rgb(0,255,0)", "fonts/roboto", "16");</code>
                /// </example>
                public Font(string FontFace, string FontSize)
                {
                    try
                    {
                        fontSize = int.Parse(FontSize);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Font size is either null or not in the correct format for integer parsing: " + FontSize);
                    }

                    if (FontFace != null && FontFace != "")
                    {
                        fontFace = UnityEngine.Resources.Load(FontFace) as UnityEngine.Font;

                        if (fontFace == null) throw new Exception("Font couldn't load font face " + FontFace);
                    }
                }

                public Font(Font Src)
                {
                    fontFace = Src.fontFace;
                    fontSize = Src.fontSize;
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Font) fontFace: {0}, fontSize: {1}", fontFace, fontSize);
                }

                /// <summary>
                /// Getter/setter for the _fontSize member.
                /// </summary>
                /// <remarks>Values less than or equal to 0 are invalid.</remarks>
                public int fontSize
                {
                    get { return _fontSize; }

                    set
                    {
                        if (value <= 0) throw new Exception("Font size must be greater than 0");

                        _fontSize = value;
                    }
                }

                /// <summary>
                /// The font face.
                /// </summary>
                public UnityEngine.Font fontFace;

                /// <summary>
                /// The font size.
                /// </summary>
                private int _fontSize;
            }
        }
    } 
}