using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvert
{
    public class Program
    {
        public static Settings AppSettings;
        public static Native Pandoc;

        static void Main(string[] args)
        {
            Display.DisplayWelcome();
            CheckForPandoc();
            AppSettings = new Settings(true, args);
            SetSettingsIfMissing();
            AppSettings.SelectOutput();
            UserSelectSave();
            SetUpNative();
            Pandoc.ConvertFiles();
            Display.ConversionDone(Pandoc.GetFileCount());
            System.Threading.Thread.Sleep(2000);
        }

        public static void UserSelectSave()
        {
            if (AppSettings.PromptSave)
            {
                Display.SaveSettings();
                string choice = Console.ReadLine();
                if (choice.ToUpper() == "Y")
                {
                    AppSettings.SaveSettings();
                }
            }
        }

        public static void SetUpNative()
        {
            Tuple<string, string> folders = AppSettings.GetSelectedFolders();
            Pandoc = new Native(folders.Item1, AppSettings.InputFormat, folders.Item2, AppSettings.OutputFormat);
        }

        public static void CheckForPandoc()
        {
            if (!Native.TestPandocPresent())
            {
                Display.PandocNotFound();
                Environment.Exit(1);
            }
        }

        public static void SetSettingsIfMissing()
        {
            if (!AppSettings.AreSettingsSet())
            {
                AppSettings.SetMissingSettings();
            }
        }
    }
}
