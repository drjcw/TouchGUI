using System;
using System.Collections.Generic;

namespace QuickVR
{
    namespace Core
    {
        namespace Events
        {
            public class EventDispatcherCollection
            {
                public interface IDispatcher
                {
                   void AttachHandler(string Event, EventHandler<Event> E);
                }

                public EventDispatcherCollection()
                {
                    events = new Dictionary<string, EventDispatcher>();
                }

                public void AttachHandler(string Event, object Reciever, string Handler)
                {
                    if (!events.ContainsKey(Event)) throw new Exception("AttachHandler unknown event type: " + Event);

                    events[Event].AttachHandler(Reciever, Handler);
                }

                public void AttachHandler(string Event, EventHandler<Event> E)
                {
                    if (!events.ContainsKey(Event)) throw new Exception("AttachHandler unknown event type: " + Event);

                    events[Event].AttachHandler(E);
                }

                public void AddEvent(string Event/*, object Parent*/)
                {
                    if (events.ContainsKey(Event)) throw new Exception("AddEvent event already in collection: " + Event);

                    events.Add(Event, new EventDispatcher(/*Parent*/));
                }

                public bool Contains(string Event)
                {
                    return events.ContainsKey(Event);
                }

                public void Dispatch(string Event, object Parent)
                {
                    if (!events.ContainsKey(Event)) throw new Exception("Dispatch unknown event type: " + Event);

                    events[Event].args.properties.Set("EventName", Event);
                    events[Event].Dispatch(Parent);
                    events[Event].args.properties.Clear();
                }

                public void SetEventArg(string Event, string Arg, string Value)
                {
                    events[Event].args.properties.Set(Arg, Value);
                }

                protected Dictionary<string, EventDispatcher> events;
            }  
        }
    }
}