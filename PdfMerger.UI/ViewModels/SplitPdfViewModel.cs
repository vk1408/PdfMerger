using PdfMerger.Core;
using PdfMerger.UI.Models;
using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

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
        }, (par)=>SelectedFile!=null);

        public ObservableCollection<DocumentSection> DocumentSections { get; } = new ObservableCollection<DocumentSection>();

        public int SectionCount => DocumentSections.Count;

        private DocumentSection _selectedDocumentSection;

        public DocumentSection SelectedDocumentSection
        {
            get => _selectedDocumentSection;
            set => RaiseAndAndSetIfChanged(ref _selectedDocumentSection, value);
        }

        public RelayCommand AddSectionCommand => new RelayCommand((par) =>
        {
            DocumentSections.Add(new DocumentSection($"Section {DocumentSections.Count + 1}"));
        }, (par) => SelectedFile != null);

        public RelayCommand DeleteSectionCommand => new RelayCommand((par) =>
        {
            DocumentSections.Remove(SelectedDocumentSection);
        }, (par) => (SelectedFile != null) && (SelectedDocumentSection!=null));


    }
}
