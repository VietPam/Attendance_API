namespace HRMS_API.Models;

public record LoginModel(string Email, string Password);

public record RegisterModel(string Email, string Password, string Role);