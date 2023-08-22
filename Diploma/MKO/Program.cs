namespace MKO
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Lab());
            Application.Run(new Menu());
            //Application.Run(new Test());
            //Application.Run(new Result());
            //Application.Run(new Calculation());
        }
    }
}