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
        public RelayCommand NavigateToSelectionViewCommand => new RelayCommand((par) => { _navigationService.NavigateTo<SelectionViewModel>(); });


    }
}
