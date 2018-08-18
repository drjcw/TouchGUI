using System;
using System.Xml;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Widget for displaying a standard button.
            /// </summary>
            public class Button : Presentable
            {
                /// <summary>
                /// Initializes the components of the button to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                /// <param name="Children">The XML node list containing the child elements that compose the style of the button (see XML notation docs for further information).</param>
                /// <param name="X">The x positioning of the button relative to the parent as governed by the horizontal alignment as follows:
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
                /// <param name="Y">The y positioning of the button relative to the parent as governed by the vertical alignment as follows:
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
                /// <param name="W">The width of the button.</param>
                /// <param name="H">The height of the button.</param>
                /// <param name="HAlign">The horizontal alignemnt of the button within the parent rect as specified by the following values:
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
                /// <param name="VAlign">The horizontal alignemnt of the button within the parent rect  as specified by the following values:
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
                /// Instantiate a button object with an id of "myButton" (child nodes omitted for clarity) spaced 10% from the left edge of the parent rect, measuring 100x100 virtual units and centered vertically:
                /// <code>
                /// Button button = new Button("myButton", xmlChildren, "10%", null, "100", "100", null, "center");
                /// </code>
                /// </example>
                public Button(StyleSheet Styling, string ID, XmlNodeList Children, string X, string Y, string W, string H, string HAlign, string VAlign, string Enabled, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    _guiDispatcher = new GUIEventDispatcher(/*this*/);

                    enabled = Enabled == null || Enabled == "" ? true : bool.Parse(Enabled);

                    foreach (XmlNode element in Children)
                    {
                        string style = Xml.Att(element.Attributes["style"]);

                        switch (element.Name)
                        {
                            // Text styling
                            case "text":

                                string textAlign = Styling.Apply(style, "textAlign", element.Attributes);

                                if (textAlign == "" || textAlign == null) textAlign = "middleCenter";

                                text = new Text(element.InnerText,
                                                textAlign,
                                                null,
                                                Styling.Apply(style, "colour", element.Attributes),
                                                             Styling.Apply(style, "fontFace", element.Attributes),
                                                             Styling.Apply(style, "fontSize", element.Attributes));

                                textDisabled = Colour.Parse(Xml.Att(element.Attributes["disabledColour"]));

                                break;

                            // Background
                            case "background":

                                background = new Image(null,
                                                       Styling.Apply(style, "src", element.Attributes),
                                                       Styling.Apply(style, "colour", element.Attributes),
                                                       Styling.Apply(style, "scale", element.Attributes),
                                                       "0",
                                                       "0",
                                                       "100%",
                                                       "100%",
                                                       null,
                                                       null,
                                                       null);

                                break;

                            default: throw new Exception("Button has unexpected element: " + element.Name);
                        }
                    }

                    if (background == null) throw new Exception("Button background is required");
                }

                /// <summary>
                /// Initializes the components of the button to the specified values. 
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                /// <param name="T">The Text object representing the test and styling of the button label.</param>
                /// <param name="B">The image object representing the button background.</param>
                /// <param name="X">The x positioning of the button relative to the parent as governed by the horizontal alignment as follows:
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
                /// <param name="Y">The y positioning of the button relative to the parent as governed by the vertical alignment as follows:
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
                /// <param name="W">The width of the button.</param>
                /// <param name="H">The height of the button.</param>
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
                /// <example>
                /// Instantiate a button object with an id of "myButton" (child objects omitted for clarity) spaced 10% from the left edge of the parent rect, measuring 100x100 virtual units and centered vertically:
                /// <code>
                /// Button button = new Button("myButton", textLabel, imgBackground, "10%", null, "100", "100", null, "center");
                /// </code>
                /// </example>
                public Button(string ID, Text T, UnityEngine.Color? TextDisabled, Image B, string X, string Y, string W, string H, string HAlign, string VAlign, string Enabled, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    text = T == null ? null : new Text(T);
                    textDisabled = TextDisabled;
                    _background = B;
                    _guiDispatcher = new GUIEventDispatcher(/*this*/);
                    enabled = Enabled == null || Enabled == "" ? true : bool.Parse(Enabled);
                }

                /// <summary>
                /// Presents the button visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.Rect rPx = rect.ToPixelRect(VirtualScale, Parent);

                    background.Present(VirtualScale, rPx);

                    bool clicked;

                    if (text != null)
                    {
                        UnityEngine.GUIStyle style = text.CalculateGUIStyle(VirtualScale);

                        style.normal.textColor = enabled ? text.colour ?? style.normal.textColor : textDisabled ?? (text.colour ?? style.normal.textColor);
                        style.normal.background = null;
                        style.active.background = null;
                        style.hover.background = null;
                        style.focused.background = null;

                        clicked = UnityEngine.GUI.Button(rPx, text.text, style);
                    }
                    else clicked = UnityEngine.GUI.Button(rPx, "");

                    if (enabled && clicked)
                    {
                        guiDispatcher.Dispatch("onClick", this);
                    }
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Button) text: {0}, background: {1}, {2}", DebugInfo(text), DebugInfo(background), base.DebugInfo());
                }

                /// <summary>
                /// Getter/setter for the _text member.
                /// </summary>
                /// <remarks>_text can be null. Word wrapping will be disabled.</remarks>
                public Text text
                {
                    get { return _text; }

                    set
                    {
                        _text = value;

                        if (_text != null) _text.wordWrap = false;
                    }
                }

                /// <summary>
                /// Getter/setter for the _background member.
                /// </summary>
                /// <remarks>_background cannot be null. Positioning will be top-left and dimensions will be 100%.</remarks>
                public Image background
                {
                    get { return _background; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Button background member cannot be null (id: {0})", id));

                        _background = value;

                        _background.rect.pos.x.type = Unit.Type.Virtual;
                        _background.rect.pos.x.value = 0;
                        _background.rect.pos.y.type = Unit.Type.Virtual;
                        _background.rect.pos.y.value = 0;

                        _background.rect.dim.x.type = Unit.Type.Percentage;
                        _background.rect.dim.x.value = 100;
                        _background.rect.dim.y.type = Unit.Type.Percentage;
                        _background.rect.dim.y.value = 100;

                        _background.rect.hAlign = new Alignment(null);
                        _background.rect.vAlign = new Alignment(null);
                    }
                }

                public GUIEventDispatcher guiDispatcher
                {
                    get { return _guiDispatcher; }
                }

                public bool enabled;
                public UnityEngine.Color? textDisabled;

                /// <summary>
                /// Text and styling for the button label.
                /// </summary>
                protected Text _text;

                /// <summary>
                /// Image for the button background.
                /// </summary>
                protected Image _background;

                protected GUIEventDispatcher _guiDispatcher;
            }
        }
    } 
}