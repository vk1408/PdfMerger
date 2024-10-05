using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;

namespace PdfMerger.UI.ViewModels
{

    public class SplitPdfViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public SplitPdfViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private bool _isFileSelected;
        public bool IsFileSelected
        {
            get => _isFileSelected;
            set => RaiseAndAndSetIfChanged(ref _isFileSelected, value);
        }

        private string _selectedFile;
        public string SelectedFile
        {
            get => _selectedFile;
            set => RaiseAndAndSetIfChanged(ref _selectedFile, value);
        }
        public RelayCommand SelectFileCommand => new RelayCommand((par) =>
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Select PDF files...",
                Filter = "Pdf documents (.pdf)|*.pdf",
                Multiselect = false,
            };
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                SelectedFile = dialog.FileName;
                IsFileSelected = true;
            }

        });

        public RelayCommand DeselectFileCommand => new RelayCommand((par) => 
        { 
            SelectedFile = null; 
            IsFileSelected = false; 
        }, (par)=>SelectedFile!=null);





    }
}
