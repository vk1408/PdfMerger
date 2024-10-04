using Microsoft.Extensions.DependencyInjection;
using PdfMerger.UI.ViewModels;
using PdfMergerUI;
using System.Windows;
using System.Windows.Controls;

namespace PdfMerger.UI.Views
{

    public partial class MergePdfView : UserControl
    {
        public MergePdfView()
        {
            InitializeComponent();
            listboxFiles.DragOver += ListboxFiles_DragOver;
            listboxFiles.Drop += ListboxFiles_Drop;
        }

        private void ListboxFiles_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            var vm = App.Current.Services.GetRequiredService<MergePdfViewModel>();

            vm.AddFiles(files);

            e.Handled = true;
        }

        private void ListboxFiles_DragOver(object sender, DragEventArgs e)
        {
            var data = e.Data;

        }

    }
}
