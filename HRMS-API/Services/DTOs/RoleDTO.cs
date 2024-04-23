namespace Services.DTOs;
public class RoleDTO
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public RoleDTO()
    {
        Name = "";
        Code = "";
        Description = "";
    }

    public RoleDTO(string name, string code, string description)
    {
        Name = name;
        Code = code;
        Description = description;
    }
}
