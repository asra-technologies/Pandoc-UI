using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileConvert
{
    public class Settings
    {
        private readonly static string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Asra-Notion\Pandoc-UI\settings.xml";
        public Preset[] PresetList { get; set; }
        public bool UseOutputFolder { get; set; }
        public int SelectedOutput { get; set; }
        public bool PromptSave { get; set; }

        /// <summary>
        /// Initialise the application default settings
        /// </summary>
        public Settings()
        {
            PresetList = new Preset[] { new Preset() { Name = "default", InputFolder = "", InputFormat = "", OutputFolder = "", OutputFormat = "" } };
            SelectedOutput = -1;
            PromptSave = true;
        }

        /// <summary>
        /// Init the app with the user saved settings
        /// </summary>
        /// <param name="useUserSettings">true to use the settings, false to use the program arguments</param>
        /// <param name="args">Launch arguments</param>
        public Settings(bool useUserSettings, string[] args) : this()
        {
            if (useUserSettings && File.Exists(SettingsPath))
            {
                Settings fromFile = XmlHelper.FromXmlFile<Settings>(SettingsPath);
                this.UseOutputFolder = fromFile.UseOutputFolder;
                this.SelectedOutput = fromFile.SelectedOutput;
                this.PromptSave = fromFile.PromptSave;
                this.PresetList = fromFile.PresetList;
            }
            else
            {
                ParseArguments(args);
            }
        }

        private void ParseArguments(string[] args)
        {
            PresetList[0].InputFolder = Environment.CurrentDirectory;
            UseOutputFolder = true;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                        i++;
                        PresetList[0].InputFormat = args[i];
                        break;
                    case "-f":
                        i++;
                        PresetList[0].OutputFolder = args[i];
                        SelectedOutput = 0;
                        break;
                    case "-o":
                        i++;
                        PresetList[0].OutputFormat = args[i];
                        break;
                    default:
                        break;
                }
            }
        }

        public void SaveSettings()
        {
            XmlHelper.ToXmlFile(this, SettingsPath);
            Display.SettingsSavedSuccess();
        }

        public void SelectOutput()
        {
            if (PresetList.Length > 1)
            {
                Display.MultiplePresets(PresetList);
                string selection = Console.ReadLine();
                int temp = int.Parse(selection) - 1;
                if (temp >= 0 && temp < PresetList.Length)
                {
                    SelectedOutput = temp;
                    Console.WriteLine("Selected Option : " + PresetList[SelectedOutput].InputFolder + ", " + PresetList[SelectedOutput].OutputFolder);
                }
            }
            else
            {
                SelectedOutput = 0;
            }
        }

        public bool AreSettingsSet()
        {
            return (PresetList[0].InputFormat != string.Empty && PresetList[0].OutputFolder == string.Empty && PresetList[0].OutputFormat != string.Empty);
        }

        public void SetMissingSettings()
        {
            if (PresetList[0].InputFormat == string.Empty)
            {
                Display.SetInputFormat();
                PresetList[0].InputFormat = Console.ReadLine();
            }
            if (PresetList[0].OutputFolder == string.Empty)
            {
                Display.SetOutputFolder();
                PresetList[0].OutputFolder = Console.ReadLine();
            }
            if (PresetList[0].OutputFormat == string.Empty)
            {
                Display.SetOutputFormat();
                PresetList[0].OutputFormat = Console.ReadLine();
            }
        }

        public Tuple<string, string> GetSelectedFolders()
        {
            string input = PresetList[SelectedOutput].InputFolder;
            string output = PresetList[SelectedOutput].OutputFolder;
            return new Tuple<string, string>(input, output);
        }
    }
}
