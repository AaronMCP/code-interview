using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public static class ControlExtends
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(
         IntPtr hWnd, // handle to window 
         int id, // hot key identifier 
         uint fsModifiers, // key-modifier options 
         Keys vk // virtual-key code 
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(
         IntPtr hWnd, // handle to window 
         int id // hot key identifier 
        );

        private static Dictionary<Keys,int> _registeredHotkeys = new Dictionary<Keys,int>();
        
        public static void RegHotKey(this Control ctrl,int id, uint keyModifier,Keys hotKey)
        {
            try
            {
                if (_registeredHotkeys.ContainsKey(hotKey))
                    return;

                RegisterHotKey(ctrl.Handle, id, keyModifier, hotKey);
                _registeredHotkeys.Add(hotKey,id);
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }
        }

        public static void UnRegHotKey(this Control ctrl)
        {
            try
            {
                if (_registeredHotkeys.Count == 0)
                    return;
                foreach(KeyValuePair<Keys,int> kvp in _registeredHotkeys)
                {
                UnregisterHotKey(ctrl.Handle, kvp.Value);
                }
                _registeredHotkeys.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }
        }
    }
}
