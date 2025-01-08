using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        bool AddContact(ContactRegistrationForm registrationForm);
        IEnumerable<Contact> GetContacts();
        bool DeleteContact(string id);
        bool UpdateContact(string contactId, Contact updatedContact);
    }
}