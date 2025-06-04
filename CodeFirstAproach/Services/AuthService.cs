using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;
using CodeFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirstAproach.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IOptions<JwtSettings> jwtOptions)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<bool> RegisterAsync(string username, string password, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(username, cancellationToken);
        if (existingUser != null)
            return false;

        var newUser = new User
        {
            Username = username
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);

        await _userRepository.AddAsync(newUser, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<TokenPairDto?> LoginAsync(string username, string password, CancellationToken ct)
    {
        var user = await _userRepository.GetByUsernameAsync(username, ct);
        if (user is null)
            return null;

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (verificationResult != PasswordVerificationResult.Success)
            return null;

        var accessToken = GenerateJwtToken(user);
        var refreshToken = Guid.NewGuid().ToString();

        user.RefreshToken = refreshToken;
        await _userRepository.UpdateAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);

        return new TokenPairDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: null,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task<TokenPairDto?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

        if (user == null)
            return null;

        var newAccessToken = GenerateJwtToken(user);
        var newRefreshToken = Guid.NewGuid().ToString();

        user.RefreshToken = newRefreshToken;
        await _userRepository.UpdateAsync(user, cancellationToken);

        return new TokenPairDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}