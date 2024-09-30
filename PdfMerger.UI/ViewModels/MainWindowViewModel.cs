using Microsoft.Extensions.DependencyInjection;
using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using PdfMergerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PdfMerger.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
      

        public SelectionViewModel SelectionViewModel { get; private set; }
        public EditPdfViewModel EditPdfViewModel { get; private set; }
        public MergePdfViewModel MergePdfViewModel { get; private set; }
        public SplitPdfViewModel SplitPdfViewModel { get; private set; }



        private readonly INavigationService _navigationService;

        public ViewModelBase CurrentViewModel => _navigationService.CurrentViewModel;

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.ViewChanged += OnViewChanged;
            SelectionViewModel = new SelectionViewModel(_navigationService);
            EditPdfViewModel = new EditPdfViewModel(_navigationService);
            MergePdfViewModel = new MergePdfViewModel(_navigationService);
            SplitPdfViewModel = new SplitPdfViewModel(_navigationService);
        }

        private void OnViewChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
