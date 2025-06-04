using CodeFirstAproach.DAL;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstAproach.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbContext1 _context;

    public UserRepository(DbContext1 context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}