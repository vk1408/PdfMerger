
using System.Windows;
using Microsoft.Win32;
using WinForms=System.Windows.Forms;
using pdfMergerClassLibrary;


namespace pdfMergerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            fileOneSelectedImage.Visibility = Visibility.Hidden;
            fileTwoSelectedImage.Visibility = Visibility.Hidden;
            outputFolderSelectedImage.Visibility = Visibility.Hidden;

            clearFileOnePathButton.Visibility = Visibility.Hidden;
            clearFileTwoPathButton.Visibility = Visibility.Hidden;
            clearOutputPathButton.Visibility = Visibility.Hidden;

        }

        private void fileOneButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF document (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                pdfMerger.fileOnePath = openFileDialog.FileName;
                if (pdfMerger.fileOnePath != string.Empty)
                {
                    folderOnePath.Text = pdfMerger.fileOnePath;
                    fileOneSelectedImage.Visibility = Visibility.Visible;
                    pdfMerger.fileOneSelected = true;
                    clearFileOnePathButton.Visibility=Visibility.Visible;
                }
                

            }
        }

        private void fileTwoButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF document (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                pdfMerger.fileTwoPath = openFileDialog.FileName;
                if (pdfMerger.fileTwoPath != string.Empty)
                {
                    folderTwoPath.Text = pdfMerger.fileTwoPath;
                    fileTwoSelectedImage.Visibility = Visibility.Visible;
                    pdfMerger.fileTwoSelected = true;
                    clearFileTwoPathButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void outputPathButton_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new WinForms.FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            WinForms.DialogResult result = folderBrowser.ShowDialog();
            if (result == WinForms.DialogResult.OK)
            {
                pdfMerger.outputPath = folderBrowser.SelectedPath;
                if (pdfMerger.outputPath != string.Empty)
                {
                    outputFolderPath.Text = pdfMerger.outputPath;
                    outputFolderSelectedImage.Visibility = Visibility.Visible;
                    pdfMerger.outputPathSelected = true;
                    clearOutputPathButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void mergeFilesButton_Click(object sender, RoutedEventArgs e)
        {
            pdfMerger.mergedFileName = MergedFileName.Text+".pdf";
            if (pdfMerger.fileOneSelected && pdfMerger.fileTwoSelected && pdfMerger.outputPathSelected && pdfMerger.mergedFileName!=string.Empty)
            {
                pdfMerger.mergePdfFiles();
                if (pdfMerger.error)
                {
                    mergeResult.Text = pdfMerger.errorMessage;
                }
                else
                {
                    // Files succesfully merged!
                    mergeResult.Text = "Done!";
                }
            }
            else 
            {
                // Clean output
                mergeResult.Text = string.Empty;
                // Check if all of the files and output path were selected
                if (pdfMerger.fileOneSelected == false)
                    mergeResult.Text += "Please select file one! \n";
                if (pdfMerger.fileTwoSelected == false)
                    mergeResult.Text += "Please select file two!  \n";
                if (pdfMerger.outputPathSelected == false)
                    mergeResult.Text += "Please select output folder! ";
            }

        }

        private void clearFileOnePathButton_Click(object sender, RoutedEventArgs e)
        {
            pdfMerger.fileOnePath = string.Empty;
            folderOnePath.Text = "File 1 path...";
            pdfMerger.fileOneSelected = false;
            clearFileOnePathButton.Visibility = Visibility.Hidden;
            fileOneSelectedImage.Visibility = Visibility.Hidden;
        }

        private void clearFileTwoPathButton_Click(object sender, RoutedEventArgs e)
        {
            pdfMerger.fileTwoPath = string.Empty;
            folderTwoPath.Text = "File 2 path...";
            pdfMerger.fileTwoSelected = false;
            clearFileTwoPathButton.Visibility = Visibility.Hidden;
            fileTwoSelectedImage.Visibility = Visibility.Hidden;

        }

        private void clearOutputPathButton_Click(object sender, RoutedEventArgs e)
        {
            pdfMerger.outputPath = string.Empty;
            outputFolderPath.Text = "Output path...";
            pdfMerger.outputPathSelected = false;
            clearOutputPathButton.Visibility = Visibility.Hidden;
            outputFolderSelectedImage.Visibility = Visibility.Hidden;
        }
    }
}
