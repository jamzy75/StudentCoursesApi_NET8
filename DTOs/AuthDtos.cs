using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.DTOs;

public class RegisterDto
{
    [Required, MaxLength(50)]
    public string UserName { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required, MinLength(6)]
    public string Password { get; set; } = default!;
}

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
}