
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerTimeControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ComputerStartTime pcStartTime = new ComputerStartTime();

            Console.WriteLine("pc start data = " + pcStartTime.GetStartData());
            Console.WriteLine("pc start time = " + pcStartTime.GetStartTime());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
