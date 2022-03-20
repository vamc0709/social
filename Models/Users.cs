using Social.DTOs;

namespace Social.Models;



public record User
{

    public long UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public long Mobile { get; set; }


    public string Email { get; set; }

    public UserDTO asDto => new UserDTO
    {
        UserId = UserId,
        UserName = UserName,
        FirstName = FirstName,
        LastName = LastName,
        Mobile = Mobile,
        Email = Email,

    };
}