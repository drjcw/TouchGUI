using System;
using System.Collections.Generic;
using System.Xml;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            public class StyleSheet
            {
                public StyleSheet()
                {
                    styles = new Dictionary<string, PropertyCollection>();
                }

                public void Add(string Name, PropertyCollection Style)
                {
                    if (styles.ContainsKey(Name)) throw new Exception("StyleSheet already contains a style with name " + Name);

                    styles.Add(Name, Style);
                }

                public void Parse(XmlNode XMLStyleSheet)
                {
                    foreach (XmlNode xmlStyle in XMLStyleSheet.ChildNodes)
                    {
                        string name = Xml.Att(xmlStyle.Attributes["name"]);

                        // Empty style name is the same as "default"
                        if (name == null || name == "") name = "__default__";

                        if (styles.ContainsKey(name) == false) styles.Add(name, new PropertyCollection());

                        foreach (XmlAttribute a in xmlStyle.Attributes)
                        {
                            if (a.Name == "name") continue;

                            string attName = a.Name;
                            string attValue = a.InnerText;

                            if (attValue == null || attValue == "") continue;

                            styles[name].Set(attName, attValue);
                        }
                    }
                }

                public string Apply(string StyleName, string Property)
                {
                    return Apply(StyleName, Property, null);
                }

                public string Apply(string StyleName, string Property, XmlAttributeCollection Attributes)
                {
                    string widgetValue;

                    if (Attributes != null) widgetValue = Xml.Att(Attributes[Property]);
                    else widgetValue = null;

                    // Widget widgetValue has precedence over defined styles
                    if (widgetValue != null && widgetValue != "")
                    {
                        return widgetValue;
                    }
                    // Style is specified, attempt to decode it
                    else if (StyleName != null && StyleName != "")
                    {
                        string[] styleNames = StyleName.Split(' ');

                        // Style precedence is order of definition
                        foreach (string style in styleNames)
                        {
                            if (style != null && style != "")
                            {
                                if (styles.ContainsKey(style) == false) throw new Exception("Undefined style: " + style);

                                string value = styles[style].Get(Property);

                                if (value != null && value != "")
                                {
                                    return styles[style].Get(Property);
                                }
                            }
                        }
                    }

                    // If a default style exists, use that, otherwise property is null
                    if (styles.ContainsKey("default")) return styles["default"].Get(Property);
                    else return null;
                }

                protected Dictionary<string, PropertyCollection> styles;
            }
        }
    } 
}