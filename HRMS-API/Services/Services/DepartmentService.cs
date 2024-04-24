using Domain.Entities.Departments;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Services;
public class DepartmentService(ApplicationDbContext context)
{
    public async Task InitAsync()
    {
        if (await context.Departments.AnyAsync())
        {
            return;
        }

        SqlDepartment department = new()
        {
            Name = "Initial Department",
            Code = "Initial Code",
            Description = "Initial Department Description"
        };
        await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();
    }
    //public async Task<List<DepartmentDTO>> GetAllAsync()
    //{
    //    List<Department> departments = await context.Departments.ToListAsync();
    //    return departments.Select(x => x.ToDTO()).ToList();
    //}

    //public async Task<DepartmentDTO> GetAsync(int id)
    //{
    //    Department? department = await context.Departments.FindAsync(id);

    //    if (department == null)
    //    {
    //        return new DepartmentDTO();
    //    }

    //    return department.ToDTO();
    //}

    //public async Task<bool> CreateOne(string Name, string Description)
    //{
    //    Department department = new()
    //    {
    //        Name = Name,
    //        Description = Description
    //    };
    //    await context.Departments.AddAsync(department);
    //    await context.SaveChangesAsync();
    //    return true;
    //}

    //public async Task<bool> UpdateOne(int id, string Name, string Description)
    //{
    //    Department? department = await context.Departments.FindAsync(id);
    //    if (department == null)
    //    {
    //        return false;
    //    }

    //    department.Name = Name;
    //    department.Description = Description;

    //    await context.SaveChangesAsync();
    //    return true;
    //}

    //public async Task<bool> DeleteOne(int id)
    //{
    //    Department? department = await context.Departments.FindAsync(id);
    //    if (department == null)
    //    {
    //        return false;
    //    }

    //    context.Departments.Remove(department);
    //    await context.SaveChangesAsync();
    //    return true;
    //}
}
