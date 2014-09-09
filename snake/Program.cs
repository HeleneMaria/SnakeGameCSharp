using System;
using System.Windows.Forms;
using System.Net;
using System.Text;
using System.Net.NetworkInformation;

namespace snake
{
    static class Program
    {

        /// <summary>
        /// Where the program begins
        /// </summary>
        public static int reset,wait;

        /// <summary>
        /// Main of the principal application, where everything starts
        /// </summary>
        [STAThread]
        static void Main()
        {
            reset = 0;
            wait = 0;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Game());
        }
    }
}
