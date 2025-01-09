using Business.Interfaces;

namespace Business.Models;

public class Contact : ContactBase, IContact
{
    public string Id { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
}
