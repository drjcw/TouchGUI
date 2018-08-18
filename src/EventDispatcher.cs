using System;
using System.Reflection;

namespace QuickVR
{
    namespace Core
    {
        namespace Events
        {
            public class EventDispatcher
            {
                public EventDispatcher(/*object Parent*/)
                {
                    //parent = Parent;
                    args = new Event();
                }

                public void AttachHandler(object Reciever, string Handler)
                {
                    if (Handler == null || Handler == "") return;

                    try
                    {
                        EventInfo evHandler = GetType().GetEvent("dispatcher");
                        Type tHandler = evHandler.EventHandlerType;

                        MethodInfo recieverHandler = Reciever.GetType().GetMethod(Handler, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        Delegate d = Delegate.CreateDelegate(tHandler, Reciever, recieverHandler);
                        
                        evHandler.RemoveEventHandler(this, d);
                        evHandler.AddEventHandler(this, d);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Couldn't attach event handler {0}. Reason: {1}", Handler, e.Message));
                    }
                }

                public void AttachHandler(EventHandler<Event> E)
                {
                    dispatcher -= E;
                    dispatcher += E;
                }

                public event EventHandler<Event> dispatcher = delegate { };

                //public void Dispatch()
                //{
                //    dispatcher(parent, args);
                //}


                public void Dispatch(object Parent)
                {
                    dispatcher(Parent, args);
                }

                //protected object parent;
                public Event args;
            }
        }
    } 
}