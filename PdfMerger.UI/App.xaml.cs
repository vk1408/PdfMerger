using Microsoft.Extensions.DependencyInjection;
using PdfMerger.UI;
using PdfMerger.UI.Services.NavigationService;
using PdfMerger.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindow = new MainWindow();
            this.MainWindow = mainWindow;
            this.MainWindow.DataContext = mainWindowViewModel;


        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {

            var navService = App.Current.Services.GetService<INavigationService>();
            var mainWindowViewModel = (this.MainWindow as MainWindow).DataContext as MainWindowViewModel;
            navService.NavigateTo(mainWindowViewModel.SelectionViewModel);
            Application.Current.MainWindow.Show();
        }

 
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<INavigationService, NavigationService>();

            return services.BuildServiceProvider();
        }

    }
}
