namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Defines a 2d vector of units.
            /// </summary>
            public class UnitVector2d : Debugable
            {
                /// <summary>
                /// Initializes the components of the vector to null values.
                /// </summary>
                public UnitVector2d()
                {
                    x = new Unit(null);
                    y = new Unit(null);
                }

                /// <summary>
                /// Initializes the vector components to the specified unit values.
                /// </summary>
                /// <param name="X">The x component.</param>
                /// <param name="Y">The y component.</param>
                /// <example>
                /// Intantiate a vector object to the virtual unit vector (20, 40):
                /// <code>UnitVector2d v = new UnitVector2d(new Unit(20, Unit.Type.Virtual), new Unit(40, Unit.Type.Virtual));</code>
                /// </example>
                public UnitVector2d(Unit X, Unit Y)
                {
                    if (X == null) x = new Unit(null);
                    else x = new Unit(X.value, X.type);

                    if (Y == null) y = new Unit(null);
                    else y = new Unit(Y.value, Y.type);
                }

                /// <summary>
                /// Initializes the vector components to the values specified by the strings.
                /// </summary>
                /// <param name="X">The x component of the vector.</param>
                /// <param name="Y">The y component of the vector.</param>
                /// <remarks>This constructor is used primarily in conjunction with XML-based layouts</remarks>
                /// <example>
                /// Instantiate a vector object to the pixel unit vector (20, 40):
                /// <code>UnitVector2d = new UnitVector2d("20px", "40px");</code>
                /// </example>
                public UnitVector2d(string X, string Y)
                {
                    x = new Unit(X);
                    y = new Unit(Y);
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(UnitVector2d) x: {4}{5}, y: {6}{7}", x.value, x.type, y.value, y.type);
                }

                /// <summary>
                /// x component of vector.
                /// </summary>
                public Unit x;

                /// <summary>
                /// y component of vector.
                /// </summary>
                public Unit y;
            }
        }
    } 
}