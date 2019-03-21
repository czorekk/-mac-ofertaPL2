using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.IO;
using AppKit;
using CoreGraphics;

namespace Oferta__
{
    public class AppSecure : NSWindowController
    {
        public AppSecure()
        {
        }

        public static void Verify()
        {
            string file = Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Key.txt");
            if (File.Exists(file))
            {
                string uuid = MacUUID();
            }
            else
            {
                //var instance = new AppSecure();
                //instance.RequestKey();
                RequestKey();
            }
        }

        public static void RequestKey()
        {
            NSAlert alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "desc",
                MessageText = "title",
            };
            alert.RunModal();
            /*
            var textfield = new NSTextField(new CGRect(0, 0, 300, 20));

            NSAlert alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "Aby używać programu, musisz wpisać klucz. Aby go otrzymać, musisz podać swój identyfikator komputera twórcy programu.\n \nTwój identyfikator: " + MacUUID().Split('=')[1],
                MessageText = "Brak klucza produktu",
            };
            alert.AccessoryView = textfield;
            alert.AddButton("Kopiuj identyfikator do schowka"); //1000
            alert.AddButton("Aktywuj"); //1001
            if(alert.RunModal() == 1001)
            {
                Console.WriteLine(((NSTextField)(alert.AccessoryView)).StringValue);
            }
            */
            //alert.RunModal();
        }

        public static void CopyUUIDToClipboard()
        {

        }

        static string MacUUID()
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "sh",
                Arguments = "-c \"ioreg -rd1 -c IOPlatformExpertDevice | awk '/IOPlatformUUID/'\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UserName = Environment.UserName
            };
            var builder = new StringBuilder();
            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                builder.Append(process.StandardOutput.ReadToEnd());
            }
            return builder.ToString();
        }
    }
}
