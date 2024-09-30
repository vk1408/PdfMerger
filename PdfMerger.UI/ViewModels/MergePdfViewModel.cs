using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerger.UI.ViewModels
{
    public class MergePdfViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MergePdfViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public RelayCommand NavigateToSelectionViewCommand => new RelayCommand((par) => { _navigationService.NavigateTo<SelectionViewModel>(); });


    }
}
