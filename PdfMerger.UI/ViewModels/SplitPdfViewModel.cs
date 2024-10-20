using PdfMerger.Core;
using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace PdfMerger.UI.ViewModels
{

    public class SplitPdfViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public SplitPdfViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.ViewChanged += OnViewChanged;
        }

        private void OnViewChanged()
        {
            if (_navigationService.CurrentViewModel is SplitPdfViewModel)
            {
                SelectedFile = null;
                IsFileSelected = false;
                DocumentSections.Clear();
            }
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
            set 
            { 
                if (_selectedFile != value)
                {
                    _selectedFile = value;
                    RaisePropertyChanged();

                    if (string.IsNullOrEmpty(_selectedFile)==false) 
                        PdfUri = new Uri(_selectedFile);
                    else
                        PdfUri = new Uri("about:blank");

                    SelectedFileChanged?.Invoke(this, PdfUri);

                }
            }
        }

        private Uri _pdfUri;
        public Uri PdfUri 
        {
            get => _pdfUri;
            set => RaiseAndAndSetIfChanged(ref _pdfUri, value);
        }

        private int _pageCount;
        public int PageCount
        {
            get => _pageCount; 
            set => RaiseAndAndSetIfChanged(ref _pageCount, value);
        }


        public event EventHandler<Uri> SelectedFileChanged;  


        public ObservableCollection<DocumentSection> DocumentSections { get; } = new ObservableCollection<DocumentSection>();

        private DocumentSection _selectedDocumentSection;

        public DocumentSection SelectedDocumentSection
        {
            get => _selectedDocumentSection;
            set => RaiseAndAndSetIfChanged(ref _selectedDocumentSection, value);
        }

        private int _sectionCount = 0;


        #region Commands
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
                PageCount = PdfEditor.GetPageCount(dialog.FileName);
            }

        });
        public RelayCommand DeselectFileCommand => new RelayCommand((par) =>
        {
            SelectedFile = null;
            IsFileSelected = false;
            PageCount = 0;
            DocumentSections.Clear();
        }, (par) => SelectedFile != null);
        public RelayCommand AddSectionCommand => new RelayCommand((par) =>
        {
            _sectionCount++;
            DocumentSections.Add(new DocumentSection($"Section {_sectionCount}"));
        },  (par) => SelectedFile != null);
        public RelayCommand DeleteSectionCommand => new RelayCommand((par) =>
        {
            _sectionCount--;
            DocumentSections.Remove(SelectedDocumentSection);
        }, (par) => (SelectedFile != null) && (SelectedDocumentSection != null));
        public RelayCommand SplitCommand => new RelayCommand((par) => 
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.Description = "Select output folder...";
            folderDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
            var result = folderDialog.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                var outputFolder = folderDialog.SelectedPath;
                PdfEditor.SplitPdfDocument(SelectedFile, DocumentSections, outputFolder, OSplitFinished);
                
            }
        
        }, (par) => SelectedFile != null && DocumentSections.Count>0 
        && ! DocumentSections.Any(x=>x.Pages.Count==0)
        && ! DocumentSections.Any(x => string.IsNullOrEmpty(x.Name)));
       
        private void OSplitFinished()
        {
            string caption = "Successfull!";
            string messageBoxText = "File successfully splited!";
            MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption);
        }
        #endregion


    }
}
