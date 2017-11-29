using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileConvert;
using System.ComponentModel;

namespace Pandoc_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string OutputExtension;
        private string OutputFormat;
        public MainWindow()
        {
            InitializeComponent();
            if (!Native.TestPandocPresent())
            {
                Environment.Exit(1);
            }
        }

        private void SetInputFile(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = "";
            dlg.Filter = "";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                InputFile.Text = filename;
                Native.Instance.Input = new PandocFile(filename, System.IO.Path.GetExtension(filename));
                if(Native.Instance.Output != null)
                {
                    ConvertFileButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void SetOutputFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = OutputExtension;
            dlg.Filter = OutputFormat;

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                OutputFile.Text = filename;
                Native.Instance.Output = new PandocFile(filename, System.IO.Path.GetExtension(filename));
                if (Native.Instance.Input != null)
                {
                    ConvertFileButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void SaveFormatChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox fileFormat = sender as ComboBox;
            ComboBoxItem selection = fileFormat.SelectedValue as ComboBoxItem;

            switch (selection.Content.ToString())
            {
                case "Pdf":
                    OutputExtension = ".pdf";
                    OutputFormat = "PDF (*.pdf)|*.pdf";
                    break;
                case "Word":
                    OutputExtension = ".docx";
                    OutputFormat = "Word Document (*.docx)|*.docx";
                    break;
                case "Html":
                    OutputExtension = ".html";
                    OutputFormat = "Html file (*.html)|*.html";
                    break;
                default:
                    break;
            }
        }

        private void PandocConvert(object sender, RoutedEventArgs e)
        {

            ConvertSpinner.Visibility = Visibility.Visible;
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += DoBackgroundWork;
            backgroundWorker.RunWorkerCompleted += BackgroundTaskDone;
            backgroundWorker.RunWorkerAsync();
        }

        void DoBackgroundWork(object sender, DoWorkEventArgs e)
        {
            Native.Instance.SetFiles();
            Native.Instance.ConvertFiles();
        }

        void BackgroundTaskDone(object sender, RunWorkerCompletedEventArgs e)
        {
            ConvertSpinner.Visibility = Visibility.Hidden;
        }
    }
}
