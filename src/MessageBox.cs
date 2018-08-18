using System;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            public class MessageBox : Page
            {
                [Flags]
                public enum ButtonType : uint
                {
                    Ok = 0x1,
                    Cancel = 0x2,
                    Yes = 0x4,
                    No = 0x8,
                    Continue = 0x10,
                    Help = 0x20
                };

                public MessageBox(Layout Parent, string Body, string Title, ButtonType Buttons) : base(null, Title, null, "20%", "80%", null, "center", null, null)
                {
                    parent = Parent;

                    // NOTE: y values cannot be % as height is determined dynamically (encaps below as msgbox methods with validation)

                    title = Title;
                    body = Body;
                    labelTitle = new Label(null, new Text(title, "upperCenter", null, null, null, "60"), "0", "0", null, null, "center", "top", null);
                    labelBody = new Label(null, new Text(body, "upperCenter", "true", null, null, "50"), "0", "0", "100%", null, null, null, null);
                    button = new Button(null, new Text("ok", "middleCenter", "true", null, null, "50"), null, new Image(null, null, "rgb(204, 204, 204)", null, "0", "0", "100%", "100%", null, null, null), null, "10", "400", "100", "center", "bottom", null, null);

                    titleTopPadding = new Unit(10, Unit.Type.Virtual);
                    bodyTopPadding = new Unit(10, Unit.Type.Virtual);
                    buttonTopPadding = new Unit(10, Unit.Type.Virtual);
                    buttons = Buttons;

                    button.guiDispatcher.AttachHandler("onClick", this, "mb_Click");
                }

                protected void mb_Click(object sender, Events.Event e)
                {
                    parent.CloseActiveModal();
                }

                public override void Present(float VirtualScale, UnityEngine.Rect Parent)
                {
                    float titleHeight, bodyHeight, buttonHeight;

                    // spacing between top of msgbx and title text
                    float titleSpacingY = titleTopPadding.ToPixel(VirtualScale, 0);

                    // spacing between the title text and the body text
                    float bodySpacingY = bodyTopPadding.ToPixel(VirtualScale, 0);

                    // spacing between body text and button(s)
                    float buttonSpacingY = buttonTopPadding.ToPixel(VirtualScale, 0);

                    // spacing between button and bottom border
                    float buttonbottomPadding = button.rect.pos.y.ToPixel(VirtualScale, 0);

                    float msgWidth = rect.dim.x.ToPixel(VirtualScale, Parent.width);

                    if (title != null && title != "")
                    {
                        titleHeight = labelTitle.text.CalculatePixelSize(VirtualScale).y;
                    }
                    else
                    {
                        titleHeight = 0;
                        bodySpacingY = titleSpacingY;
                        titleSpacingY = 0;
                    }

                    if (body != null && body != "")
                    {
                        bodyHeight = labelBody.text.CalculatePixelHeight(VirtualScale, labelBody.rect.dim.x.ToPixel(VirtualScale, msgWidth));
                    }
                    else
                    {
                        bodyHeight = 0;
                        bodySpacingY = 0;
                    }

                    buttonHeight = button.rect.dim.y.ToPixel(VirtualScale, Parent.height);

                    rect.dim.y.type = Unit.Type.Pixel;
                    rect.dim.y.value = titleSpacingY + titleHeight + bodySpacingY + bodyHeight + buttonHeight + buttonbottomPadding + buttonSpacingY;

                    UnityEngine.Rect rPx = rect.ToPixelRect(VirtualScale, Parent);

                    background.Present(VirtualScale, rPx);

                    labelTitle.rect.pos.y = titleTopPadding;
                    labelTitle.Present(VirtualScale, rPx);

                    labelBody.rect.pos.y.type = Unit.Type.Pixel;
                    labelBody.rect.pos.y.value = titleSpacingY + titleHeight + bodySpacingY;
                    labelBody.Present(VirtualScale, rPx);

                    button.Present(VirtualScale, rPx);
                }

                public override string DebugInfo()
                {
                    return base.DebugInfo();
                }

                public Image background
                {
                    get { return _background; }

                    set
                    {
                        if (value == null) throw new Exception(string.Format("Button background member cannot be null (id: {0})", id));

                        _background = value;

                        _background.rect.pos.x.type = Unit.Type.Virtual;
                        _background.rect.pos.x.value = 0;
                        _background.rect.pos.y.type = Unit.Type.Virtual;
                        _background.rect.pos.y.value = 0;

                        _background.rect.dim.x.type = Unit.Type.Percentage;
                        _background.rect.dim.x.value = 100;
                        _background.rect.dim.y.type = Unit.Type.Percentage;
                        _background.rect.dim.y.value = 100;

                        _background.rect.hAlign = new Alignment(null);
                        _background.rect.vAlign = new Alignment(null);
                    }
                }

                Layout parent;

                public Unit titleTopPadding;
                public Unit bodyTopPadding;
                public Unit buttonTopPadding;

                public string body;
                public Label labelTitle;
                public Label labelBody;
                protected Image _background;
                public ButtonType buttons;
                public Button button;
            }
        }
    } 
}