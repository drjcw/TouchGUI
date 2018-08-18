using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Layout for a set of GUI pages.
            /// </summary>
            public class Layout : PageContainer
            {
                /// <summary>
                /// Populates the layout with the elements contained within the supplied XML GUI layout.
                /// </summary>
                /// <param name="XMLGUI">The string containing the XML GUI Layout.</param>
                public Layout(ref string XMLGUI, object Handler) : base(null, null)
                {
                    handler = Handler;
                    ids = new Dictionary<string, Thing>();
                    modalOverlay = new Image(null, null, "rgba(0,0,0,224)", null, null, null, "100%", "100%", null, null, null);
                    styleSheet = new StyleSheet();

                    // Skip comments
                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
                    xmlReaderSettings.IgnoreComments = true;

                    using (XmlReader xmlReader = XmlReader.Create(new StringReader(XMLGUI), xmlReaderSettings))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(xmlReader);
                        XmlNode xmlGui = xmlDoc.DocumentElement.SelectSingleNode("/layout");

                        id = Xml.Att(xmlGui.Attributes["id"]);
                        if (id == null) throw new Exception("Layout id can't be null or blank");
                        RegisterId(this);

                        rect.dim.x = new Unit(Xml.Att(xmlGui.Attributes["width"]));
                        rect.dim.y = new Unit(Xml.Att(xmlGui.Attributes["height"]));
                        rect.pos.x = new Unit(null);
                        rect.pos.y = new Unit(null);

                        rect.hAlign = new Alignment(Alignment.Type.Horizontal, Xml.Att(xmlGui.Attributes["alignH"]));
                        rect.vAlign = new Alignment(Alignment.Type.Vertical, Xml.Att(xmlGui.Attributes["alignV"]));

                        if (rect.dim.x.value == null || rect.dim.y.value == null) throw new Exception("Layout dimensions cannot be null");
                        if (rect.dim.x.type == Unit.Type.Percentage || rect.dim.y.type == Unit.Type.Percentage || rect.dim.x.type == Unit.Type.Pixel || rect.dim.y.type == Unit.Type.Pixel) throw new Exception("Layout dimensions cannot be pixel or percentages");

                        AddChildren(xmlDoc.DocumentElement.ChildNodes, this);
                    }
                }

                /// <summary>
                /// Registers an id with the master id list.
                /// </summary>
                /// <param name="ID">The unique id (if any) to register.</param>
                /// <remarks>Ids must be unique, but null or empty ids are permitted (and not registered)</remarks>
                public void RegisterId(Thing T)
                {
                    if (T.id != null && T.id != "")
                    {
                        if (ids.ContainsKey(T.id)) throw new Exception(string.Format("Layout already contains the unique id {0}", T.id));

                        ids.Add(T.id, T);
                    }
                }

                public Thing GetRegisteredWidget(string ID)
                {
                    if (!ids.ContainsKey(ID)) throw new Exception(string.Format("Layout does not contain the registered widget {0}", ID));

                    return ids[ID];
                }

                public void DisplayMessageBox(string Body, string Title, MessageBox.ButtonType Buttons)
                {
                    MessageBox mb = new MessageBox(this, Body, Title, Buttons);

                    mb.titleTopPadding = new Unit(styleSheet.Apply("__defaultMessageBox__", "titlePaddingTop"));
                    mb.bodyTopPadding = new Unit(styleSheet.Apply("__defaultMessageBox__", "bodyPaddingTop"));
                    mb.buttonTopPadding = new Unit(styleSheet.Apply("__defaultMessageBox__", "buttonPaddingTop"));

                    mb.rect.dim.x = new Unit(styleSheet.Apply("__defaultMessageBox__", "width"));
                    mb.rect.pos.y = new Unit(styleSheet.Apply("__defaultMessageBox__", "y"));
                    mb.rect.vAlign = new Alignment(Alignment.Type.Vertical, styleSheet.Apply("__defaultMessageBox__", "alignV"));


                    mb.labelTitle.text.font = new Font(styleSheet.Apply("__defaultMessageBox__", "titleFontFace"), styleSheet.Apply("__defaultMessageBox__", "titleFontSize"));
                    mb.labelBody.text.font = new Font(styleSheet.Apply("__defaultMessageBox__", "bodyFontFace"), styleSheet.Apply("__defaultMessageBox__", "bodyFontSize"));

                    mb.button.text.font = new Font(styleSheet.Apply("__defaultMessageBox__", "buttonFontFace"), styleSheet.Apply("__defaultMessageBox__", "buttonFontSize"));

                    mb.button.rect.dim.x = new Unit(styleSheet.Apply("__defaultMessageBox__", "buttonWidth"));
                    mb.button.rect.dim.y = new Unit(styleSheet.Apply("__defaultMessageBox__", "buttonHeight"));
                    mb.button.rect.pos.y = new Unit(styleSheet.Apply("__defaultMessageBox__", "buttonY"));

                    mb.button.background = new Image(null, styleSheet.Apply("__defaultMessageBox__", "buttonBackgroundSrc"), styleSheet.Apply("__defaultMessageBox__", "buttonBackgroundColour"), null, null, null, null, null, null, null, null);

                    mb.background = new Image(null, styleSheet.Apply("__defaultMessageBox__", "backgroundSrc"), null, null, null, null, null, null, null, null, null);

                    activeModal = mb;
                }

                public void ActivateModal(string ID)
                {
                    if (ID == null || ID == "") throw new Exception("id of desired modal to activate cannot be null or empty");
                    if (!pages.ContainsKey(ID)) throw new Exception("Desired modal to activate does not exist in collection: " + ID);

                    activeModal = pages[ID];
                    
                    activeModal.guiDispatcher.SetEventArg("onModalFocus", "current", activeModal.id);
                    activeModal.guiDispatcher.Dispatch("onModalFocus", this);
                }

                public void CloseActiveModal()
                {        
                    if(activeModal != null)
                    {
                        activeModal.guiDispatcher.SetEventArg("onModalUnfocus", "current", activeModal.id);
                        activeModal.guiDispatcher.Dispatch("onModalUnfocus", this);
                        activeModal = null;
                    }                    
                }

                public override void AddChildren(XmlNodeList Children, Layout Parent)
                {
                    foreach (XmlNode xmlElement in Children)
                    {
                        if (xmlElement.Name == "page") AddPage(Parent, xmlElement);
                        else if (xmlElement.Name == "stylesheet") styleSheet.Parse(xmlElement);
                        else if (xmlElement.Name == "messagebox") AddMessageBoxStyle(xmlElement);
                        else if (xmlElement.Name == "modal") AddModal(Parent, xmlElement);
                        else AddWidget(xmlElement, Parent);
                    }
                }

                protected void AddModal(Layout Parent, XmlNode XMLModal)
                {
                    string modalId = Xml.Att(XMLModal.Attributes["id"]);

                    AddPage(Parent, XMLModal);

                    Page modal = pages[modalId];

                    modal.rect.hAlign = new Alignment(Alignment.Type.Horizontal, Alignment.Direction.Center);
                    modal.rect.dim.x = new Unit(styleSheet.Apply("__defaultMessageBox__", "width"));
                    modal.rect.dim.y = new Unit(Xml.Att(XMLModal.Attributes["height"]));
                    modal.rect.pos.y = new Unit(styleSheet.Apply("__defaultMessageBox__", "y"));

                }

                protected void AddMessageBoxStyle(XmlNode XMLMessageBox)
                {
                    string name = Xml.Att(XMLMessageBox.Attributes["name"]);

                    // Empty style name is the same as "default"
                    if (name == null || name == "") name = "__defaultMessageBox__";

                    PropertyCollection msgStyle = new PropertyCollection();

                    string style = Xml.Att(XMLMessageBox.Attributes["style"]);

                    msgStyle.Set("width", styleSheet.Apply(style, "width", XMLMessageBox.Attributes));
                    msgStyle.Set("y", styleSheet.Apply(style, "y", XMLMessageBox.Attributes));
                    msgStyle.Set("alignV", styleSheet.Apply(style, "alignV", XMLMessageBox.Attributes));

                    foreach (XmlNode element in XMLMessageBox.ChildNodes)
                    {
                        style = Xml.Att(element.Attributes["style"]);

                        switch (element.Name)
                        {
                            case "title":

                                msgStyle.Set("titleFontSize", styleSheet.Apply(style, "fontSize", element.Attributes));
                                msgStyle.Set("titleFontFace", styleSheet.Apply(style, "fontFace", element.Attributes));
                                msgStyle.Set("titlePaddingTop", styleSheet.Apply(style, "paddingTop", element.Attributes));

                                break;

                            case "body":

                                msgStyle.Set("bodyFontSize", styleSheet.Apply(style, "fontSize", element.Attributes));
                                msgStyle.Set("bodyFontFace", styleSheet.Apply(style, "fontFace", element.Attributes));
                                msgStyle.Set("bodyPaddingTop", styleSheet.Apply(style, "paddingTop", element.Attributes));

                                break;

                            case "background":

                                msgStyle.Set("backgroundColour", styleSheet.Apply(style, "colour", element.Attributes));
                                msgStyle.Set("backgroundSrc", styleSheet.Apply(style, "src", element.Attributes));
                                break;

                            case "button":

                                msgStyle.Set("buttonPaddingTop", styleSheet.Apply(style, "paddingTop", element.Attributes));
                                msgStyle.Set("buttonY", styleSheet.Apply(style, "y", element.Attributes));
                                msgStyle.Set("buttonWidth", styleSheet.Apply(style, "width", element.Attributes));
                                msgStyle.Set("buttonHeight", styleSheet.Apply(style, "height", element.Attributes));

                                foreach (XmlNode child in element.ChildNodes)
                                {
                                    style = Xml.Att(child.Attributes["style"]);

                                    if (child.Name == "text")
                                    {
                                        //msgStyle.Set("buttonTextAlign", styleSheet.Apply(style, "textAlign", child.Attributes));
                                        msgStyle.Set("buttonFontSize", styleSheet.Apply(style, "fontSize", child.Attributes));
                                        msgStyle.Set("buttonFontFace", styleSheet.Apply(style, "fontFace", child.Attributes));
                                    }
                                    else if (child.Name == "background")
                                    {
                                        msgStyle.Set("buttonBackgroundSrc", styleSheet.Apply(style, "src", child.Attributes));
                                        msgStyle.Set("buttonBackgroundColour", styleSheet.Apply(style, "colour", child.Attributes));
                                        //msgStyle.Set("buttonBackgroundScale", styleSheet.Apply(style, "scale", child.Attributes));
                                    }
                                    else throw new Exception("Button has unexpected element: " + element.Name);
                                }

                                break;

                            default: throw new Exception(string.Format("Encountered unknown messagebox child: {0}", element.Name));
                        }
                    }

                    styleSheet.Add(name, msgStyle);
                }

                /// <summary>
                /// Presents the responsive GUI layout visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    if (!visible || activePage == null) return;

                    // Figure out which aspect ratio to mazimise the use of screen real estate
                    float aspW = Parent.width / (float)rect.dim.x.value;
                    float aspH = Parent.height / (float)rect.dim.y.value;
                    float aspect = aspW < aspH ? aspW : aspH;
                    float physW = (float)rect.dim.x.value * aspect;
                    float physH = (float)rect.dim.y.value * aspect;
                    float offsetX;
                    float offsetY;

                    // Horizontal alignment
                    if (rect.hAlign.dir == Alignment.Direction.Left) offsetX = 0;
                    else if (rect.hAlign.dir == Alignment.Direction.Center) offsetX = (Parent.width - physW) * 0.5f;
                    else if (rect.hAlign.dir == Alignment.Direction.Right) offsetX = Parent.width - physW;
                    else offsetX = 0;

                    // Vertical alignment
                    if (rect.vAlign.dir == Alignment.Direction.Top) offsetY = 0;
                    else if (rect.vAlign.dir == Alignment.Direction.Center) offsetY = (Parent.height - physH) * 0.5f;
                    else if (rect.vAlign.dir == Alignment.Direction.Bottom) offsetY = Parent.height - physH;
                    else offsetY = 0;

                    // The parent rect that all other GUI elements will be contained within
                    UnityEngine.Rect parent = new UnityEngine.Rect(new UnityEngine.Vector2(offsetX, offsetY), new UnityEngine.Vector2((float)rect.dim.x.value * aspect, (float)rect.dim.y.value * aspect));

                    base.Present(aspect, parent);

                    if (activeModal != null)
                    {
                        UnityEngine.GUI.WindowFunction draw = new UnityEngine.GUI.WindowFunction(
                            delegate (int id)
                            {

                            //if (UnityEngine.GUI.Button(activeModal.rect.ToPixelRect(aspect, parent), "Hello World")) UnityEngine.Debug.Log("Got a click in window " + id);

                            modalOverlay.Present(aspect, parent);


                                activeModal.Present(aspect, parent);
                            }
                            );

                        UnityEngine.GUIStyle style = new UnityEngine.GUIStyle();
                        style.border.top = 0;

                        UnityEngine.GUI.ModalWindow(0, new UnityEngine.Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height), draw, "", style);
                    }
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    System.Text.StringBuilder nfo = new System.Text.StringBuilder();

                    nfo.Append(string.Format("Layout {0}: {1}", id, base.DebugInfo()));

                    return nfo.ToString();
                }
                
                public object handler
                {
                    get { return _handler; }
                    protected set { _handler = value; }
                }

                /// <summary>
                /// The master list of unique ids and their associated widgets
                /// </summary>
                protected Dictionary<string, Thing> ids;


                protected object _handler;


                protected Page activeModal;
                protected Image modalOverlay;


                public StyleSheet styleSheet;
            }
        }
    } 
}