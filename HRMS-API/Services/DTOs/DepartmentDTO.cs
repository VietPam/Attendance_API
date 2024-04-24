namespace Services.DTOs;
public class DepartmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<UserDTO>? Users { get; set; } = [];
    public List<PositionDTO>? Position { get; set; } = [];
    public DepartmentDTO()
    {
        Id = 0;
        Name = "";
        Description = "";
        Users = [];
        Position = [];
    }
    public DepartmentDTO(int id, string name, string description, List<UserDTO> users, List<PositionDTO> position)
    {
        Id = id;
        Name = name;
        Description = description;
        Users = users;
        Position = position;
    }
}
