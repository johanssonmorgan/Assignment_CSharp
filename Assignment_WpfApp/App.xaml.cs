﻿using Assignment_WpfApp.ViewModels;
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
                services.AddSingleton<IUserService, UserService>();

                services.AddTransient<UserListViewModel>();
                services.AddTransient<UserAddViewModel>();
                services.AddTransient<UserDetailsViewModel>();
                services.AddTransient<UserEditViewModel>();

                services.AddTransient<UserListView>();
                services.AddTransient<UserAddView>();
                services.AddTransient<UserDetailsView>();
                services.AddTransient<UserEditView>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }



    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<UserListViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = mainViewModel;
        mainWindow.Show();
    }

}
