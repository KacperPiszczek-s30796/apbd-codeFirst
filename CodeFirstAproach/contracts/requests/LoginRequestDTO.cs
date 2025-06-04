using System.ComponentModel.DataAnnotations;

namespace CodeFirstAproach.contracts.requests;

public class LoginRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}