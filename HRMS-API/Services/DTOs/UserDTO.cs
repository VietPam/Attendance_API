namespace Services.DTOs;
public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";
    public string Avatar { get; set; } = "";
    public DateTime DateOfBirth { get; set; }

    public UserDTO()
    {
        Id = 0;
        Name = "";
        Email = "";
        Phone = "";
        Address = "";
        Avatar = "";
        DateOfBirth = DateTime.UtcNow;
    }
    public UserDTO(int id, string name, string email, string phone, string address, string avatar, DateTime dateOfBirth)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        Avatar = avatar;
        DateOfBirth = dateOfBirth;
    }
}
