using Microsoft.Extensions.DependencyInjection;
using PdfMerger.UI;
using PdfMerger.UI.Services.NavigationService;
using PdfMerger.UI.ViewModels;
using System;
using System.Windows;

namespace PdfMergerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var navService = Services.GetService<INavigationService>();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navService)
            };
            navService.NavigateTo<SelectionViewModel>();
            MainWindow.Show();

        }


        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<EditPdfViewModel>();
            services.AddSingleton<MergePdfViewModel>();
            services.AddSingleton<SelectionViewModel>();
            services.AddSingleton<SplitPdfViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
