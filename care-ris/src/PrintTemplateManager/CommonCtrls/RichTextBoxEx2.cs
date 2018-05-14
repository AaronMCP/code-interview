using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace Hys.CommonControls
{

    public class RichTextBoxEx2 : RichTextBox
    {
   
        const string DLL_RICHEDIT = "msftedit.dll";

        const string WC_RICHEDITW = "RICHEDIT50W";

        private IntPtr moduleHandle;

        private bool attemptedLoad;



        [DllImport("Kernel32.dll")]

        internal static extern IntPtr LoadLibrary(string libname);





        public RichTextBoxEx2()
        {

            // This is where we store the riched library.

            moduleHandle = IntPtr.Zero;

            attemptedLoad = false;

        }



        protected override CreateParams CreateParams
        {

            get
            {

                CreateParams cp = base.CreateParams;

                AttemptToLoadNewRichEdit();

                if (moduleHandle != IntPtr.Zero)
                {

                    cp.ClassName = WC_RICHEDITW;

                }

                return cp;

            }

        }



        void AttemptToLoadNewRichEdit()
        {

            // Check for library

            if (false == attemptedLoad)
            {

                attemptedLoad = true;

                string strFile = Path.Combine(Environment.SystemDirectory, DLL_RICHEDIT);

                moduleHandle = LoadLibrary(strFile);

            }

        }

        #region Oscar added

        // 2015-11-25, Oscar added, fix "宋体" issue. (US27923)
        bool internalTextChanging;
        protected override void OnTextChanged(EventArgs e)
        {
            if (this.internalTextChanging) return;
            this.internalTextChanging = true;
            try
            {
                this.FixText();
                base.OnTextChanged(e);
            }
            finally
            {
                this.internalTextChanging = false;
            }
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            if (this.internalTextChanging) return;
            base.OnReadOnlyChanged(e);
        }

        #endregion
    }
}
