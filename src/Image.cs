using System;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            /// <summary>
            /// Widget for displaying formatted images.
            /// </summary>
            public class Image : Presentable
            {
                /// <summary>
                /// Initializes the components of the image to the values specified by the strings.
                /// </summary>
                /// <param name="ID">The unique id (if any) of this object.</param>
                /// <param name="Src">The path (if any) to the image file to load.</param>
                /// <param name="Colour">The colour tint to apply to the image or the colour fill to use as the image.
                /// <list type="table">
                /// <listheader>
                /// <term>Src Value</term>
                /// <term>Colour Value</term>
                /// <term>Result</term>
                /// </listheader>
                /// <item>
                /// <term>Specified</term>
                /// <term>Specified</term>
                /// <term>Image is rendered with Colour tint</term>
                /// </item>
                /// <item>
                /// <term>Specified</term>
                /// <term>null or empty</term>
                /// <term>Image is rendered without tint</term>
                /// </item>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Specified</term>
                /// <term>Colour is used as fill colour for image</term>
                /// </item>
                /// <item>
                /// <term>null or empty</term>
                /// <term>null or empty</term>
                /// <term>Invalid, exception thrown</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="Scale">The scale mode used for rendering the image to the specicified unit rect dimensions:
                /// <list type="table">
                /// <listheader>
                /// <term>String Value</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Stretches the texture to fill the complete rectangle passed in to GUI.DrawTexture</term>
                /// </item>
                /// <item>
                /// <term>stretchToFill</term>
                /// <term>Stretches the texture to fill the complete rectangle passed in to GUI.DrawTexture</term>
                /// </item>
                /// <item>
                /// <term>scaleAndCrop</term>
                /// <term>Scales the texture, maintaining aspect ratio, so it completely covers the position rectangle passed to GUI.DrawTexture. If the texture is being draw to a rectangle with a different aspect ratio than the original, the image is cropped</term>
                /// </item>
                /// <item>
                /// <term>scaleToFit</term>
                /// <term>Scales the texture, maintaining aspect ratio, so it completely fits withing the position rectangle passed to GUI.DrawTexture</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="X">The x positioning of the image relative to the parent as governed by the horizontal alignment as follows:
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
                /// <param name="Y">The y positioning of the image relative to the parent as governed by the vertical alignment as follows:
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
                /// <param name="W">The width of the image as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>W</term>
                /// <term>H</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>null or empty</term>
                /// <term>The image's dimensions (in pixels) will be used</term>
                /// </item>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Specified</term>
                /// <term>The width will be calculated according to the specified height and aspect ration of the image</term>
                /// </item>
                /// <item>
                /// <term>Specified</term>
                /// <term>-n/a-</term>
                /// <term>The specified width will be used</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="H">THe height of the image as follows:
                /// <list type="table">
                /// <listheader>
                /// <term>H</term>
                /// <term>W</term>
                /// <term>Description</term>
                /// </listheader>
                /// <item>
                /// <term>null or empty</term>
                /// <term>null or empty</term>
                /// <term>The image's dimensions (in pixels) will be used</term>
                /// </item>
                /// <item>
                /// <term>null or empty</term>
                /// <term>Specified</term>
                /// <term>The height will be calculated according to the specified width and aspect ration of the image</term>
                /// </item>
                /// <item>
                /// <term>Specified</term>
                /// <term>-n/a-</term>
                /// <term>The specified height will be used</term>
                /// </item>
                /// </list>
                /// </param>
                /// <param name="HAlign">The horizontal alignemnt of the image within the parent rect as specified by the following values:
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
                /// <param name="VAlign">The horizontal alignemnt of the image within the parent rect  as specified by the following values:
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
                /// <example>
                /// Instantiate an image object with the id "myImage", the path "somePic", no colour tint, default scale, a width of 300 virtual pixels, a height calculated automatically, positioned 10 virtual units down and centered horizontally: 
                /// <code>Image img = new Image("myImage", "somePic", null, 300, null, null, "10", "center", null);</code>
                /// Instantiate an image object no id, a solid colour fill, default scale, a width of 300 virtual pixels, a height calculated automatically, positioned 10 virtual units down and centered horizontally: 
                /// <code>Image img = new Image(null, null, "rgb(255,0,255)", 300, null, null, "10", "center", null);</code>
                /// </example>
                public Image(string ID, string Src, string Colour, string Scale, string X, string Y, string W, string H, string HAlign, string VAlign, string Visible) : base(ID, X, Y, W, H, HAlign, VAlign, Visible)
                {
                    switch (Scale)
                    {
                        case "":

                            scale = UnityEngine.ScaleMode.StretchToFill;
                            break;

                        case null:

                            scale = UnityEngine.ScaleMode.StretchToFill;
                            break;

                        case "stretchToFill":

                            scale = UnityEngine.ScaleMode.StretchToFill;
                            break;

                        case "scaleAndCrop":

                            scale = UnityEngine.ScaleMode.ScaleAndCrop;
                            break;

                        case "scaleToFit":

                            scale = UnityEngine.ScaleMode.ScaleToFit;
                            break;

                        default:
                            throw new Exception("Image widget has unknow scale mode");
                    }

                    if ((Colour == null || Colour == "") && (Src == null || Src == "")) throw new Exception("Image widget has empty or null src string and colour string");

                    rgba = TouchGUI.Colour.Parse(Colour);

                    // Build texture from colour
                    if ((Src == null || Src == "") && rgba != null)
                    {
                        img = new UnityEngine.Texture2D(1, 1);
                        img.SetPixel(0, 0, (UnityEngine.Color)rgba);
                        img.Apply();
                    }
                    // Load texture from file
                    else
                    {
                        img = UnityEngine.Resources.Load(Src) as UnityEngine.Texture2D;
                    }

                    if (img == null) throw new Exception("Image widget couldn't load " + Src);

                    // If width and height are null, use the image dimensions
                    if (rect.dim.x.value == null && rect.dim.y.value == null)
                    {
                        rect.dim.x.type = Unit.Type.Pixel;
                        rect.dim.x.value = img.width;

                        rect.dim.y.type = Unit.Type.Pixel;
                        rect.dim.y.value = img.height;
                    }
                    // If width is null, set the width with the image aspect ratio according to the specified height 
                    else if (rect.dim.x.value == null)
                    {
                        float aspect = (float)img.width / (float)img.height;

                        rect.dim.x.type = rect.dim.y.type;
                        rect.dim.x.value = (float)rect.dim.y.value * aspect;
                    }
                    // If height is null, set the height with the image aspect ratio according to the specified width 
                    else if (rect.dim.y.value == null)
                    {
                        float aspect = (float)img.height / (float)img.width;

                        rect.dim.y.type = rect.dim.x.type;
                        rect.dim.y.value = (float)rect.dim.x.value * aspect;
                    }
                }

                /// <summary>
                /// Presents the image visually on the screen.
                /// </summary>
                /// <param name="VirtualScale">The scaling factor used to map virtual units to pixel units if component unit type is virtual.</param>
                /// <param name="Parent">The parent rect (in pixel units) that this object resides within.</param>
                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    UnityEngine.Rect rPx = rect.ToPixelRect(VirtualScale, Parent);

                    // Unity 5.5.1.f does not support calls to DrawTexture with a custom colour tint so we have to change the global tint to our colour
                    UnityEngine.Color orig = UnityEngine.GUI.color;
                    if (rgba != null) UnityEngine.GUI.color = (UnityEngine.Color)rgba;
                    UnityEngine.GUI.DrawTexture(rPx, img, scale);
                    UnityEngine.GUI.color = orig;
                }

                /// <summary>
                /// Outputs the debug information for this class.
                /// </summary>
                /// <returns>The formatted string of class member values.</returns>
                public override string DebugInfo()
                {
                    return string.Format("(Image) img: {0}, scale: {1}, rgba: {2}, {3}", img, scale, rgba, base.DebugInfo());
                }

                /// <summary>
                /// Getter/setter for the _img member.
                /// </summary>
                /// <remarks>_img cannot be null.</remarks>
                public UnityEngine.Texture2D img
                {
                    get { return _img; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Image img member cannot be null (id: {0})", id));

                        _img = value;
                    }
                }

                /// <summary>
                /// The texture to draw
                /// </summary>
                protected UnityEngine.Texture2D _img;

                /// <summary>
                /// The scale mode used by the texture
                /// </summary>
                public UnityEngine.ScaleMode scale;

                /// <summary>
                /// The colour tint/fill
                /// </summary>
                public UnityEngine.Color? rgba;
            }
        }
    } 
}