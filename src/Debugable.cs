namespace QuickVR
{
    namespace Core
    {
        /// <summary>
        /// Abstract class for all GUI elements to output debug information.
        /// </summary>
        public abstract class Debugable
        {
            /// <summary>
            /// Output the class members as a formatted string.
            /// </summary>
            /// <returns>The formatted string of class member values.</returns>
            public abstract string DebugInfo();

            /// <summary>
            /// Outputs the debug info for a child member of the class.
            /// </summary>
            /// <param name="C"></param>
            /// <returns>The formatted string of child class member values.</returns>
            /// <remarks>Used for inline debug output, returning the string "null" if the child is null.</remarks>
            /// <example>
            /// An example implementation of DebugInfo() for a class with debuggable members:
            /// <code>
            /// public override string DebugInfo()
            /// {
            ///     return string.Format("M1: {0}, M2: {1}, M3: {2}", member1, member2, DebugInfo(member3));
            /// }
            /// </code>
            /// </example>
            public static string DebugInfo(Debugable C)
            {
                return C == null ? "null" : C.DebugInfo();
            }
        }
    } 
}