
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using PdfMerger.Core;
using WinForms=System.Windows.Forms;



namespace PdfMerger.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _fileOneSelected;
        private bool _fileTwoSelected;
        private bool _outputPathSelected;
        private bool _fileNameOk = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileOneButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf;";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                folderOnePath.Text = openFileDialog.FileName;
                _fileOneSelected = true;
            }
     
        }

        private void fileTwoButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf;";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                folderTwoPath.Text = openFileDialog.FileName;
                _fileTwoSelected = true;
            }
        }

        private void outputPathButton_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new WinForms.FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowser.SelectedPath))
            {
                outputFolderPath.Text = folderBrowser.SelectedPath;
                _outputPathSelected = true;
            }
        }

        private void mergeFilesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_fileOneSelected==false)
            {
                MessageBox.Show("Select file one!");
                return;
            }
            else if (_fileTwoSelected == false)
            {
                MessageBox.Show("Select file two!");
                return;
            }
            else if (_outputPathSelected == false)
            {
                MessageBox.Show("Select output path!");
                return;
            }
            else
            {
                try
                {
                    string outputFilePath = Path.Combine(outputFolderPath.Text, outputFileName.Text+".pdf");
                    PdfFileMerger.MergePdfFiles(folderOnePath.Text, folderTwoPath.Text, outputFilePath);
                    mergeResult.Text = "Files succesfully merged!";
                }
                catch (Exception ex)
                {
                    mergeResult.Text = $"Merging files failed. {ex.Message}";
                }
            }
       
        }

        private void clearFileOnePathButton_Click(object sender, RoutedEventArgs e)
        {
            folderOnePath.Text = "Select file 1 path...";
        }

        private void clearFileTwoPathButton_Click(object sender, RoutedEventArgs e)
        {
            folderTwoPath.Text = "Select file 2 path...";
        }

        private void clearOutputPathButton_Click(object sender, RoutedEventArgs e)
        {
            outputFolderPath.Text = "Select output path...";
        }

        private void mergedFileName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(outputFileName.Text) && !Utils.hasSpecialChar(outputFileName.Text))
            {
                outputFileNameImage.Visibility = Visibility.Visible;
            }
            else
            {
                outputFileNameImage.Visibility = Visibility.Hidden;
            }
        }
    }
}
