
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
        private bool _fileNameOk;
        private const string FileOnePathDefaultText = "Select file one path...";
        private const string FileTwoPathDefaultText = "Select file two path...";
        private const string OutputFolderDefaultText = "Select output folder...";
        private const string OutputFileDefaultName = "output"; 
        private const string Checked = "✔";
        private const string Unchecked = "✖";

        public MainWindow()
        {
            InitializeComponent();
            ResetAll();
        }

        private void ResetAll()
        {
            _fileOneSelected = false;
            _fileTwoSelected = false;
            _outputPathSelected = false;
            _fileNameOk = false;

            txtFileOnePath.Text = FileOnePathDefaultText;
            txtFileTwoPath.Text = FileTwoPathDefaultText;
            txtOutputFolderPath.Text = OutputFolderDefaultText;
            txtOutputFileName.Text = OutputFileDefaultName;

            lblFileOneSize.Content = string.Empty;
            lblFileTwoSize.Content = string.Empty;

            lblFileOneSelected.Content = Unchecked;
            lblFileTwoSelected.Content = Unchecked;
            lblOutputFolderSelected.Content = Unchecked;

            txtError.Text = string.Empty;
        }

        private void btnSelectfileOnePath_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf;";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                txtFileOnePath.Text = openFileDialog.FileName;
                _fileOneSelected = true;
                lblFileOneSelected.Content = Checked;
                long fileLenghtKB = new FileInfo(openFileDialog.FileName).Length / 1024;  
                lblFileOneSize.Content = $"{fileLenghtKB} KB";
            }
     
        }

        private void btnSelectFileTwoPath_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf;";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                txtFileTwoPath.Text = openFileDialog.FileName;
                _fileTwoSelected = true;
                lblFileTwoSelected.Content = Checked;
                long fileLenghtKB = new FileInfo(openFileDialog.FileName).Length / 1024;
                lblFileTwoSize.Content = $"{fileLenghtKB} KB";
            }
        }

        private void btnSelectOutputPath_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new WinForms.FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowser.SelectedPath))
            {
                txtOutputFolderPath.Text = folderBrowser.SelectedPath;
                _outputPathSelected = true;
                lblOutputFolderSelected.Content = Checked;
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
            else if (_fileNameOk == false)
            {
                MessageBox.Show("Inappropriate file name!");
                return;
            }
            else
            {
                try
                {
                    string outputFilePath = Path.Combine(txtOutputFolderPath.Text, txtOutputFileName.Text + ".pdf");
                    PdfFileMerger.MergePdfFiles(txtFileOnePath.Text, txtFileTwoPath.Text, outputFilePath);
                    txtError.Text = "Files succesfully merged!";
                }
                catch (Exception ex)
                {
                    txtError.Text = $"Merging files failed. {ex.Message}";
                }
            }
       
        }

        private void btnClearFileOnePath_Click(object sender, RoutedEventArgs e)
        {
            txtFileOnePath.Text = FileOnePathDefaultText;
            _fileOneSelected = false;
            lblFileOneSelected.Content = Unchecked;
            lblFileOneSize.Content = string.Empty;
        }

        private void btnClearFileTwoPath_Click(object sender, RoutedEventArgs e)
        {
            txtFileTwoPath.Text = FileTwoPathDefaultText;
            _fileTwoSelected = false;
            lblFileTwoSelected.Content = Unchecked;
            lblFileTwoSize.Content = string.Empty;
        }

        private void btnClearOutputPath_Click(object sender, RoutedEventArgs e)
        {
            txtOutputFolderPath.Text = OutputFolderDefaultText;
            _outputPathSelected = false;
            lblOutputFolderSelected.Content = Unchecked;
        }

        private void txtOutputFileName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutputFileName.Text) || Utils.HasSpecialChar(txtOutputFileName.Text))
            {
                _fileNameOk = false;
                lblOutputFileNameOk.Content = Unchecked;
            }
            else
            {
                _fileNameOk = true;
                lblOutputFileNameOk.Content = Checked;

            }
        }
    }
}
