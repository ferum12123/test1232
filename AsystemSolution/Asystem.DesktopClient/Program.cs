using System;
using System.Windows.Forms;

namespace Asystem.DesktopClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
