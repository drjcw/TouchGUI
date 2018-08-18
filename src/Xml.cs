using System.Xml;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            public static class Xml
            {
                /// <summary>
                /// Shorthand means of checking if an XML attribute exists and returning its value (if any).
                /// </summary>
                /// <param name="A">The XML attribute to parse.</param>
                /// <returns>Either the value of the attribute (if exists), otherwise null.</returns>
                public static string Att(XmlAttribute A)
                {
                    return A == null ? null : A.InnerText;
                }
            } 
        }
    }
}
