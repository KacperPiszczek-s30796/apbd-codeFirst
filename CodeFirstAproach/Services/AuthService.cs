using System.Collections.Concurrent;
using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.Model;
using CodeFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Identity;

namespace CodeFirstAproach.Services;

public class AuthService : IAuthService
{
    private static readonly ConcurrentDictionary<string, User> _users = new();
    private readonly PasswordHasher<User> _passwordHasher = new();

    public async Task<bool> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
        
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return false;
        
        if (_users.ContainsKey(request.Username))
            return false;
        
        var user = new User
        {
            Username = request.Username
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        
        return _users.TryAdd(user.Username, user);
    }
}