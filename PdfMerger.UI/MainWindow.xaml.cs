
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
        private const string _fileOnePathDefaultText = "Select file one path...";
        private const string _fileTwoPathDefaultText = "Select file two path...";
        private const string _outputFolderDefaultText = "Select output folder...";
        private const string _outputFileDefaultName = "output"; 
        private const string _checked = "✔";
        private const string _unchecked = "✖";


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

            txtFileOnePath.Text = _fileOnePathDefaultText;
            txtFileTwoPath.Text = _fileTwoPathDefaultText;
            txtOutputFolderPath.Text = _outputFolderDefaultText;
            txtOutputFileName.Text = _outputFileDefaultName;

            lblFileOneSelected.Content = _unchecked;
            lblFileTwoSelected.Content = _unchecked;
            lblOutputFolderSelected.Content = _unchecked;
            //lblOutputFileNameOk.Content = _checked;

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
                lblFileOneSelected.Content = _checked;
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
                lblFileTwoSelected.Content = _checked;
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
                lblOutputFolderSelected.Content = _checked;
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
            txtFileOnePath.Text = _fileOnePathDefaultText;
            _fileOneSelected = false;
            lblFileOneSelected.Content = _unchecked;
        }

        private void btnClearFileTwoPath_Click(object sender, RoutedEventArgs e)
        {
            txtFileTwoPath.Text = _fileTwoPathDefaultText;
            _fileTwoSelected = false;
            lblFileTwoSelected.Content = _unchecked;
        }

        private void btnClearOutputPath_Click(object sender, RoutedEventArgs e)
        {
            txtOutputFolderPath.Text = _outputFolderDefaultText;
            _outputPathSelected = false;
            lblOutputFolderSelected.Content = _unchecked;
        }

        private void txtOutputFileName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutputFileName.Text) || Utils.hasSpecialChar(txtOutputFileName.Text))
            {
                _fileNameOk = false;
                lblOutputFileNameOk.Content = _unchecked;
            }
            else
            {
                _fileNameOk = true;
                lblOutputFileNameOk.Content = _checked;

            }
        }
    }
}
