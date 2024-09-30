using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PdfMerger.UI.ViewModels
{
    public class SelectionViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SelectionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
    
        }
        public RelayCommand NavigateToEditPdfViewCommand => new RelayCommand((par) => { _navigationService.NavigateTo<EditPdfViewModel>(); });

        public RelayCommand NavigateToMergePdfViewCommand => new RelayCommand((par) => { _navigationService.NavigateTo<MergePdfViewModel>(); });

        public RelayCommand NavigateToSplitPdfViewCommand => new RelayCommand((par) => { _navigationService.NavigateTo<SplitPdfViewModel>(); });
  
    }
}
