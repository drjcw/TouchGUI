using System;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Defines a rectangle of unit components.
            /// </summary>
            public class UnitRect : Debugable
            {
                /// <summary>
                /// Initializes the components of the unit rect to null values.
                /// </summary>
                public UnitRect()
                {
                    pos = new UnitVector2d();
                    dim = new UnitVector2d();
                    hAlign = new Alignment(null, null);
                    vAlign = new Alignment(null, null);
                }

                /// <summary>
                /// Initializes the components of the unit rect to the values specified by the strings.  
                /// </summary>
                /// <param name="X">The x positioning of the unit rect relative to the parent as governed by the horizontal alignment as follows:
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
                /// <param name="Y">The y positioning of the rect relative to the parent as governed by the vertical alignment as follows:
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
                /// <param name="W">The width of the unit rect.</param>
                /// <param name="H">The height of the unit rect.</param>
                /// <param name="HAlign">The horizontal alignemnt of the unit rect within the parent rect as specified by the following values:
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
                /// <param name="VAlign">The horizontal alignemnt of the unit rect within the parent rect  as specified by the following values:
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
                /// Instantiate a unit rect object spaced 10% from the left edge of the parent rect, measuring 100x100 virtual units and centered vertically:
                /// <code>
                /// UnitRect r = new UnitRect("10%", null, "100", "100", null, "center");
                /// </code>
                /// </example>
                public UnitRect(string X, string Y, string W, string H, string HAlign, string VAlign)
                {
                    pos = new UnitVector2d(X, Y);
                    dim = new UnitVector2d(W, H);
                    hAlign = new Alignment(Alignment.Type.Horizontal, HAlign);
                    vAlign = new Alignment(Alignment.Type.Vertical, VAlign);
                }

                /// <summary>
                /// Initializes the components of the unit rect to the specified values.  
                /// </summary>
                /// <param name="X">The x positioning of the unit rect relative to the parent as governed by the horizontal alignment as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>Alignment</term>
                /// <term>X Position</term>
                /// </listheader>
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
                /// <param name="Y">The y positioning of the unit rect relative to the parent as governed by the vertical alignment as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>Alignment</term>
                /// <term>Y Position</term>
                /// </listheader>
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
                /// <param name="W">The width of the unit rect.</param>
                /// <param name="H">The height of the unit rect.</param>
                /// <param name="HAlign">The horizontal alignemnt of the unit rect within the parent rect.
                /// </param>
                /// <param name="VAlign">The horizontal alignemnt of the unit rect within the parent rect.
                /// </param>
                /// <example>
                /// Instantiate a unit rect object spaced 10% from the left edge of the parent rect, measuring 100x100 virtual units and centered vertically:
                /// <code>
                /// UnitRect r = new UnitRect(new Unit(10, Unit.Type.Percentage), null, new Unit(100, Unit.Type.Virtual), new Unit(100, Unit.Type.Virtual), null, new Alignment(Alignment.Type.Vertical, Alignment.Direction.Bottom));
                /// </code>
                /// </example>
                public UnitRect(Unit X, Unit Y, Unit W, Unit H, Alignment HAlign, Alignment VAlign)
                {
                    pos = new UnitVector2d(X, Y);
                    dim = new UnitVector2d(W, H);
                    hAlign = new Alignment(HAlign);
                    vAlign = new Alignment(VAlign);
                }

                /// <summary>
                /// Converts the relative positioning of the unit rect inside the specified parent pixel rect to absolute pixel coordinates.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) if component unit type is percentage.</param>
                /// <returns>The rect (in absolute pixel coordinates) of the unit rect.</returns>
                /// <example>
                /// Convert the unit rect to an absolute pixel rect:
                /// <code>
                /// UnityEngine.Rect rPx = rect.ToPixels(vScale, pxParent);
                /// </code>
                /// </example>
                public UnityEngine.Rect ToPixelRect(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.Rect pxRect = new UnityEngine.Rect();

                    float x = pos.x.value == null ? 0.0f : (float)pos.x.value;
                    float y = pos.y.value == null ? 0.0f : (float)pos.y.value;
                    float w = dim.x.value == null ? 0.0f : (float)dim.x.value;
                    float h = dim.y.value == null ? 0.0f : (float)dim.y.value;

                    if (pos.x.type == Unit.Type.Pixel) pxRect.x = Parent.x + x;
                    else if (pos.x.type == Unit.Type.Virtual) pxRect.x = x * VirtualScale + Parent.x;
                    else if (pos.x.type == Unit.Type.Percentage) pxRect.x = Parent.width * (x / 100.0f) + Parent.x;
                    else pxRect.x = Parent.x;

                    if (pos.y.type == Unit.Type.Pixel) pxRect.y = Parent.y + y;
                    else if (pos.y.type == Unit.Type.Virtual) pxRect.y = y * VirtualScale + Parent.y;
                    else if (pos.y.type == Unit.Type.Percentage) pxRect.y = Parent.height * (y / 100.0f) + Parent.y;
                    else pxRect.y = Parent.y;

                    if (dim.x.type == Unit.Type.Pixel) pxRect.width = w;
                    else if (dim.x.type == Unit.Type.Virtual) pxRect.width = w * VirtualScale;
                    else if (dim.x.type == Unit.Type.Percentage) pxRect.width = Parent.width * (w / 100.0f);
                    else throw new Exception("Rect has no width type");

                    if (dim.y.type == Unit.Type.Pixel) pxRect.height = h;
                    else if (dim.y.type == Unit.Type.Virtual) pxRect.height = h * VirtualScale;
                    else if (dim.y.type == Unit.Type.Percentage) pxRect.height = Parent.height * (h / 100.0f);
                    else throw new Exception("Rect has no height type");

                    switch (hAlign.dir)
                    {
                        case null:
                            break;

                        case Alignment.Direction.Left:
                            break;

                        case Alignment.Direction.Right:

                            pxRect.x = ((Parent.x + Parent.width) - pxRect.width - pxRect.x) + Parent.x;
                            break;

                        case Alignment.Direction.Center:

                            pxRect.x = (Parent.width - pxRect.width) * 0.5f + Parent.x;
                            break;
                    }

                    // AAAAAAAAAAAAAAAAAAAAH  CLIIIIIIIIIIIIIIIIIIIIIIIIIIIP

                    switch (vAlign.dir)
                    {
                        case null:
                            break;

                        case Alignment.Direction.Top:
                            break;

                        case Alignment.Direction.Bottom:

                            pxRect.y = ((Parent.y + Parent.height) - pxRect.height - pxRect.y) + Parent.y;
                            break;

                        case Alignment.Direction.Center:

                            pxRect.y = (Parent.height - pxRect.height) * 0.5f + Parent.y;
                            break;
                    }

                    return pxRect;
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(UnitRect) width: {0}{1}, height: {2}{3}, x: {4}{5}, y: {6}{7}, halign: {8}, valign: {9}", dim.x.value, dim.x.type, dim.y.value, dim.y.type, pos.x.value, pos.x.type, pos.y.value, pos.y.type, hAlign.dir, vAlign.dir);
                }

                /// <summary>
                /// The x and y positioning of the unit rect relative to the parent as governed by the horizontal and vertical alignment.
                /// </summary>
                /// <remarks>
                /// The horizontal alignment governs the x positioning as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>Alignment</term>
                /// <term>X Position</term>
                /// </listheader>
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
                /// The vertical alignment governs the y positioning as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>Alignment</term>
                /// <term>Y Position</term>
                /// </listheader>
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
                /// </remarks>
                public UnitVector2d pos;

                /// <summary>
                /// The width and height of the unit rect.
                /// </summary>
                public UnitVector2d dim;

                /// <summary>
                /// The horizontal alignment of the unit rect within the parent rect.
                /// </summary>
                public Alignment hAlign;

                /// <summary>
                /// The vertical alignment of the unit rect within the parent rect.
                /// </summary>
                public Alignment vAlign;
            }
        }
    } 
}