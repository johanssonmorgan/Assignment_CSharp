using Business.Helpers;
using Business.Models;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactRegistrationForm Create()
    {
        return new ContactRegistrationForm();
    }

    public static Contact Create(ContactRegistrationForm form)
    {
        return new Contact
        {
            Id = IdGenerator.Generate(),
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            Phone = form.Phone,
            StreetAddress = form.StreetAddress,
            PostalCode = form.PostalCode,
            City = form.City
        };
    }
}