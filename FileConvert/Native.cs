using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvert
{
    public class Native
    {
        private List<Tuple<PandocFile, PandocFile>> files;

        private static Native instance;
        public PandocFile Input { get; set; }
        public PandocFile Output { get; set; }

        public static Native Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Native();
                }
                return instance;
            }
            private set { }
        }

        public Native()
        {
            files = new List<Tuple<PandocFile, PandocFile>>();
        }

        public Native(Preset[] presetList, int selectedOutput) : this(
            presetList[selectedOutput].InputFolder, 
            presetList[selectedOutput].InputFormat, 
            presetList[selectedOutput].OutputFolder, 
            presetList[selectedOutput].OutputFormat) { }

        public Native(string pathToScan, string inputFormat, string outputFolder, string outputFormat) : this()
        {
            string[] list = Directory.GetFiles(pathToScan, "*" + inputFormat, SearchOption.AllDirectories);
            foreach (var item in list)
            {
                PandocFile inputFile = new PandocFile(item, inputFormat);
                PandocFile outputFile = new PandocFile(inputFile);
                outputFile.ChangeFileExtension(outputFormat);
                outputFile.ModifyFilePathRelative(outputFolder, pathToScan);
                Tuple<PandocFile, PandocFile> tuple = new Tuple<PandocFile, PandocFile>(inputFile, outputFile);
                files.Add(tuple);
            }
        }

        public static bool TestPandocPresent()
        {
            Pandoc test = new Pandoc();
            test.Start();
            return test.GetExitCode() == 0;
        }

        public void ConvertFiles()
        {
            Parallel.ForEach(files, (item) =>
            {
                PandocFile input = item.Item1;
                PandocFile output = item.Item2;
                Console.WriteLine("Converting file: " + input.ProvideCompletePath());
                Directory.CreateDirectory(Path.GetDirectoryName(output.ProvideCompletePath()));
                Pandoc process = new Pandoc(input.ProvideCompletePath(), output.ProvideCompletePath(), "", Environment.CurrentDirectory);
                process.Start();
                process.GetOutput();
                if(process.GetExitCode() != 0)
                {
                    Console.WriteLine("Error converting file {0}", input.ProvideCompletePath());
                }
            });
        }

        public int GetFileCount()
        {
            return files.Count;
        }

        public void SetFiles()
        {
            Tuple<PandocFile, PandocFile> simple = new Tuple<PandocFile, PandocFile>(Input, Output);
            this.files.Add(simple);
        }
    }
}
