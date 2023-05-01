
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
        }

        private void fileOneButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                pdfMerger.fileOnePath = openFileDialog.FileName;
                folderOnePath.Content = openFileDialog.FileName;
            }
        }

        private void fileTwoButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                pdfMerger.fileTwoPath = openFileDialog.FileName;
                folderTwoPath.Content = openFileDialog.FileName;
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
                outputFolderPath.Content = folderBrowser.SelectedPath;
            }
        }

        private void mergeFilesButton_Click(object sender, RoutedEventArgs e)
        {
            pdfMerger.mergePdfFiles();
            if (pdfMerger.error)
            {
                mergeResult.Content = pdfMerger.errorMessage;
            }
            else
            mergeResult.Content = "Done!";
        }
    }
}
