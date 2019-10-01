using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views;
using Sara.Logging;

namespace Sara.LogReader.WinForm
{
    static class Program
    { 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // :TODO Add File writer to .config  - Sar
            Log.Write("Starting Program", typeof (Program).FullName, MethodBase.GetCurrentMethod().Name);
            Log.WriteTrace($"Initializing Sara.LogReader {Application.ProductVersion}", MethodBase.GetCurrentMethod().Name);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(args.Length == 0 ? new MainForm(string.Empty) : new MainForm(args[0]));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                var msg = exception.Message;
                MessageBox.Show(msg, @"Unhandled UI Exception");
                Log.WriteError($"Unhandled UI Exception - {msg}", typeof(Program).FullName, MethodBase.GetCurrentMethod().Name, exception);
            }
            else
                Log.WriteError("Unhandled UI Exception - e.ExceptionObject was null!!!", typeof(Program).FullName, MethodBase.GetCurrentMethod().Name);

            Log.Exit(new TimeSpan(0, 0, 2, 0));
            Application.Exit();
            // Force a exit with a success code of zero
            Environment.Exit(0);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.WriteError(typeof(Program).FullName, MethodBase.GetCurrentMethod().Name, e.Exception);
            Log.Exit(new TimeSpan(0, 0, 2, 0));
            // Force a exit with a success code of zero
            Application.Exit();
            Environment.Exit(0);
        }
    }
}