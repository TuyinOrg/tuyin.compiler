using System.Diagnostics.SymbolStore;

namespace BigBuns.Graph.Viewer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (args.Length == 0)
            {
                args = new string[1]
                {
                    @"E:\bigbuns\cil\test\reference.txt.tb"
                };
            }

            var forms = new Form1();
            if (args.Length > 0) 
                forms.DataFileName = args[0];

            if (args.Length > 1)
                forms.ImageFileName = args[1];

            Application.Run(forms);
        }
    }
}