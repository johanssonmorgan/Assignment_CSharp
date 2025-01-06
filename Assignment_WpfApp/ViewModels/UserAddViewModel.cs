using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_WpfApp.ViewModels;

public partial class UserAddViewModel(IUserService userService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IUserService _userService = userService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private UserRegistrationForm _user = UserFactory.Create();

    [RelayCommand]
    private void Save()
    {
        _userService.AddUser(User);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }
}