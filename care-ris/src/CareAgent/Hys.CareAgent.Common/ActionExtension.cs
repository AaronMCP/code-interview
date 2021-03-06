﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hys.CareAgent.Common
{
    public static class ActionExtension
    {
        /// <summary>
        /// Delay the action with specified milliseconds.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delayMilliseconds"></param>
        public static void Delay(this Action action, int delayMilliseconds = 500)
        {
            var timer = new Timer { Interval = delayMilliseconds };
            timer.Tick += (sender, e) =>
            {
                action();
                timer.Stop();
                timer = null;
            };
            timer.Start();
        }

        /// <summary>
        /// Delay the action with specified milliseconds.
        /// </summary>
        /// <typeparam name="T">The type of parametr</typeparam>
        /// <param name="action">The action to delay</param>
        /// <param name="param">The prameter to invoke the action</param>
        /// <param name="delayMilliseconds">The delay time in milliseocnd</param>
        public static void Delay<T>(this Action<T> action, T param, int delayMilliseconds = 500)
        {
            var timer = new Timer { Interval = delayMilliseconds };
            timer.Tick += (sender, e) =>
            {
                action(param);
                timer.Stop();
                timer = null;
            };
            timer.Start();
        }
    }
}
