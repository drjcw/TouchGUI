using System;

namespace QuickVR
{
    namespace Core
    {
        namespace Events
        {
            public class Event : EventArgs
            {
                public Event()
                {
                    properties = new PropertyCollection();
                }

                public PropertyCollection properties;
            }
        }
    } 
}