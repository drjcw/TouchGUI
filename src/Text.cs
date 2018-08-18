using System;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Specifies a formatted text for use in text rendering.
            /// </summary>
            public class Text : Debugable
            {
                /// <summary>
                /// Initializes the components of the text to the values specified by the strings.
                /// </summary>
                /// <param name="Text">The text string to be rendered.</param>
                /// <param name="TextAlign">The string containing the text alignment.
                /// <list type="table">
                /// <listheader>
                /// <term>Format</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Aligns the text to the upper-left corner of the containing element.</term>
                /// </item>           
                /// <item>
                /// <term>upperLeft</term>
                /// <term>Aligns the text to the upper-left corner of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>upperCenter</term>
                /// <term>Aligns the text to the center of the top edge of the containing element.</term>
                /// </item><item>
                /// <term>upperRight</term>
                /// <term>Aligns the text to the upper-right corner of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>middleLeft</term>
                /// <term>Aligns the text to the center of the left edge of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>middleCenter</term>
                /// <term>Aligns the text to the center of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>middleRight</term>
                /// <term>Aligns the text to the center of the right edge of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>lowerLeft</term>
                /// <term>Aligns the text to the lower-left corner of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>lowerCenter</term>
                /// <term>Aligns the text to the center of the bottom edge of the containing element.</term>
                /// </item>
                /// <item>
                /// <term>lowerRight</term>
                /// <term>Aligns the text to the lower-right corner of the containing element.</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="WordWrap">Specifies whether or not the text will be wrapped within the containing element.
                /// <list type="table">
                /// <listheader>
                /// <term>Format</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>No word wrap will be applied</term>
                /// </item>   
                /// <item>
                /// <term>false</term>
                /// <term>No word wrap will be applied</term>
                /// </item>        
                /// <item>
                /// <term>true</term>
                /// <term>Word wrap will be applied</term>
                /// </item>  
                /// </list>
                /// </param>
                /// <param name="TextColour">The string containing the text colour.
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
                /// <param name="FontFace">The path to the font face to load for this text.</param>
                /// <param name="FontSize">The integer non-zero size of this text.</param>
                /// <remarks>This method is used primarily in conjunction with XML-based layouts.</remarks>
                /// <example>
                /// Instantiate a text object for the string "Hello World!" with centered text alignment, no word wrap, a green colour, a font face of Roboto and a font size of 16:
                /// <code>
                /// Text t = new Text("Hello World!", "middleCenter", null, "rgb(0,255,0)", "fonts/roboto", "16");
                /// </code>
                /// </example>
                public Text(string Text, string TextAlign, string WordWrap, string TextColour, string FontFace, string FontSize)
                {
                    text = Text;
                    font = new Font(FontFace, FontSize);
                    colour = Colour.Parse(TextColour);

                    switch (TextAlign)
                    {
                        case null:

                            textAlign = UnityEngine.TextAnchor.UpperLeft;
                            break;

                        case "":

                            textAlign = UnityEngine.TextAnchor.UpperLeft;
                            break;

                        case "middleLeft":

                            textAlign = UnityEngine.TextAnchor.MiddleLeft;
                            break;

                        case "middleCenter":

                            textAlign = UnityEngine.TextAnchor.MiddleCenter;
                            break;

                        case "middleRight":

                            textAlign = UnityEngine.TextAnchor.MiddleRight;
                            break;

                        case "upperLeft":

                            textAlign = UnityEngine.TextAnchor.UpperLeft;
                            break;

                        case "upperCenter":

                            textAlign = UnityEngine.TextAnchor.UpperCenter;
                            break;

                        case "upperRight":

                            textAlign = UnityEngine.TextAnchor.UpperRight;
                            break;

                        case "lowerLeft":

                            textAlign = UnityEngine.TextAnchor.LowerLeft;
                            break;

                        case "lowerCenter":

                            textAlign = UnityEngine.TextAnchor.LowerCenter;
                            break;

                        case "lowerRight":

                            textAlign = UnityEngine.TextAnchor.LowerRight;
                            break;

                        default: throw new Exception("Text alignment is unknown: " + TextAlign);
                    }

                    if (WordWrap == null || WordWrap == "" || WordWrap == "false") wordWrap = false;
                    else if (WordWrap == "true") wordWrap = true;
                    else throw new Exception("Unknown wordwrap: " + WordWrap);
                }

                public Text(Text Src)
                {
                    text = Src.text;
                    textAlign = Src.textAlign;
                    wordWrap = Src.wordWrap;
                    colour = Src.colour;
                    font = new Font(Src.font);
                }

                /// <summary>
                /// Calculates the dimensions (in absolute pixel coordinates) of the text object based on the text string contents and text formatting style.
                /// </summary>
                /// <param name="VirtualScale"></param>
                /// <remarks>Note: this is for text objects without word wrapping. For word wrapping, use CalculatePixelRect.</remarks>
                /// <returns>The dimensions (in absolute pixel coordinates) of this text object's string.</returns>
                /// <example>
                /// Calculate the absolute pixel dimensions for a label widget without word wrapping:
                /// <code>
                /// UnityEngine.Vector2 vDim = text.CalculatePixelSize(VirtualScale);
                /// </code>
                /// </example>
                public UnityEngine.Vector2 CalculatePixelSize(float VirtualScale)
                {
                    return CalculateGUIStyle(VirtualScale).CalcSize(new UnityEngine.GUIContent(text));
                }

                /// <summary>
                /// Calculates the bounding rect (in absolute pixel coordinates) of the text object based on the text string contents, text formatting style and container unit rect.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) if component unit type is percentage.</param>
                /// <param name="Container">The container unit rect of the element this text object belongs to.</param>
                /// <returns>The bounding rect (in absolute pixel coordinates) of this text object.</returns>
                /// <remarks>Note: if either the width or height of the container are null, the size of the bounding rect will be that of the container.</remarks>
                /// <example>
                /// Calculate the absolute pixel bounding rect for a label widget within the specified parent rect:
                /// <code>
                /// UnityEngine.Rect rPx = text.CalculatePixelRect(VirtualScale, Parent, rect);
                /// </code>
                /// </example>
                public UnityEngine.Rect CalculatePixelRect(float VirtualScale, UnityEngine.Rect Parent, UnitRect Container)
                {
                    UnityEngine.Vector2 size = CalculatePixelSize(VirtualScale);

                    if (Container == null)
                    {
                        if (wordWrap == true) throw new Exception("CalculatePixelRect with null container can only be used when wordwrap is disabled");

                        return new UnityEngine.Rect(0, 0, size.x, size.y);
                    }

                    UnitRect r = new UnitRect(Container.pos.x, Container.pos.y, Container.dim.x, Container.dim.y, Container.hAlign, Container.vAlign);

                    if (Container.dim.x.value == null)
                    {
                        r.dim.x.type = Unit.Type.Pixel;
                        r.dim.x.value = size.x;
                    }

                    if (Container.dim.y.value == null)
                    {
                        if (wordWrap)
                        {
                            float w = r.dim.x.ToPixel(VirtualScale, Parent.width);
                            float h = CalculateGUIStyle(VirtualScale).CalcHeight(new UnityEngine.GUIContent(text), w);

                            r.dim.y.type = Unit.Type.Pixel;
                            r.dim.y.value = h;
                        }
                        else
                        {
                            r.dim.y.type = Unit.Type.Pixel;
                            r.dim.y.value = size.y;
                        }
                    }

                    return r.ToPixelRect(VirtualScale, Parent);
                }

                public float CalculatePixelHeight(float VirtualScale, float PixelWidth)
                {
                    if (wordWrap == true)
                    {
                        return CalculateGUIStyle(VirtualScale).CalcHeight(new UnityEngine.GUIContent(text), PixelWidth);
                    }
                    else
                    {
                        return CalculatePixelSize(VirtualScale).y;
                    }
                }

                /// <summary>
                /// Calculates the GUIStyle required to represent the styling of a text object.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to scale the font size to the screen dimensions.</param>
                /// <returns>The GUIStyle representing the styling of the text object.</returns>
                /// <example>
                /// Calculate the style for use with Unity immediate GUI for the given text object:
                /// <code>
                /// UnityEngine.GUIStyle = text.CalculateGUIStyle(VirtualScale);
                /// </code>
                /// </example>
                public UnityEngine.GUIStyle CalculateGUIStyle(float VirtualScale)
                {
                    UnityEngine.GUIStyle style = new UnityEngine.GUIStyle();// = UnityEngine.GUI.skin.label;

                    style.padding = new UnityEngine.RectOffset(0, 0, 0, 0);
                    style.margin = new UnityEngine.RectOffset(0, 0, 0, 0);
                    style.wordWrap = wordWrap;
                    style.alignment = textAlign;
                    style.fontSize = (int)(font.fontSize * VirtualScale);
                    if (font.fontFace != null) style.font = font.fontFace;
                    if (colour != null) style.normal.textColor = (UnityEngine.Color)colour;
                    else style.normal.textColor = UnityEngine.Color.black;

                    return style;
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Text) text: {0}, textAlign: {1}, wordWrap: {2}, colour: {3}, {4}", text, textAlign, wordWrap, colour, DebugInfo(font));
                }

                /// <summary>
                /// Getter/setter for the _font member.
                /// </summary>
                /// <remarks>_font cannot be null.</remarks>
                public Font font
                {
                    get { return _font; }

                    set
                    {
                        if (value == null) throw new Exception("Text font member cannot be null");

                        _font = value;
                    }
                }

                /// <summary>
                /// The text string to be rendered.
                /// </summary>
                public string text;

                /// <summary>
                /// The alignment of the text within the containing element.
                /// </summary>
                public UnityEngine.TextAnchor textAlign;

                /// <summary>
                /// Word wrapping on/off
                /// </summary>
                public bool wordWrap;

                /// <summary>
                /// The font colour.
                /// </summary>
                public UnityEngine.Color? colour;

                /// <summary>
                /// THe font to use for text rendering.
                /// </summary>
                protected Font _font;
            }
        }
    } 
}