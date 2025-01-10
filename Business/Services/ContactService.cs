using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IFileService fileService) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private List<Contact> _contacts = [];

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
            Debug.WriteLine(ex.Message);
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
            Debug.WriteLine(ex.Message);
            return [];
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
            Debug.WriteLine(ex.Message);
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
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
