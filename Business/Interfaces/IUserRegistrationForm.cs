namespace Business.Interfaces
{
    public interface IUserRegistrationForm
    {
        string City { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string PostalCode { get; set; }
        string StreetAddress { get; set; }
    }
}