namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Base class for all GUI elements that can be referred to by a unique id in code.
            /// </summary>
            public class Thing : Debugable
            {
                /// <summary>
                /// Initializes the thing with a null id.
                /// </summary>
                public Thing()
                {
                    id = null;
                }

                /// <summary>
                /// Initializes the thing with the id specified by <paramref name="ID"/>.
                /// </summary>
                /// <param name="ID">The unique id (if any) to initialize this thing to.</param>
                public Thing(string ID)
                {
                    id = ID;
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Thing) id: {0}", id);
                }

                /// <summary>
                /// Getter/setter for the _id member.
                /// </summary>
                public string id
                {
                    get { return _id; }
                    protected set { _id = value; }
                }

                /// <summary>
                /// The unique id of this object (if any)
                /// </summary>
                /// <remarks>Can be null or empty.</remarks>
                protected string _id;
            }
        }
    } 
}