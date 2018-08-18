namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Abstract base class for all GUI elements that can be presented visually on the screen.
            /// </summary>
            public abstract class Presentable : Thing
            {
                /// <summary>
                /// Initializes the presentable with a unit rect of null values.
                /// </summary>
                //public Presentable(bool Visible)
                //{
                //    rect = new UnitRect();
                //    visible = true;
                //}

                ///// <summary>
                ///// Initializes the presentable with a unit rect of null values and the id specified by <paramref name="ID"/>.
                ///// </summary>
                ///// <param name="ID"></param>
                //public Presentable(string ID, bool Visible) : base(ID)
                //{
                //    rect = new UnitRect();
                //    visible = Visible;
                //}

                /// <summary>
                /// Initializes the components of the presentable to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
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
                /// <param name="Y">The y positioning of the unit rect relative to the parent as governed by the vertical alignment as follows:
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
                public Presentable(string ID, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID)
                {
                    rect = new UnitRect(X, Y, W, H, HAlign, VAlign);
                    visible = Visible == null || Visible == "" ? true : bool.Parse(Visible);
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Presentable) {0} {1}", base.DebugInfo(), rect.DebugInfo());
                }

                /// <summary>
                /// Abstract method for presenting the derived object visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public abstract void Present(float VirtualScale, UnityEngine.Rect Parent);

                /// <summary>
                /// The unit rect representing the bounding box of this object.
                /// </summary>
                public UnitRect rect;
                
                public bool visible;
            }
        }
    } 
}