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
            /// Widget for displaying collections of widgets on tabbed pages.
            /// </summary>
            public class Tab : Presentable
            {
                /// <summary>
                /// Initializes the components of the tab widget to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                /// <param name="MaxTabWidth">The maximum (non-zero) width a page tab can be (can be null for unconstrained width)</param>
                /// <param name="Parent">The parent layout (used for adding and validating page/widget ids)</param>
                /// <param name="Children">The XML node list containing the child elements that compose the style of the button (see XML notation docs for further information).</param>
                /// <param name="X">The x positioning of the tab container rect relative to the parent as governed by the horizontal alignment as follows:
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
                /// <param name="Y">The y positioning of the tab container rect relative to the parent as governed by the vertical alignment as follows:
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
                /// <param name="W">The width of the tab container rect.</param>
                /// <param name="H">The height of the tab container rect.</param>
                /// <param name="HAlign">The horizontal alignemnt of the tab container rect within the parent rect as specified by the following values:
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
                /// <param name="VAlign">The horizontal alignemnt of the tab container rect within the parent rect  as specified by the following values:
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
                public Tab(StyleSheet Styling, string ID, string MaxTabWidth, Layout Parent, XmlNodeList Children, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    maxTabWidth = new Unit(MaxTabWidth);

                    foreach (XmlNode element in Children)
                    {
                        string style = Xml.Att(element.Attributes["style"]);

                        // Tab header for defining tab sizes, backgrounds etc.
                        if (element.Name == "header")
                        {
                            headerHeight = new Unit(Styling.Apply(style, "height", element.Attributes));
                            maxTabWidth = new Unit(Styling.Apply(style, "maxW", element.Attributes));

                            foreach (XmlNode hChild in element.ChildNodes)
                            {
                                style = Xml.Att(element.Attributes["style"]);

                                switch (hChild.Name)
                                {
                                    case "background":

                                        headerBackground = new Image(null,
                                          Styling.Apply(style, "src", hChild.Attributes),
                                                       Styling.Apply(style, "colour", hChild.Attributes),
                                                       Styling.Apply(style, "scale", hChild.Attributes),
                                          "0",
                                          "0",
                                          "100%",
                                          "100%",
                                          null,
                                          null, 
                                          null);

                                        break;

                                    case "active":

                                        Image imgA = new Image(null,
                                          Styling.Apply(style, "src", hChild.Attributes),
                                                       Styling.Apply(style, "colour", hChild.Attributes),
                                                       Styling.Apply(style, "scale", hChild.Attributes),
                                          "0",
                                          "0",
                                          "100%",
                                          "100%",
                                          null,
                                          null,
                                          null);

                                        activeTab = new Button(null,
                                           null,
                                           null,
                                           imgA,
                                           "0",
                                           "0",
                                           "100%",
                                           "100%",
                                           null,
                                           null,
                                           Xml.Att(element.Attributes["enabled"]),
                                           null);

                                        activeTab.guiDispatcher.AttachHandler("onClick", this, "tab_Click");

                                        break;

                                    case "inactive":

                                        Image imgI = new Image(null,
                                           Styling.Apply(style, "src", hChild.Attributes),
                                                       Styling.Apply(style, "colour", hChild.Attributes),
                                                       Styling.Apply(style, "scale", hChild.Attributes),
                                          "0",
                                          "0",
                                          "100%",
                                          "100%",
                                          null,
                                          null,
                                          null);

                                        inactiveTab = new Button(null,
                                           null,
                                           null,
                                           imgI,
                                           "0",
                                           "0",
                                           "100%",
                                           "100%",
                                           null,
                                           null,
                                           Xml.Att(element.Attributes["enabled"]),
                                           null);

                                        inactiveTab.guiDispatcher.AttachHandler("onClick", this, "tab_Click");

                                        break;

                                    case "text":

                                        tabText = new Text(null,
                                                "middleCenter",
                                                null,
                                                Styling.Apply(style, "colour", element.Attributes),
                                                             Styling.Apply(style, "fontFace", hChild.Attributes),
                                                             Styling.Apply(style, "fontSize", hChild.Attributes));

                                        break;

                                    default: throw new Exception(string.Format("Encountered unknown tab child: {0}", element.Name));
                                }
                            }
                        }
                        else if (element.Name == "body")
                        {
                            bodyHeight = new Unit(Styling.Apply(style, "height", element.Attributes));

                            pages = new PageContainer(Xml.Att(element.Attributes["id"]), Xml.Att(element.Attributes["title"]));

                            pages.AddChildren(element.ChildNodes, Parent);

                            pages.ActivatePage(Xml.Att(element.FirstChild.Attributes["id"]));
                        }
                        else throw new Exception("Unexpected child node in tab widget: " + element.Name);
                    }

                    if ((headerHeight == null || headerHeight.value == null) && (bodyHeight == null || bodyHeight.value == null)) throw new Exception("Tab must have either header or body element height specified");
                    if (activeTab == null) throw new Exception("Tab must have active tab specified");
                    if (inactiveTab == null) throw new Exception("Tab must have inactive tab specified");
                }

                protected void tab_Click(object sender, Events.Event e)
                {
                    pages.ActivatePage(e.properties.Get("id"));
                }

                /// <summary>
                /// Presents the tab widget visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    // Bounding pixel rect of entire tab widget container
                    UnityEngine.Rect rPx = rect.ToPixelRect((float)VirtualScale, (UnityEngine.Rect)Parent);

                    // Bounding pixel rects for the tab header and body elements
                    UnityEngine.Rect pxHeader, pxBody;

                    // Header height null, deduce from body height
                    if (headerHeight != null && headerHeight.value != null)
                    {
                        UnitRect rectHeader = new UnitRect(new Unit(0, Unit.Type.Virtual), new Unit(0, Unit.Type.Virtual), new Unit(rPx.width, Unit.Type.Pixel), headerHeight, null, null);

                        pxHeader = rectHeader.ToPixelRect(VirtualScale, rPx);
                        pxBody = new UnityEngine.Rect(rPx.x, rPx.y + pxHeader.height, rPx.width, rPx.height - pxHeader.height);
                    }
                    // Body height null, deduce from header height?
                    else if (bodyHeight != null && bodyHeight.value != null)
                    {
                        UnitRect rectBody = new UnitRect(new Unit(0, Unit.Type.Virtual), new Unit(0, Unit.Type.Virtual), new Unit(rPx.width, Unit.Type.Pixel), bodyHeight, null, null);

                        pxBody = rectBody.ToPixelRect(VirtualScale, rPx);
                        pxHeader = new UnityEngine.Rect(rPx.x, rPx.y + (rPx.height - pxBody.height), rPx.width, rPx.height - pxBody.height);
                        pxHeader.y += rPx.height - pxBody.height;
                    }
                    else throw new Exception("Tab must have either header or body element height specified");

                    // Header background (not tabs)
                    if (headerBackground != null) headerBackground.Present(VirtualScale, pxHeader);

                    // Container pixel width for a given tab
                    float tabContainerW = pxHeader.width / pages.pages.Count;

                    // Actual tab width as constrained by maxTabWidth
                    float tabW = tabContainerW;

                    // Attempt to constrain tab width
                    if (maxTabWidth != null && maxTabWidth.value != null && maxTabWidth.value >= 0)
                    {
                        float pxMaxTabWidth = maxTabWidth.ToPixel(VirtualScale, rPx.width);
                        tabW = pxMaxTabWidth < tabW ? pxMaxTabWidth : tabW;
                    }

                    int i = 0;

                    foreach (KeyValuePair<string, Page> p in pages.pages)
                    {
                        // Tab container (actual tab rect will be centered inside of this)
                        float x = pxHeader.x + (i * tabContainerW);
                        float y = pxHeader.y;
                        float w = tabContainerW;
                        float h = pxHeader.height;
                        UnityEngine.Rect pxTabContainer = new UnityEngine.Rect(x, y, w, h);

                        // Actual tab
                        UnitRect rectTab = new UnitRect(new Unit(0, Unit.Type.Pixel), new Unit(0, Unit.Type.Pixel), new Unit(tabW, Unit.Type.Pixel), new Unit(100, Unit.Type.Percentage), new Alignment(Alignment.Type.Horizontal, Alignment.Direction.Center), null);
                        UnityEngine.Rect pxTab = rectTab.ToPixelRect(VirtualScale, pxTabContainer);

                        // Active tabs
                        if (pages.activePage.title == p.Value.title)
                        {
                            if (tabText != null)
                            {
                                tabText.text = p.Value.title;
                                activeTab.text = tabText;
                            }

                            activeTab.guiDispatcher.SetEventArg("onClick", "id", p.Value.id);
                            activeTab.Present(VirtualScale, pxTab);
                        }
                        // Inactive tab
                        else
                        {
                            if (tabText != null)
                            {
                                tabText.text = p.Value.title;
                                inactiveTab.text = tabText;
                            }

                            inactiveTab.guiDispatcher.SetEventArg("onClick", "id", p.Value.id);
                            inactiveTab.Present(VirtualScale, pxTab);
                        }

                        i++;
                    }

                    if(pages.activePage.visible) pages.activePage.Present(VirtualScale, pxBody);
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Tab) maxTabWidth: {0}, headerHeight: {1}, bodyHeight: {2}, headerBackground: {3}, activeTab: {4}, inactiveTab: {5}, tabText: {6}, pages: {7}, {8}", maxTabWidth, headerHeight, bodyHeight, DebugInfo(headerBackground), DebugInfo(activeTab), DebugInfo(inactiveTab), DebugInfo(tabText), DebugInfo(pages), base.DebugInfo());
                }

                /// <summary>
                /// Getter/setter for the _maxTabWidth member.
                /// </summary>
                /// <remarks>_maxTabWidth can be null. Values less than or equal to 0 are invalid.</remarks>
                public Unit maxTabWidth
                {
                    get { return _maxTabWidth; }

                    set
                    {
                        if (value != null && value.value <= 0) throw new Exception(string.Format("Tab max width must either be null or greater than 0 (id: {0})", id));

                        _maxTabWidth = value;
                    }
                }

                /// <summary>
                /// Getter/setter for the _headerHeight member.
                /// </summary>
                /// <remarks>_headerHeight can be null. Values less than or equal to 0 are invalid. If _headerHeight is non-zero, _bodyHeight will be nulled.</remarks>
                public Unit headerHeight
                {
                    get { return _headerHeight; }

                    set
                    {
                        if (value != null && value.value != null)
                        {
                            if (value.value <= 0) throw new Exception(string.Format("Tab header height must either be null or greater than 0 (id: {0})", id));

                            _bodyHeight = null;
                        }

                        _headerHeight = value;
                    }
                }

                /// <summary>
                /// Getter/setter for the _bodyHeight member.
                /// </summary>
                /// <remarks>_bodyHeight can be null. Values less than or equal to 0 are invalid. If _bodyHeight is non-zero, _headerHeight will be nulled.</remarks>
                public Unit bodyHeight
                {
                    get { return _bodyHeight; }

                    set
                    {
                        if (value != null && value.value != null)
                        {
                            if (value.value <= 0) throw new Exception(string.Format("Tab body height must either be null or greater than 0 (id: {0})", id));

                            _headerHeight = null;
                        }

                        _bodyHeight = value;
                    }
                }

                /// <summary>
                /// Getter/setter for the _headerBackground member.
                /// </summary>
                /// <remarks>_headerBackground can be null. Image spans 100% of the containing unit rect.</remarks>
                public Image headerBackground
                {
                    get { return _headerBackground; }

                    set
                    {
                        _headerBackground = value;

                        if (_headerBackground != null)
                        {
                            _headerBackground.rect.pos.x.type = Unit.Type.Virtual;
                            _headerBackground.rect.pos.x.value = 0;
                            _headerBackground.rect.pos.y.type = Unit.Type.Virtual;
                            _headerBackground.rect.pos.y.value = 0;

                            _headerBackground.rect.dim.x.type = Unit.Type.Percentage;
                            _headerBackground.rect.dim.x.value = 100;
                            _headerBackground.rect.dim.y.type = Unit.Type.Percentage;
                            _headerBackground.rect.dim.y.value = 100;

                            _headerBackground.rect.hAlign = new Alignment(null);
                            _headerBackground.rect.vAlign = new Alignment(null);
                        }
                    }
                }

                /// <summary>
                /// Getter/setter for the _activeTab member.
                /// </summary>
                /// <remarks>_activeTab cannot be null. Button spans 100% of the containing unit rect.</remarks>
                public Button activeTab
                {
                    get { return _activeTab; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Tab active tab member cannot be null (id: {0})", id));

                        _activeTab = value;
                        _activeTab.rect.pos.x.type = Unit.Type.Virtual;
                        _activeTab.rect.pos.x.value = 0;
                        _activeTab.rect.pos.y.type = Unit.Type.Virtual;
                        _activeTab.rect.pos.y.value = 0;

                        _activeTab.rect.dim.x.type = Unit.Type.Percentage;
                        _activeTab.rect.dim.x.value = 100;
                        _activeTab.rect.dim.y.type = Unit.Type.Percentage;
                        _activeTab.rect.dim.y.value = 100;

                        _activeTab.rect.hAlign = new Alignment(null);
                        _activeTab.rect.vAlign = new Alignment(null);

                    }
                }

                /// <summary>
                /// Getter/setter for the _inactiveTab member.
                /// </summary>
                /// <remarks>_inactiveTab cannot be null. Button spans 100% of the containing unit rect.</remarks>
                public Button inactiveTab
                {
                    get { return _inactiveTab; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Tab inactive tab member cannot be null (id: {0})", id));

                        _inactiveTab = value;
                        _inactiveTab.rect.pos.x.type = Unit.Type.Virtual;
                        _inactiveTab.rect.pos.x.value = 0;
                        _inactiveTab.rect.pos.y.type = Unit.Type.Virtual;
                        _inactiveTab.rect.pos.y.value = 0;

                        _inactiveTab.rect.dim.x.type = Unit.Type.Percentage;
                        _inactiveTab.rect.dim.x.value = 100;
                        _inactiveTab.rect.dim.y.type = Unit.Type.Percentage;
                        _inactiveTab.rect.dim.y.value = 100;

                        _inactiveTab.rect.hAlign = new Alignment(null);
                        _inactiveTab.rect.vAlign = new Alignment(null);

                    }
                }

                /// <summary>
                /// Getter/setter for the _tabText member.
                /// </summary>
                /// <remarks>_tabText can be null. Text is aligned to middle-center and wordwrap is disabled.</remarks>
                public Text tabText
                {
                    get { return _tabText; }

                    set
                    {
                        _tabText = value;

                        if (_tabText != null)
                        {
                            _tabText.textAlign = UnityEngine.TextAnchor.MiddleCenter;
                            _tabText.wordWrap = false;
                        }
                    }
                }

                /// <summary>
                /// The maximum width of a given tab (if null or less than 1, no maximum width is used)
                /// </summary>
                protected Unit _maxTabWidth;

                /// <summary>
                /// The height of the tab header area
                /// </summary>
                protected Unit _headerHeight;

                /// <summary>
                /// The height of the tab body area
                /// </summary>
                protected Unit _bodyHeight;

                /// <summary>
                /// The background image for the tab header area
                /// </summary>
                protected Image _headerBackground;

                /// <summary>
                /// The button style for the active tab
                /// </summary>
                protected Button _activeTab;

                /// <summary>
                /// The button style for the inactive tab(s)
                /// </summary>
                protected Button _inactiveTab;

                /// <summary>
                /// Text styling for tab button labels
                /// </summary>
                protected Text _tabText;

                /// <summary>
                /// The pages for the specified tabs
                /// </summary>
                public PageContainer pages;
            }
        }
    } 
}