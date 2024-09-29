using Microsoft.Extensions.DependencyInjection;
using PdfMerger.UI.MVVM;
using PdfMerger.UI.Services.NavigationService;
using PdfMergerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerger.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
      

        public SelectionViewModel SelectionViewModel { get; private set; }
        public EditPdfViewModel EditPdfViewModel { get; private set; }
        public MergePdfViewModel MergePdfViewModel { get; private set; }
        public SplitPdfViewModel SplitPdfViewModel { get; private set; }


        public MainWindowViewModel()
        {
            InitViewModels();
          

        }

        private void InitViewModels()
        {
            SelectionViewModel = new SelectionViewModel(this,App.Current.Services.GetService<INavigationService>());
            EditPdfViewModel = new EditPdfViewModel();
            MergePdfViewModel = new MergePdfViewModel();
            SplitPdfViewModel = new SplitPdfViewModel();
        }
    }
}
