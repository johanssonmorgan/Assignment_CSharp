namespace Business.Interfaces
{
    public interface IUser
    {
        string City { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string PostalCode { get; set; }
        string StreetAddress { get; set; }
    }
}