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
      

        private readonly INavigationService _navigationService;

        public ViewModelBase CurrentViewModel => _navigationService.CurrentViewModel;

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.ViewChanged += OnViewChanged;
        }

        private void OnViewChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
