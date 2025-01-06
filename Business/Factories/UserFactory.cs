using Business.Helpers;
using Business.Models;

namespace Business.Factories;

public static class UserFactory
{
    public static UserRegistrationForm Create()
    {
        return new UserRegistrationForm();
    }

    public static User Create(UserRegistrationForm form)
    {
        return new User
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