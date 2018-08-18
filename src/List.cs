using System;
using System.Xml;
using System.Collections.Generic;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Widget for displaying list items.
            /// </summary>
            public class List : Presentable
            {
                /// <summary>
                /// Initializes the components of the list to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
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
                /// Instantiate a list object with a null id (child nodes omitted for clarity) spaced 10% from the left edge of the parent rect, measuring 800x500 virtual units:
                /// <code>
                /// List list = new List(null, xmlChildren, "10%", null, "800", "500", null, null);
                /// </code>
                /// </example>
                public List(StyleSheet Styling, string ID, XmlNodeList Children, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    items = new Dictionary<string, Button>();

                    foreach (XmlNode element in Children)
                    {
                        string style = Xml.Att(element.Attributes["style"]);

                        switch (element.Name)
                        {
                            // Text styling
                            case "text":

                                _text = new Button(null, null, null, null, null, null, null, null, null, null, null, null);

                                text = new Text(null,
                                        "middleLeft",
                                        null,
                                        Styling.Apply(style, "colour", element.Attributes),
                                                             Styling.Apply(style, "fontFace", element.Attributes),
                                                             Styling.Apply(style, "fontSize", element.Attributes));

                                _text.textDisabled = Colour.Parse(Xml.Att(element.Attributes["disabledColour"]));

                                break;

                            // Icon (bullet) image
                            case "icon":

                                icon = new Image(null,
                                  Styling.Apply(style, "src", element.Attributes),
                                                       Styling.Apply(style, "colour", element.Attributes),
                                                       Styling.Apply(style, "scale", element.Attributes),
                                  "0",
                                  "0",
                                  "50%",
                                 "50%",
                                  "center",
                                  "center",
                                  null);

                                break;

                            // List item
                            case "li":

                                Button item = new Button(null, text, null, null, null, null, null, null, null, null, Xml.Att(element.Attributes["enabled"]), Xml.Att(element.Attributes["visible"]));
                                item.text.text = element.InnerText;

                                items.Add(Xml.Att(element.Attributes["key"]), item);
                                break;

                            default: throw new Exception(string.Format("Encountered unknown list child: {0}", element.Name));
                        }
                    }

                    if (text == null) throw new Exception(string.Format("List text member cannot be null (id: {0})", id));

                    _guiDispatcher = new GUIEventDispatcher(/*this*/);
                }

                public void AddItem(string Key, string Value)
                {
                    if (items.ContainsKey(Key)) throw new Exception("AddItem already contains key " + Key);

                    Button item = new Button(null, text, null, null, null, null, null, null, null, null, null, null);
                    item.text.text = Value;

                    items.Add(Key, item);
                }

                public Button RetrieveItem(string Key)
                {
                    if (!items.ContainsKey(Key)) throw new Exception("RetrieveItem list does not contain key " + Key);

                    return items[Key];  
                }

                public void ClearItems()
                {
                    items.Clear();
                }

                /// <summary>
                /// Presents the list visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.Rect rPx = rect.ToPixelRect(VirtualScale, Parent);
                    //UnityEngine.GUIStyle style = text.CalculateGUIStyle(VirtualScale);

                    //// Line height of text item
                    //text.text = "Dummy";
                    //float lineHeight = text.CalculatePixelSize(VirtualScale).y;

                    //// Icon dimensions are square and of the same dimensions as the line height
                    //UnitRect rectIcon = new UnitRect(null, null, new Unit(lineHeight, Unit.Type.Pixel), new Unit(lineHeight, Unit.Type.Pixel), null, null);
                    //UnityEngine.Rect pxIcon = rectIcon.ToPixelRect(VirtualScale, rPx);

                    float yOffset = 0;

                    foreach (KeyValuePair<string, Button> item in items)
                    {
                        UnityEngine.GUIStyle style = item.Value.text.CalculateGUIStyle(VirtualScale);

                        style.normal.textColor = item.Value.enabled ? text.colour ?? style.normal.textColor : _text.textDisabled ?? (text.colour ?? style.normal.textColor);

                        // Line height of text item
                        float lineHeight = item.Value.text.CalculatePixelSize(VirtualScale).y;

                        // Icon dimensions are square and of the same dimensions as the line height
                        UnitRect rectIcon = new UnitRect(null, null, new Unit(lineHeight, Unit.Type.Pixel), new Unit(lineHeight, Unit.Type.Pixel), null, null);
                        UnityEngine.Rect pxIcon = rectIcon.ToPixelRect(VirtualScale, rPx);

                        pxIcon.y += yOffset;

                        // Text item
                        UnityEngine.Rect pxItem = new UnityEngine.Rect(pxIcon);
                        //text.text = item.Value;

                        if (icon != null)
                        {
                            // Icon
                            if(item.Value.visible) icon.Present(VirtualScale, pxIcon);

                            // Offset the text item by one icon's worth of distance
                            pxItem.x += lineHeight;
                        }


                        // Text item width
                        pxItem.width = item.Value.text.CalculatePixelSize(VirtualScale).x;

                        if (item.Value.visible && UnityEngine.GUI.Button(pxItem, item.Value.text.text, style) && item.Value.enabled)
                        {
                            guiDispatcher.SetEventArg("onClick", "key", item.Key);
                            guiDispatcher.SetEventArg("onClick", "value", item.Value.text.text);
                            guiDispatcher.Dispatch("onClick", this);
                        }

                        // Offset next item by one line 
                        yOffset += lineHeight * 1.5f;
                    }
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(List) item count: {0}, text: {1}, icon:{2}, {3}", items.Count, DebugInfo(text), DebugInfo(icon), base.DebugInfo());
                }

                /// <summary>
                /// Getter/setter for the _icon member.
                /// </summary>
                /// <remarks>_text cannot be null. Text is center-left aligned with wordwrap disabled.</remarks>
                public Text text
                {
                    get { return _text.text; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("List text member cannot be null (id: {0})", id));

                        _text.text = value;
                        _text.text.textAlign = UnityEngine.TextAnchor.MiddleLeft;
                        _text.text.wordWrap = false;
                    }
                }

                /// <summary>
                /// Getter/setter for the _icon member.
                /// </summary>
                /// <remarks>_icon can be null. Image is centered horizontally and vertically, spanning 50% of the containing unit rect.</remarks>
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

                            _icon.rect.dim.x.type = Unit.Type.Percentage;
                            _icon.rect.dim.x.value = 50;
                            _icon.rect.dim.y.type = Unit.Type.Percentage;
                            _icon.rect.dim.y.value = 50;

                            _icon.rect.hAlign.dir = Alignment.Direction.Center;
                            _icon.rect.hAlign.dir = Alignment.Direction.Center;
                        }
                    }
                }

                public GUIEventDispatcher guiDispatcher
                {
                    get { return _guiDispatcher; }
                }

                /// <summary>
                /// List item icon
                /// </summary>
                protected Image _icon;

                /// <summary>
                /// List item text formatting
                /// </summary>
                protected Button _text;

                /// <summary>
                /// List items
                /// </summary>
                protected Dictionary<string, Button> items;

                protected GUIEventDispatcher _guiDispatcher;
            }
        }
    } 
}