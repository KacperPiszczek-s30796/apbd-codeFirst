using System.ComponentModel.DataAnnotations;

namespace CodeFirstAproach.Model;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    public string? RefreshToken { get; set; }
}