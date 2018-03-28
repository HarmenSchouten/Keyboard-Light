using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardLighter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public enum indicatorModes { GREEN_ON, GREEN_GLOW, GREEN_FAST, GREEN_SLOW, ORANGE_ON, ORANGE_GLOW, ORANGE_FAST, ORANGE_SLOW, RED_ON, RED_GLOW, RED_FAST, RED_SLOW }
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
