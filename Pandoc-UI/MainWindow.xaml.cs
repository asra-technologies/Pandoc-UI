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
    }
}
