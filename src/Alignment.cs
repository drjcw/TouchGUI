using System;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            ///  Defines a horizontal or vertical alignment type.
            /// </summary>
            public class Alignment
            {
                /// <summary>
                /// Specifies the direction of the alignment.
                /// </summary>
                /// <remarks>
                /// For Horizontal alignment types, the allowable directions are:
                /// <list type="bullet">
                /// <item>Left</item>
                /// <item>Center</item>
                /// <item>Right</item>
                /// </list>
                /// For Vertical alignment types, the allowable directions are:
                /// <list type="bullet">
                /// <item>Top</item>
                /// <item>Center</item>
                /// <item>Bottom</item>
                /// </list>
                /// </remarks>
                public enum Direction
                {
                    Left,
                    Right,
                    Top,
                    Bottom,
                    Center
                };

                /// <summary>
                /// Specifies the type of the alignment.
                /// </summary>
                public enum Type
                {
                    Horizontal,
                    Vertical
                };

                /// <summary>
                /// Initializes the alignment to the properties of an existing alignment.
                /// </summary>
                /// <param name="A">The alignment that the alignment will recieve its properties from.</param>
                /// <remarks>If <paramref name="A"/> is null, the alignment's properties will be null.</remarks>
                /// <example>
                /// Instantiate an alignment object according to an existing alignment:
                /// <code>
                /// Alignment a = new Alignment(someOtherAlignment);
                /// </code>
                /// Instantiate an alignment object with null properties:
                /// <code>
                /// Alignment a = new Alignment(null);
                /// </code>
                /// </example>
                public Alignment(Alignment A)
                {
                    if (A == null)
                    {
                        type = null;
                        dir = null;
                    }
                    else
                    {
                        type = A.type;
                        dir = A.dir;
                    }
                }

                /// <summary>
                /// Initializes a new alignment to the specified values
                /// </summary>
                /// <param name="T">The type of alignment.</param>
                /// <param name="D">The direction of the alignment.</param>
                /// <example>
                /// Instantiate an alignment object with horizontal left alignment:
                /// <code>
                /// Alignment a = new Alignment(Alignment.Type.Horizontal, Alignment.Direction.Left);
                /// </code>
                /// </example>
                public Alignment(Type T, Direction D)
                {
                    type = T;
                    dir = D;
                }

                /// <summary>
                /// Initializes a new alignment to the specified values
                /// </summary>
                /// <param name="T">The type of the alignment.</param>
                /// <param name="D">The string value of the direction.</param>
                /// <remarks>This constructor is used primarily in conjunction with XML-based layouts</remarks>
                /// <example>
                /// Instantiate an alignment object with horizontal left alignment:
                /// <code>
                /// Alignment a = new Alignment(Alignment.Type.Horizontal, "left");
                /// </code>
                /// Instantiate an alignment object with null properties:
                /// <code>
                /// Alignment a = new Alignment(null, null);
                /// </code>
                /// </example>
                public Alignment(Type? T, string D)
                {
                    if (T == null || D == null || D == "")
                    {
                        dir = null;
                        type = null;
                        return;
                    }

                    type = T;

                    if (type == Type.Horizontal)
                    {
                        switch (D)
                        {
                            case "left":

                                dir = Direction.Left;
                                break;

                            case "center":

                                dir = Direction.Center;
                                break;

                            case "right":

                                dir = Direction.Right;
                                break;

                            default:

                                throw new Exception(string.Format("Unknown horizontal alignment type: {0}", D));
                        }
                    }
                    else if (type == Type.Vertical)
                    {
                        switch (D)
                        {
                            case "top":

                                dir = Direction.Top;
                                break;

                            case "center":

                                dir = Direction.Center;
                                break;

                            case "bottom":

                                dir = Direction.Bottom;
                                break;

                            default:

                                throw new Exception(string.Format("Unknown vertical alignment type: {0}", D));
                        }
                    }
                }

                /// <summary>
                /// Getter/setter for the _dir member.
                /// </summary>
                /// <remarks>The direction will be validated according to the current alignment type (if any).</remarks>
                public Direction? dir
                {
                    get { return _dir; }

                    set
                    {
                        if (_type == Type.Horizontal && (value == Direction.Top || value == Direction.Bottom)) throw new Exception("Invalid horizontal alignment: " + value);
                        if (_type == Type.Vertical && (value == Direction.Left || value == Direction.Right)) throw new Exception("Invalid horizontal alignment: " + value);

                        _dir = value;
                    }

                }

                /// <summary>
                /// Getter/setter for the _type member.
                /// </summary>
                /// <remarks>The type will be validated according to the current alignment direction (if any).</remarks>
                public Type? type
                {
                    get
                    {
                        return _type;
                    }

                    set
                    {
                        if (value == Type.Horizontal && (_dir == Direction.Top || _dir == Direction.Bottom)) throw new Exception("Invalid horizontal alignment: " + _dir);
                        if (value == Type.Vertical && (_dir == Direction.Left || _dir == Direction.Right)) throw new Exception("Invalid horizontal alignment: " + _dir);

                        _type = value;
                    }
                }

                /// <summary>
                /// The direction of this alignment.
                /// </summary>
                private Direction? _dir;

                /// <summary>
                /// The type of this alignment.
                /// </summary>
                private Type? _type;
            }
        }
    } 
}