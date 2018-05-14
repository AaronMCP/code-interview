using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hys.PrintTemplateManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Aqua";
            Application.Run(new PrintTemplatePanel());
        }
    }
}
