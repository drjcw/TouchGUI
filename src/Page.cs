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
            /// Widget representing a page within the GUI layout.
            /// </summary>
            public class Page : Presentable
            {
                /// <summary>
                /// Initializes the components of the page widget to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                /// <param name="Title">The title (if any) of this page.</param>
                public Page(string ID, string Title, string Visible) : base(ID, "0", "0", "100%", "100%", null, null, null)
                {
                    title = Title;
                    widgets = new List<Presentable>();
                    _guiDispatcher = new GUIEventDispatcher(/*this*/);
                }

                public Page(string ID, string Title, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    title = Title;
                    widgets = new List<Presentable>();
                    _guiDispatcher = new GUIEventDispatcher(/*this*/);
                }

                /// <summary>
                /// Parses the supplied XML node for widgets and adds them to this page.
                /// </summary>
                /// <param name="XMLWidget">The widget (in XML format) to add.</param>
                /// <param name="Parent">The parent layout this page belongs to.</param>
                /// <remarks>This constructor is used primarily in conjunction with XML-based layouts</remarks>
                public void AddWidget(XmlNode XMLWidget, Layout Parent)
                {
                    Presentable w = null;
                    string style = Xml.Att(XMLWidget.Attributes["style"]);

                    switch (XMLWidget.Name)
                    {
                        case "img":

                            w = new Image(Xml.Att(XMLWidget.Attributes["id"]),
                                          Parent.styleSheet.Apply(style, "src", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "colour", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "scale", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "x", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "y", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "width", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "height", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignH", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignV", XMLWidget.Attributes),
                                          Xml.Att(XMLWidget.Attributes["visible"]));
                            break;

                        case "label":

                            w = new Label(Xml.Att(XMLWidget.Attributes["id"]),
                                          XMLWidget.InnerText,
                                          Xml.Att(XMLWidget.Attributes["textAlign"]),
                                          Xml.Att(XMLWidget.Attributes["wordWrap"]),
                                          Parent.styleSheet.Apply(style, "colour", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "fontFace", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "fontSize", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "x", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "y", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "width", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "height", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignH", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignV", XMLWidget.Attributes),
                                          Xml.Att(XMLWidget.Attributes["visible"]));
                            break;

                        case "input":

                            w = new Input(Parent.styleSheet,
                                          Xml.Att(XMLWidget.Attributes["id"]),
                                          Xml.Att(XMLWidget.Attributes["type"]),
                                          Parent.styleSheet.Apply(style, "maxLength", XMLWidget.Attributes),
                                          XMLWidget.ChildNodes,
                                          Parent.styleSheet.Apply(style, "x", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "y", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "width", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "height", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignH", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignV", XMLWidget.Attributes),
                                          Xml.Att(XMLWidget.Attributes["visible"]));

                            break;

                        case "button":

                            Button b = new Button(Parent.styleSheet, Xml.Att(XMLWidget.Attributes["id"]),
                                           XMLWidget.ChildNodes,
                                           Parent.styleSheet.Apply(style, "x", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "y", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "width", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "height", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignH", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignV", XMLWidget.Attributes),
                                          Xml.Att(XMLWidget.Attributes["enabled"]),
                                          Xml.Att(XMLWidget.Attributes["visible"]));

                            b.guiDispatcher.AttachHandler("onClick", Parent.handler, Xml.Att(XMLWidget.Attributes["onClick"]));
                            w = b;
                            break;

                        case "tab":

                            w = new Tab(Parent.styleSheet, Xml.Att(XMLWidget.Attributes["id"]),
                                        Parent.styleSheet.Apply(style, "maxW", XMLWidget.Attributes),
                                        Parent,
                                        XMLWidget.ChildNodes,
                                           Parent.styleSheet.Apply(style, "x", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "y", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "width", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "height", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignH", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignV", XMLWidget.Attributes),
                                          Xml.Att(XMLWidget.Attributes["visible"]));
                            break;

                        case "list":

                            List l = new List(Parent.styleSheet, Xml.Att(XMLWidget.Attributes["id"]),
                                        XMLWidget.ChildNodes,
                                           Parent.styleSheet.Apply(style, "x", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "y", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "width", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "height", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignH", XMLWidget.Attributes),
                                          Parent.styleSheet.Apply(style, "alignV", XMLWidget.Attributes),
                                          Xml.Att(XMLWidget.Attributes["visible"]));

                            l.guiDispatcher.AttachHandler("onClick", Parent.handler, Xml.Att(XMLWidget.Attributes["onClick"]));
                            w = l;

                            break;

                        default:

                            throw new Exception(string.Format("Encountered unknown widget: {0}", XMLWidget.Name));
                    }

                    widgets.Add(w);
                    Parent.RegisterId(w);
                }

                public void AddWidget(Presentable W, Layout Parent)
                {
                    widgets.Add(W);
                    Parent.RegisterId(W);
                }

                /// <summary>
                /// Presents the page (and child widgets) visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.Rect rPx = rect.ToPixelRect(VirtualScale, Parent);

                    foreach (Presentable widget in widgets)
                    {
                        if(widget.visible) widget.Present(VirtualScale, rPx);
                    }
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    System.Text.StringBuilder nfo = new System.Text.StringBuilder();

                    nfo.Append(string.Format("\n\tPage {0}, {1}, {2}:", id, title, base.DebugInfo()));

                    foreach (Presentable w in widgets)
                    {
                        nfo.Append(string.Format("\n\t\t{0}", w.DebugInfo()));
                    }

                    return nfo.ToString();
                }

                /// <summary>
                /// The optional title of this page
                /// </summary>
                public string title;

                /// <summary>
                /// The child widgets of this page
                /// </summary>
                public List<Presentable> widgets;






                public GUIEventDispatcher guiDispatcher
                {
                    get { return _guiDispatcher; }
                }

                protected GUIEventDispatcher _guiDispatcher;
            }
        }
    } 
}