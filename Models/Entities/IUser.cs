namespace TagerProject.Models.Entities
{
    public interface IUser
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string PhoneNumber { get; }
        string Email { get; }
        string Password { get; }
        string MembershipNumber { get; }
    }

}
