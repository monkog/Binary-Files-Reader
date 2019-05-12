using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace BinaryFilesReader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		[ExcludeFromCodeCoverage]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Browser());
        }
    }
}
