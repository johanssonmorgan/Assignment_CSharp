using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace Assignment_WpfApp.ViewModels;

/// <summary>
/// ViewModel for editing an existing contact, with validation and data handling.
/// </summary>
public partial class ContactEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    /// <summary>
    /// Initializes a new instance of the ContactEditViewModel.
    /// </summary>
    /// <param name="serviceProvider">Service provider for resolving dependencies.</param>
    /// <param name="contactService">Service for managing contacts.</param>
    /// <param name="contact">The contact to be edited.</param>
    public ContactEditViewModel(IServiceProvider serviceProvider, IContactService contactService, Contact contact)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;

        // Assign the contact for editing
        Contact = contact;

        // Subscribe to property changes in Contact to update validation
        Contact.PropertyChanged += (sender, args) =>
        {
            OnPropertyChanged(nameof(CanSave));
            SaveCommand.NotifyCanExecuteChanged();
        };
    }

    /// <summary>
    /// The contact being edited.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private Contact _contact;

    /// <summary>
    /// Determines if the form is valid and can be saved.
    /// </summary>
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

    /// <summary>
    /// Saves the edited contact if the form is valid.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        var result = _contactService.UpdateContact(Contact.Id, Contact);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
        }
    }

    /// <summary>
    /// Deletes the contact and navigates back to the contact list.
    /// </summary>
    [RelayCommand]
    private void Delete()
    {
        var result = _contactService.DeleteContact(Contact.Id);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
        }
    }

    /// <summary>
    /// Cancels the edit operation and navigates back to the contact list.
    /// </summary>
    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}
