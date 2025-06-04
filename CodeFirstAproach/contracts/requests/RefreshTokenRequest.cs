using System.ComponentModel.DataAnnotations;

namespace CodeFirstAproach.contracts.requests;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}