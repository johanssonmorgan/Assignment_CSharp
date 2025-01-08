using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace Assignment_WpfApp.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    private readonly IContactService _contactService;
    private readonly IServiceProvider _serviceProvider;

    public ContactAddViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;

        // Subscribe to Contact.PropertyChanged to update CanSave dynamically
        Contact.PropertyChanged += (sender, args) =>
        {
            OnPropertyChanged(nameof(CanSave));
            SaveCommand.NotifyCanExecuteChanged(); // Update the button state
        };
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private ContactRegistrationForm _contact = ContactFactory.Create();

    // Property to track if the form is valid
    public bool CanSave
    {
        get
        {
            var properties = TypeDescriptor.GetProperties(Contact);
            foreach (PropertyDescriptor property in properties)
            {
                if (!string.IsNullOrEmpty(((IDataErrorInfo)Contact)[property.Name]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        _contactService.AddContact(Contact);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}