using Microsoft.Extensions.DependencyInjection;
using PdfMerger.UI.ViewModels;
using PdfMergerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PdfMerger.UI.Views
{
    /// <summary>
    /// Interaction logic for SplitPdfView.xaml
    /// </summary>
    public partial class SplitPdfView : UserControl
    {
        public SplitPdfView()
        {
            InitializeComponent();
            var viewModel = App.Current.Services.GetRequiredService<SplitPdfViewModel>();
            viewModel.SelectedFileChanged += ViewModel_SelectedFileChanged;
        }

        private void ViewModel_SelectedFileChanged(object sender, Uri pdfPath)
        {
            webBrowserPdfPreview.Navigate(pdfPath);
        }
    }
}
