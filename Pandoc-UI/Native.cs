using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandoc_UI
{
    public class Native
    {
        public static bool CheckIfPandocPresent()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "pandoc",
                    Arguments = "-v",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            while ( !process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
            }
            return process.ExitCode == 0;
        }
    }
}
