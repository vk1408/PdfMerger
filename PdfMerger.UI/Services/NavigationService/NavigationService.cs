using PdfMerger.UI.MVVM;
using PdfMerger.UI.ViewModels;
using PdfMerger.UI.Views;
using System;
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
        void NavigateTo(ViewModelBase viewModel);
        ViewModelBase CurrentViewModel { get; }
    }

    public class NavigationService : INavigationService
    {
        private  MainWindow _mainWindow => Application.Current.MainWindow as MainWindow;
        private readonly Dictionary<ViewModelBase, UserControl> _mapping = new Dictionary<ViewModelBase, UserControl>();

        public NavigationService()
        {
       
        }

        public void NavigateTo(ViewModelBase viewModel)
        {
    
            if (!_mapping.ContainsKey(viewModel))
            {
                if (viewModel is EditPdfViewModel)
                    _mapping.Add(viewModel, new EditPdfView());
                else if (viewModel is MergePdfViewModel)
                    _mapping.Add(viewModel, new MergePdfView());
                else if (viewModel is SelectionViewModel)
                    _mapping.Add(viewModel, new SelectionView());
                else if (viewModel is SplitPdfViewModel)
                    _mapping.Add(viewModel, new SplitPdfView());

                _mapping[viewModel].DataContext = viewModel;
            }

            _mainWindow.cconCurrentView.Content = _mapping[viewModel];

            CurrentViewModel = viewModel;
        }
        public ViewModelBase CurrentViewModel { get; private set; }
    }
}
