﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Laikos
{
    public static class GUIEventManager
    {
        public enum Events
        {
            GuiCLICK,
            GuiDOWN,
            GuiUP
            
        };

        static List<Message> lastFrame_messages = new List<Message>();

        static List<Message> currentFrame_messages = new List<Message>();

        public static void CreateMessage(Message message)
        {
            if (!currentFrame_messages.Contains(message))
                currentFrame_messages.Add(message);
        }

        public static void FindMessagesByDestination(List<Message> result)
        {
            
            for (var i = 0; i < lastFrame_messages.Count; i++)
            {
                var m = lastFrame_messages[i];
                    result.Add(m);
            }

            result.Sort((x, y) => x.Type.CompareTo(y.Type));
        }

        public static void FindMessagesBySender(GameObject sender, List<Message> result)
        {
            for (var i = 0; i < lastFrame_messages.Count; i++)
            {
                var m = lastFrame_messages[i];
                if (m.Sender.Equals(sender))
                {
                    result.Add(m);

                }
            }
            result.Sort((x, y) => x.Type.CompareTo(y.Type));
        }

        public static void FindMessage(Predicate<Message> criteria, List<Message> result)
        {
            for (int i = 0; i < lastFrame_messages.Count; i++)
            {
                Message m = lastFrame_messages[i];
                if (criteria.Invoke(m))
                {
                    result.Add(m);

                }
            }
        }

        public static void Update()
        {
            var t = lastFrame_messages;
            lastFrame_messages = currentFrame_messages;
            currentFrame_messages = t;
            currentFrame_messages.Clear();

        }

        public static bool MessageToOld(GameTime gameTime, Message message, int milliseconds)
        {
            if (message.timer != TimeSpan.Zero)
            {
                float difference = (float)gameTime.TotalGameTime.TotalMilliseconds - (float)message.timer.TotalMilliseconds;
                if (difference > milliseconds)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
