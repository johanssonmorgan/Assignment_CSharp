using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    private List<Contact> _contacts = [];

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public bool AddContact(ContactRegistrationForm registrationForm)
    {
        try
        {
            Contact contact = ContactFactory.Create(registrationForm);
            _contacts.Add(contact);
            _fileService.SaveListToFile(_contacts);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public IEnumerable<Contact> GetContacts()
    {
        try
        {
            _contacts = _fileService.LoadListFromFile();
            return _contacts;
        }

        catch (Exception ex)
        {
            return Enumerable.Empty<Contact>();
        }

    }

    public bool DeleteContact(string id)
    {
        var contacts = _fileService.LoadListFromFile();
        var contact = contacts.FirstOrDefault(contact => contact.Id == id);

        if (contact == null)
        {
            return false;
        }

        try
        {
            contacts.Remove(contact);
            _fileService.SaveListToFile(contacts);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UpdateContact(string id, Contact updatedContact)
    {
        var contacts = _fileService.LoadListFromFile();
        var contact = contacts.FirstOrDefault(contact => contact.Id == id);

        if (contact == null)
        {
            return false;
        }

        try
        {
            contacts[contacts.IndexOf(contact)] = updatedContact;
            
            _fileService.SaveListToFile(contacts);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }
}
