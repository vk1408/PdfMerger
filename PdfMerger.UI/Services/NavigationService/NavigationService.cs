using PdfMerger.UI.MVVM;
using PdfMerger.UI.ViewModels;
using PdfMerger.UI.Views;
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
        private MainWindow _mainWindow => Application.Current.MainWindow as MainWindow;
        private MainWindowViewModel _mainWindowViewModel => Application.Current.MainWindow.DataContext as MainWindowViewModel;


        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel;

            if (typeof(TViewModel) == typeof(EditPdfViewModel))
                viewModel = _mainWindowViewModel.EditPdfViewModel;
            else if (typeof(TViewModel) == typeof(MergePdfViewModel))
                viewModel = _mainWindowViewModel.MergePdfViewModel;
            else if (typeof(TViewModel) == typeof(SelectionViewModel))
                viewModel = _mainWindowViewModel.SelectionViewModel;
            else if (typeof(TViewModel) == typeof(SplitPdfViewModel))
                viewModel = _mainWindowViewModel.SplitPdfViewModel;
            else
                throw new NotImplementedException();

            
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
