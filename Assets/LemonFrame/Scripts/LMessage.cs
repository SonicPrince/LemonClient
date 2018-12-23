/***
 * 
 * Author:王飞飞
 * Date:2017-12-30
 * Email:wff_1994@126.com
 * QQ:756726530
 * 
 * 
 * **/

using System.Collections;
using System.Collections.Generic;
using System;

namespace Lemon
{
    public enum MessagerMode
    {
        REQUIRE_LISTENNER,
        DONT_REQUARE_LISTENNER
    }

    public class BroadcastException : Exception
    {
        public BroadcastException(string msg)
            : base(msg)
        {

        }
    }

    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg)
        {
        }
    }

    public class LMessage
    {
        private static Dictionary<object, Delegate> eventTable = new Dictionary<object, Delegate>();

        public static void AddListener(object eventType, CallBack handler)
        {
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, null);
            }

            Delegate d = eventTable[eventType];
            if (d != null && d.GetType() != handler.GetType())
            {
                ///抛出异常
                throw new ListenerException("ListenerException error ");
            }

            eventTable[eventType] = (CallBack)eventTable[eventType] + handler;
        }

        public static void AddListener<T>(object eventType, CallBack<T> handler)
        {
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, null);
            }

            Delegate d = eventTable[eventType];
            if (d != null && d.GetType() != handler.GetType())
            {
                ///抛出异常
                throw new ListenerException("ListenerException error ");
            }

            eventTable[eventType] = (CallBack<T>)eventTable[eventType] + handler;
        }

        public static void RemoveListener(object eventType, CallBack handler)
        {
            eventTable[eventType] = (CallBack)eventTable[eventType] - handler;
        }

        public static void Broadcast(object eventType, MessagerMode mode = MessagerMode.DONT_REQUARE_LISTENNER)
        {
            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                CallBack callback = d as CallBack;
                if (callback != null)
                {
                    callback();
                }
                else
                {
                    throw new BroadcastException("brocast error ");
                }
            }
        }

        public static void Broadcast<T>(object eventType, T arg1, MessagerMode mode = MessagerMode.DONT_REQUARE_LISTENNER)
        {
            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T> callback = d as CallBack<T>;
                if (callback != null)
                {
                    callback(arg1);
                }
                else
                {
                    throw new BroadcastException("brocast error");
                }
            }
        }
    }
}

