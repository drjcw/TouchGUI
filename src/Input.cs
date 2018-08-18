using System;
using System.Xml;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Widget for displaying plain and password input fields.
            /// </summary>
            public class Input : Presentable
            {
                /// <summary>
                /// Specifies the type of input field.
                /// </summary>
                /// <remarks>
                /// <list type="table">
                /// <listheader>
                /// <term>Type</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>Plain</term>
                /// <term>Standard text input field</term>
                /// </item>
                /// <item>
                /// <term>Password</term>
                /// <term>Input field that masks the inputted characters</term>
                /// </item>
                /// </list>
                /// </remarks>
                public enum Type
                {
                    Plain,
                    Password
                };

                /// <summary>
                /// Initializes the components of the input field to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                /// <param name="Type">The type of the input field, as described below:
                /// <list type="table">
                /// <listheader>
                /// <term>Type</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>Plain</term>
                /// <term>Standard text input field</term>
                /// </item>
                /// <item>
                /// <term>Password</term>
                /// <term>Input field that masks the inputted characters</term>
                /// </item>
                /// </list>
                /// The string values to determine the field type are as follows
                /// <list type="table">
                /// <listheader>
                /// <term>String Value</term>
                /// <term> InputType</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Plain</term>
                /// </item>
                /// <item>
                /// <term>plain</term>
                /// <term>Plain</term>
                /// </item>
                /// <item>
                /// <term>password</term>
                /// <term>Password</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="Padding">The amount of padding the text will have from the interior sides of the input field.</param>
                /// <param name="MaxLength">The maximum character length the fiels will hold (0 for unlimited)</param>
                /// <param name="Children">The XML node list containing the child elements that compose the style of the button (see XML notation docs for further information).</param>
                /// <param name="X">The x positioning of the list relative to the parent as governed by the horizontal alignment as follows:
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
                /// <param name="Y">The y positioning of the list relative to the parent as governed by the vertical alignment as follows:
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
                /// <param name="W">The width of the list.</param>
                /// <param name="H">The height of the list.</param>
                /// <param name="HAlign">The horizontal alignemnt of the list within the parent rect as specified by the following values:
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
                /// <param name="VAlign">The horizontal alignemnt of the list within the parent rect  as specified by the following values:
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
                /// Add a password input field with an id of "myPassword", no padding or maximum length (child nodes omitted for clarity) spaced 10% from the left edge of the parent rect, measuring 100x100 virtual units and centered vertically
                /// <code>
                /// Input input = new Input("myPassword", "password", null, null, xmlChildren, "10%", null, "100", "100", null, "center");
                /// </code>
                /// </example>
                public Input(StyleSheet Styling, string ID, string Type, string MaxLength, XmlNodeList Children, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    if (MaxLength != null && MaxLength != "") maxLength = int.Parse(MaxLength);
                    else maxLength = 0;

                    foreach (XmlNode element in Children)
                    {
                        string style = Xml.Att(element.Attributes["style"]);

                        switch (element.Name)
                        {


                            case "text":

                                text = new Text(element.InnerText,
                                                            "middleLeft",
                                                             null,
                                                             Styling.Apply(style, "colour", element.Attributes),
                                                             Styling.Apply(style, "fontFace", element.Attributes),
                                                             Styling.Apply(style, "fontSize", element.Attributes));

                                textUnfocus = Colour.Parse(Styling.Apply(style, "unfocusColour", element.Attributes));
                                padding = new Unit(Styling.Apply(style, "padding", element.Attributes));

                                break;

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

                                backgroundUnfocus = Colour.Parse(Styling.Apply(style, "unfocusColour", element.Attributes));

                                if (backgroundUnfocus != null && (id == null || id == "")) throw new Exception("Input id is required for focus states");

                                break;

                            case "icon":

                                icon = new Image(null,
                                                 Styling.Apply(style, "src", element.Attributes),
                                                 Styling.Apply(style, "colour", element.Attributes),
                                                 null,
                                                 "0",
                                                 "0",
                                                 "0px",
                                                 "0px",
                                                 "right",
                                                 null,
                                                 null);

                                break;

                            default: throw new Exception("Input has unexpected element: " + element.Name);
                        }

                        if (text == null) throw new Exception("Input text is required");
                        if (padding.value == null) padding = new Unit("2.5%");
                    }

                    switch (Type)
                    {
                        case "":

                            type = Input.Type.Plain;
                            break;

                        case null:

                            type = Input.Type.Plain;
                            break;

                        case "plain":

                            type = Input.Type.Plain;
                            break;

                        case "password":

                            type = Input.Type.Password;
                            break;

                        default:

                            throw new Exception(string.Format("Unknown input type: {0}", Type));
                    }
                }

                /// <summary>
                /// Presents the input field visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.Rect rPx = rect.ToPixelRect(VirtualScale, Parent);
                    UnityEngine.GUIStyle style = text.CalculateGUIStyle(VirtualScale);

                    style.clipping = UnityEngine.TextClipping.Clip;
                    style.normal.background = null;
                    style.active.background = null;
                    style.hover.background = null;
                    style.focused.background = null;

                    bool focus = UnityEngine.GUI.GetNameOfFocusedControl() == id;

                    if (background != null)
                    {
                        if (!focus && backgroundUnfocus != null)
                        {
                            UnityEngine.Color? orig = background.rgba;
                            background.rgba = backgroundUnfocus;
                            background.Present(VirtualScale, rPx);
                            background.rgba = orig;
                        }
                        else background.Present(VirtualScale, rPx);
                    }

                    if (icon != null)
                    {
                        // Resize the icon so the height matches the input box height whilst preserving the aspect ratio
                        float aspect = (float)icon.img.width / (float)icon.img.height;
                        icon.rect.dim.y.value = rPx.height;
                        icon.rect.dim.x.value = rPx.height * aspect;

                        icon.Present(1.0f, rPx);

                        // Reduce the text area of the input box by the width of the icon
                        rPx.width -= (float)icon.rect.dim.x.value;
                    }

                    // Left and right padding of text from sides of input box
                    float xOffset = padding.ToPixel(VirtualScale, rPx.width);
                    rPx.x += xOffset;
                    rPx.width -= xOffset * 2;

                    // Focus and unfocus colour
                    if (focus && text.colour != null) style.normal.textColor = (UnityEngine.Color)text.colour;
                    else if (textUnfocus != null) style.normal.textColor = (UnityEngine.Color)textUnfocus;

                    UnityEngine.GUI.SetNextControlName(id);

                    // Cursor colour
                    UnityEngine.Color cursOrig = UnityEngine.GUI.skin.settings.cursorColor;
                    UnityEngine.GUI.skin.settings.cursorColor = style.normal.textColor;

                    // Normal input box
                    if (type == Type.Plain)
                    {
                        if (maxLength <= 0) text.text = UnityEngine.GUI.TextField(rPx, text.text, style);
                        else text.text = UnityEngine.GUI.TextField(rPx, text.text, maxLength, style);
                    }
                    // Password input box
                    else if (type == Type.Password)
                    {
                        if (maxLength <= 0) text.text = UnityEngine.GUI.PasswordField(rPx, text.text, "*"[0], style);
                        else text.text = UnityEngine.GUI.PasswordField(rPx, text.text, "*"[0], maxLength, style);
                    }

                    UnityEngine.GUI.skin.settings.cursorColor = cursOrig;
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Input) type: {0}, padding: {1}, maxLength: {2}, text: {3}, textUnfocus: {4}, background: {5}, backgroundUnfocus: {6}, icon: {7}, {8}", type, padding, maxLength, DebugInfo(text), textUnfocus, DebugInfo(background), backgroundUnfocus, DebugInfo(icon), base.DebugInfo());
                }

                /// <summary>
                /// Getter/setter for the _text member.
                /// </summary>
                /// <remarks>_text cannot be null. Wordwrap is disabled.</remarks>
                public Text text
                {
                    get { return _text; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Input text member cannot be null (id: {0})", id));

                        _text = value;
                        _text.textAlign = UnityEngine.TextAnchor.MiddleLeft;
                    }
                }

                /// <summary>
                /// Getter/setter for the _background member.
                /// </summary>
                /// <remarks>_background can be null. Image spans 100% of the containing unit rect.</remarks>
                public Image background
                {
                    get { return _background; }

                    set
                    {
                        _background = value;

                        if (_background != null)
                        {
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
                }

                /// <summary>
                /// Getter/setter for the _icon member.
                /// </summary>
                /// <remarks>_icon can be null. Dimensions are stripped out as these will be calculated dynamically. Right aligned.</remarks>
                public Image icon
                {
                    get { return _icon; }

                    set
                    {
                        _icon = value;

                        if (_icon != null)
                        {
                            _icon.rect.pos.x.type = Unit.Type.Virtual;
                            _icon.rect.pos.x.value = 0;
                            _icon.rect.pos.y.type = Unit.Type.Virtual;
                            _icon.rect.pos.y.value = 0;

                            _icon.rect.dim.x.type = Unit.Type.Pixel;
                            _icon.rect.dim.x.value = 0;
                            _icon.rect.dim.y.type = Unit.Type.Pixel;
                            _icon.rect.dim.y.value = 0;

                            _icon.rect.hAlign = new Alignment(Alignment.Type.Horizontal, Alignment.Direction.Right);
                            _icon.rect.vAlign = new Alignment(null);
                        }
                    }
                }

                /// <summary>
                /// Input field text styling
                /// </summary>
                protected Text _text;

                /// <summary>
                /// Text padding from sides of input field
                /// </summary>
                public Unit padding;

                /// <summary>
                /// Maximum character length of input string (0 for unlimited)
                /// </summary>
                public int maxLength;

                /// <summary>
                /// Input field type
                /// </summary>
                public Type type;

                /// <summary>
                /// Background image of input field
                /// </summary>
                protected Image _background;

                /// <summary>
                /// Icon (search, password reveal, etc.) aligned to right of input field
                /// </summary>
                protected Image _icon;

                /// <summary>
                /// Colour of background when field is not focus widget
                /// </summary>
                public UnityEngine.Color? backgroundUnfocus;

                /// <summary>
                /// COlour of text when field is not focus widget
                /// </summary>
                public UnityEngine.Color? textUnfocus;
            }
        }
    } 
}