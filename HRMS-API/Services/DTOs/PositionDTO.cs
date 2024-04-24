namespace Services.DTOs;
public class PositionDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Salary { get; set; }
    public int Coefficient { get; set; }
    public PositionDTO()
    {
        Id = 0;
        Name = "";
        Description = "";
        Salary = 0;
        Coefficient = 0;
    }
    public PositionDTO(int id, string name, string description, decimal salary, int coefficient)
    {
        Id = id;
        Name = name;
        Description = description;
        Salary = salary;
        Coefficient = coefficient;
    }
}
