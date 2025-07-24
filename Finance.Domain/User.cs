using Finance.Shared.ApplicationFiles;
using Microsoft.AspNetCore.Identity;

namespace Finance.Domain;

public class User : IdentityUser
{
    private User() { }

    public User(string id,
        string firstName,
        string lastName,
        string email,
        FileDto image)
    {
        Id = id.ToString();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Image = image;

        UserName = firstName + lastName;
    }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public FileDto Image { get; private set; } = default!;
}
