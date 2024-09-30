using PdfMerger.UI.MVVM;
using PdfMerger.UI.ViewModels;
using PdfMerger.UI.Views;
using PdfMergerUI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PdfMerger.UI.Services.NavigationService
{
    public interface INavigationService
    {
        void NavigateTo<T>() where T : ViewModelBase;
        ViewModelBase CurrentViewModel { get; }
        Action ViewChanged { get; set; }
    }

    public class NavigationService : INavigationService
    {
        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {

            var viewModel = (ViewModelBase) App.Current.Services.GetService(typeof(TViewModel));

            CurrentViewModel = viewModel;

            OnViewChanged();
        }
        public ViewModelBase CurrentViewModel { get; private set; }

        public Action ViewChanged { get; set; }

        private void OnViewChanged()
        {
            ViewChanged?.Invoke();
        }
    }
}
