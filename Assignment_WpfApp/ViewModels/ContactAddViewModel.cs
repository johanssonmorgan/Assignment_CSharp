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

    // Had help making the validation work from ChatGPT, have tweaked most of it and adjusted it to work with my structure.

    /// <summary>
    /// Sets up the ViewModel with services and subscribes to form changes to update validation and button states.
    /// </summary>
    /// <param name="contactService">Service to manage contact data.</param>
    /// <param name="serviceProvider">Service provider for resolving dependencies.</param>
    public ContactAddViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;

        Contact.PropertyChanged += (sender, args) =>
        {
            OnPropertyChanged(nameof(CanSave));
            SaveCommand.NotifyCanExecuteChanged();
        };
    }

    /// <summary>
    /// The contact form data being added.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private ContactRegistrationForm _contact = ContactFactory.Create();

    /// <summary>
    /// Checks if the form is valid and can be saved.
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
    /// Saves the new contact if the form is valid and switches back to the contact list view.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        _contactService.AddContact(Contact);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

    /// <summary>
    /// Cancels the operation and switches back to the contact list view.
    /// </summary>
    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}