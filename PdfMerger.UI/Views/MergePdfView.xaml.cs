using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using PdfMerger.Core;
using PdfMerger.UI.ViewModels;
using PdfMergerUI;
using WinForms = System.Windows.Forms;

namespace PdfMerger.UI.Views
{
    /// <summary>
    /// Interaction logic for MergePdfView.xaml
    /// </summary>
    public partial class MergePdfView : UserControl
    {
        //private bool _fileOneSelected;
        //private bool _fileTwoSelected;
        //private bool _outputPathSelected;
        //private bool _fileNameOk;
        //private const string FileOnePathDefaultText = "Select file one path...";
        //private const string FileTwoPathDefaultText = "Select file two path...";
        //private const string OutputFolderDefaultText = "Select output folder...";
        //private const string OutputFileDefaultName = "output";
        //private const string Checked = "✔";
        //private const string Unchecked = "✖";
        public MergePdfView()
        {
            InitializeComponent();
            listboxFiles.DragOver += ListboxFiles_DragOver;
            listboxFiles.Drop += ListboxFiles_Drop;
            //ResetAll();

        }

        private void ListboxFiles_Drop(object sender, DragEventArgs e)
        {
            if (! e.Data.GetDataPresent(DataFormats.FileDrop) )
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
