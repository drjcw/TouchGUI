using System;
namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Widget for displaying formatted text.
            /// </summary>
            public class Label : Presentable
            {
                /// <summary>
                /// Initializes the components of the label to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
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
                /// Note: word wrapping can only be enabled if the label has a defined width.
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
                /// <param name="X">The x positioning of the label relative to the parent as governed by the horizontal alignment as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>Alignment</term>
                /// <term>X Position</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>x units from parent's left edge</term>
                /// </item>
                /// <item>
                /// <term>Left</term>
                /// <term>x units from parent's left edge</term>
                /// </item>
                /// <item>
                /// <term>Center</term>
                /// <term>x position is ignored</term>
                /// </item>
                /// <item>
                /// <term>Right</term>
                /// <term>x units from parent's right edge</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="Y">The y positioning of the label relative to the parent as governed by the vertical alignment as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>Alignment</term>
                /// <term>Y Position</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>y units from parent's top edge</term>
                /// </item>
                /// <item>
                /// <term>Top</term>
                /// <term>y units from parent's top edge</term>
                /// </item>
                /// <item>
                /// <term>Center</term>
                /// <term>y position is ignored</term>
                /// </item>
                /// <item>
                /// <term>Bottom</term>
                /// <term>y units from parent's bottom edge</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="W">The width of the label.
                /// <list type="table">
                /// <listheader>
                /// <term>W Value</term>
                /// <term>WordWrap Value</term>
                /// <term>Calculated Width</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>false</term>
                /// <term>Width of string according to text style</term>
                /// </item>
                /// <item>
                /// <term>null or empty</term>
                /// <term>true</term>
                /// <term>Invalid, exception thrown</term>
                /// </item>
                /// <item>
                /// <term>Specified</term>
                /// <term>false</term>
                /// <term>Width of string according to specified width</term>
                /// </item>
                /// <item>
                /// <term>Specified</term>
                /// <term>true</term>
                /// <term>Width of string according to specified width</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="H">The height of the label.
                /// <list type="table">
                /// <listheader>
                /// <term>H Value</term>
                /// <term>Calculated Height</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Height of string according to text style</term>
                /// </item>
                /// <item>
                /// <term>Specified</term>
                /// <term>Height of string according to specified height</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="HAlign">The horizontal alignemnt of the label within the parent rect as specified by the following values:
                /// <list type="table">
                /// <listheader>
                /// <term>String Value</term>
                /// <term>Alignment.Type</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>null</term>
                /// </item>
                /// <item>
                /// <term>left</term>
                /// <term>Alignment.Type.Left</term>
                /// </item>
                /// <item>
                /// <term>center</term>
                /// <term>Alignment.Type.Center</term>
                /// </item>
                /// <item>
                /// <term>right</term>
                /// <term>Alignment.Type.Right</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="VAlign">The horizontal alignemnt of the label within the parent rect  as specified by the following values:
                /// <list type="table">
                /// <listheader>
                /// <term>String Value</term>
                /// <term>Alignment.Type</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>null</term>
                /// </item>
                /// <item>
                /// <term>top</term>
                /// <term>Alignment.Type.Top</term>
                /// </item>
                /// <item>
                /// <term>center</term>
                /// <term>Alignment.Type.Center</term>
                /// </item>
                /// <item>
                /// <term>bottom</term>
                /// <term>Alignment.Type.Bottom</term>
                /// </item>
                /// </list>
                /// </param>
                /// <remarks>This constructor is used primarily in conjunction with XML-based layouts</remarks>
                /// <example>
                /// Instantiate a label object with an id of "myString", the text value of "Hello World!" with centered text alignment, no word wrap, a green colour, a font face of Roboto and a font size of 16  spaced 10% from the left edge of the parent rect, measuring 100x100 virtual units and centered vertically:
                /// <code>
                /// Label label = new Label("myString", "Hello World!", "middleCenter", null, "rgb(0,255,0)", "fonts/roboto", "16", "10%", null, "100", "100", null, "center");
                /// </code>
                /// </example>
                public Label(string ID, string Text, string TextAlign, string WordWrap, string TextColour, string FontFace, string FontSize, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    text = new Text(Text, TextAlign, WordWrap, TextColour, FontFace, FontSize);

                    if (text.wordWrap == true && rect.dim.x == null) throw new Exception("Wordwrap can only be used when width is specified");
                }

                public Label(string ID, Text TextStyle, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    if (TextStyle == null) text = null;
                    else text = new Text(TextStyle);
                }

                /// <summary>
                /// Presents the label visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.GUIStyle style = _text.CalculateGUIStyle(VirtualScale);
                    UnityEngine.Rect rPx = text.CalculatePixelRect(VirtualScale, Parent, rect);
                    UnityEngine.GUI.Label(rPx, text.text, style);
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Label) Text: {0}, {1}", _text.DebugInfo(), base.DebugInfo());
                }

                /// <summary>
                /// Getter/setter for the _text member.
                /// </summary>
                /// <remarks>_text cannot be null. Wordwrap can only be used when width is specified.</remarks>
                public Text text
                {
                    get { return _text; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Label text member cannot be null (id: {0})", id));
                        if (value.wordWrap == true && rect.dim.x == null) throw new Exception("Wordwrap can only be used when width is specified");

                        _text = value;
                    }
                }

                /// <summary>
                /// The text and styling
                /// </summary>
                protected Text _text;
            }
        }
    } 
}