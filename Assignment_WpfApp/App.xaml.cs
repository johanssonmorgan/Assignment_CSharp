using Assignment_WpfApp.ViewModels;
using Assignment_WpfApp.Views;
using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Assignment_WpfApp;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<IContactService, ContactService>();

                services.AddTransient<ContactListViewModel>();
                services.AddTransient<ContactAddViewModel>();
                services.AddTransient<ContactDetailsViewModel>();
                services.AddTransient<ContactEditViewModel>();

                services.AddTransient<ContactListView>();
                services.AddTransient<ContactAddView>();
                services.AddTransient<ContactDetailsView>();
                services.AddTransient<ContactEditView>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }



    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ContactListViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = mainViewModel;
        mainWindow.Show();
    }

}
