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
            /// Widget representing a collection of pages within the GUI layout.
            /// </summary>
            public class PageContainer : Page
            {
                /// <summary>
                /// Initializes the components of the page collection widget to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                public PageContainer(string ID, string Visible) : base(ID, null, Visible)
                {
                    pages = new Dictionary<string, Page>();
                }

                /// <summary>
                /// Adds a collection of child widgets to this page.
                /// </summary>
                /// <param name="Children">The collection of widgets (in XML format) to add.</param>
                /// <param name="Parent">The parent layout this page collection belongs to.</param>
                public virtual void AddChildren(XmlNodeList Children, Layout Parent)
                {
                    foreach (XmlNode xmlElement in Children)
                    {
                        if (xmlElement.Name == "page") AddPage(Parent, xmlElement);
                        else AddWidget(xmlElement, Parent);
                    }
                }

                /// <summary>
                /// Adds a page widget to this page collection.
                /// </summary>
                /// <param name="Parent">The parent layout this page collection belongs to.</param>
                /// <param name="XMLPage">The page widget (in XML format) to add to this collection.</param>
                protected void AddPage(Layout Parent, XmlNode XMLPage)
                {
                    // Pages must have an id, otherwise there is no way to refer to them via code
                    if (Xml.Att(XMLPage.Attributes["id"]) == null) throw new Exception("Encountered unnamed page in PageContainer");

                    Page p = new Page(Xml.Att(XMLPage.Attributes["id"]), Xml.Att(XMLPage.Attributes["title"]), Xml.Att(XMLPage.Attributes["visible"]));
                    p.guiDispatcher.AttachHandler("onFocus", Parent.handler, Xml.Att(XMLPage.Attributes["onFocus"]));

                    pages.Add(p.id, p);
                    Parent.RegisterId(p);

                    foreach (XmlNode xmlWidget in XMLPage.ChildNodes)
                    {
                        p.AddWidget(xmlWidget, Parent);
                    }
                }

                /// <summary>
                /// Activates the specified page.
                /// </summary>
                /// <param name="ID">The id of the page to activate.</param>
                /// <remarks>This is the page that will be displayed on the screen.</remarks>
                public void ActivatePage(string ID)
                {
                    if (ID == null || ID == "") throw new Exception("id of desired page to activate cannot be null or empty");
                    if (!pages.ContainsKey(ID)) throw new Exception("Desired page to activate does not exist in collection: " + ID);

                    string prev = activePage == null ? null : activePage.id;

                    activePage = pages[ID];

                    activePage.guiDispatcher.SetEventArg("onFocus", "previous", prev);
                    activePage.guiDispatcher.SetEventArg("onFocus", "current", activePage.id);
                    activePage.guiDispatcher.Dispatch("onFocus", this);
                }

                /// <summary>
                /// Presents the child widgets and active page visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    foreach (Presentable w in widgets)
                    {
                        if(w.visible) w.Present(VirtualScale, Parent);
                    }

                    if(activePage.visible) activePage.Present(VirtualScale, Parent);
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    System.Text.StringBuilder nfo = new System.Text.StringBuilder();

                    nfo.Append(string.Format("PageContainer {0}: {1}", id, base.DebugInfo()));

                    foreach (Presentable w in widgets)
                    {
                        nfo.Append(string.Format("\n\t{0}", w.DebugInfo()));
                    }

                    foreach (KeyValuePair<string, Page> p in pages)
                    {
                        nfo.Append(string.Format("\n\t{0}", p.Value.DebugInfo()));
                    }

                    return nfo.ToString();
                }

                /// <summary>
                /// Getter/setter for the _activePage member.
                /// </summary>
                public Page activePage
                {
                    get { return _activePage; }
                    protected set { _activePage = value; }
                }

                /// <summary>
                /// The page collection
                /// </summary>
                public Dictionary<string, Page> pages;          // DO GETTER SETTER

                /// <summary>
                /// The currently active page
                /// </summary>
                protected Page _activePage;
            }
        }
    } 
}