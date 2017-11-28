using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvert
{
    public class Pandoc
    {
        private Process process;

        public Pandoc() : this(null, null, "-v", Environment.CurrentDirectory) { }

        public Pandoc(string inputFile, string outputFile, string arguments, string workingDirectory)
        {
            arguments = CreateArguments(inputFile, outputFile, arguments);
            process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "pandoc",
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
        }

        private string CreateArguments(string inputFile, string outputFile, string arguments)
        {
            if(inputFile == null || outputFile == null)
            {
                if (arguments != null)
                {
                    return arguments;
                }
            }
            string command = String.Format("-s \"{0}\" -o \"{1}\" {2}", inputFile, outputFile, arguments);
            return command;
        }

        public void Start()
        {
            process.Start();
        }

        public string[] GetOutput()
        {
            List<string> result = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                result.Add(line);
                Console.WriteLine(line);
            }
            return result.ToArray();
        }

        public int GetExitCode()
        {
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
