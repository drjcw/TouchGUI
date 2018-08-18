using System.Collections.Generic;

namespace QuickVR
{
    namespace Core
    {
        namespace TouchGUI
        {
            public class GUIEventDispatcher : Events.EventDispatcherCollection
            {
                public GUIEventDispatcher(/*Presentable P*/)
                {
                    events.Add("onClick", new Events.EventDispatcher(/*P*/));
                    events.Add("onFocus", new Events.EventDispatcher(/*P*/));
                    events.Add("onUnfocus", new Events.EventDispatcher(/*P*/));
                    events.Add("onModalFocus", new Events.EventDispatcher(/*P*/));
                    events.Add("onModalUnfocus", new Events.EventDispatcher(/*P*/));
                }
            }
        } 
    }
}