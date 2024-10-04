using Microsoft.Win32;
using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PdfMerger.UI.ViewModels
{
    public class MergePdfViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MergePdfViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ObservableCollection<string> Files { get; private set; } = new ObservableCollection<string>();
        
        private string _selectedFile;
        public string SelectedFile
        {
            get => _selectedFile; 
            set => RaiseAndAndSetIfChanged(ref _selectedFile, value);   
        }

        private int _totalFileCount;
        public int TotalFileCount
        {
            get => _totalFileCount;
            set => RaiseAndAndSetIfChanged(ref _totalFileCount, value);
        }

        private double _totalInitialSize;
        public double TotalInitialSize
        {
            get => _totalInitialSize;
            set => RaiseAndAndSetIfChanged(ref _totalInitialSize, value);   
        }

        private Dictionary<string, double> _fileSizes = new Dictionary<string, double>();

        public RelayCommand AddFileCommand => new RelayCommand((par) => 
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Select PDF files...",
                Filter = "Pdf documents (.pdf)|*.pdf",
                Multiselect = true,
            };
            bool? result = dialog.ShowDialog();

            if (result == true )
            {
                string[] files = dialog.FileNames;
                AddFiles(files);
            }

        });

        public void AddFiles(string[] files)
        {
            foreach (string file in files)
            {
                if (Files.Contains(file) || Path.GetExtension(file) != ".pdf")
                    continue;

                Files.Add(file);
                TotalFileCount++;
                var fileinfo = new FileInfo(file);
                double fileSizeInMB = fileinfo.Length / 1000000.0;
                _fileSizes.Add(file, fileSizeInMB);
                TotalInitialSize += fileSizeInMB;
            }
        }

        public RelayCommand RemoveFileCommand => new RelayCommand((par) =>
        {
            if (Files.Contains(SelectedFile))
            { 
                TotalInitialSize -= _fileSizes[SelectedFile];
                _fileSizes.Remove(SelectedFile);
                TotalFileCount--;
                Files.Remove(SelectedFile);

            }

        }, 
        (par)=>SelectedFile!=null);

        public RelayCommand MoveBack => new RelayCommand((par) =>
        {
            if (Files.Contains(SelectedFile))
            {
                var file = SelectedFile;
                var pos = Files.IndexOf(file);
                if (pos > 0)
                    Files.Move(pos, pos - 1);
            }
        },
        (par)=> SelectedFile!=null && Files.IndexOf(SelectedFile)>0);

        public RelayCommand MoveForward => new RelayCommand((par) =>
        {
            if (Files.Contains(SelectedFile))
            {
                var file = SelectedFile;
                var pos = Files.IndexOf(file);
                if (pos < Files.Count-1)
                    Files.Move(pos, pos + 1);
            }
        },
        (par)=>SelectedFile!=null && Files.IndexOf(SelectedFile)<Files.Count-1);

        public RelayCommand DeleteAllFiles => new RelayCommand((par) =>
        {
            if (Files.Count == 0)
                return;
            string messageBoxText = "Delete all files from list?";
            string caption = "Confirm";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                Files.Clear();
                _fileSizes.Clear();
                TotalInitialSize = 0;
                TotalFileCount = 0;
            }
        }, 
        (par)=>Files.Count != 0);





    }
}
