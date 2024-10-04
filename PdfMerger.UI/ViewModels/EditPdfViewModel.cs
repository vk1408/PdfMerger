using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerger.UI.ViewModels
{
    public class EditPdfViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public EditPdfViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
