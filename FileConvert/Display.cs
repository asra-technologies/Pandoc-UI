using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvert
{
    public static class Display
    {
        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to Pandoc-Tools AutoConvert");
        }

        public static void PandocNotFound()
        {
            Console.WriteLine("Pandoc was not found. Please install pandoc seperatly before using this tool");
        }

        public static void SaveSettings()
        {
            Console.Write("Do you want to save the current values for next time ? (y/n) : ");
        }

        public static void SettingsSavedSuccess()
        {
            Console.WriteLine("Settings saved successfully!");
        }

        public static void MultiplePresets(Preset[] list)
        {
            Console.WriteLine("Multiple outputs folders defined, select one:");
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine((i + 1) + ". Input: " + list[i].InputFolder + ", Output: " + list[i].OutputFolder);
            }
            Console.Write("Select between 1 and " + list.Length + " : ");
        }

        public static void SetInputFormat()
        {
            Console.Write("Set the input file format extension (ex. .md) : ");
        }

        public static void SetOutputFolder()
        {
            Console.Write("Where do you want to save the converted files (Full path to folder)? ");
        }

        public static void SetOutputFormat()
        {
            Console.Write("Set the output file format extension (ex. .docx) : ");
        }

        public static void ConversionDone(int total)
        {
            Console.WriteLine("Conversion done, total files converted: " + total);
        }
    }
}
