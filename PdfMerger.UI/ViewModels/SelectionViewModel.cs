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
        private readonly MainWindowViewModel _mainWindowViewModel;
        public SelectionViewModel(MainWindowViewModel mainWindowViewModel, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _mainWindowViewModel = mainWindowViewModel;
        }
        public RelayCommand NavigateToEditPdfViewCommand => new RelayCommand(execute=>NavigateToEditPdfView());
        
        public void NavigateToEditPdfView()
        {
            _navigationService.NavigateTo(_mainWindowViewModel.EditPdfViewModel);
        }

        public RelayCommand NavigateToMergePdfViewCommand => new RelayCommand(execute => NavigateToEditPdfView());
        public void NavigateToMergePdfView()
        {
            _navigationService.NavigateTo(_mainWindowViewModel.MergePdfViewModel);

        }

        public RelayCommand NavigateToSplitPdfViewCommand => new RelayCommand(execute => NavigateToEditPdfView());
        public void NavigateToSplitPdfView()
        {
            _navigationService.NavigateTo(_mainWindowViewModel.SplitPdfViewModel);

        }
    }
}
