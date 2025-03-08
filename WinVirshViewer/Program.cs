using System;
using System.IO;
using System.Windows.Forms;

namespace WinVirshViewer;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        //var result0 = con.Client.RunCommand("virsh list --all");
        //Console.WriteLine(new StreamReader(result0.OutputStream).ReadToEnd());
        //Console.WriteLine(result0.Result);



        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
