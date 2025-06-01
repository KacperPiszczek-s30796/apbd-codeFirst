using CodeFirstAproach.DAL;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstAproach.Repositories;

public class MedicamentRepository: IMedicamentRepository
{
    private DbContext1 _context1;

    public MedicamentRepository(DbContext1 context1)
    {
        _context1 = context1;
    }

    public async Task<bool> DoesMedicamentExist(int id, CancellationToken token)
    {
        return await _context1.Medicaments
            .AnyAsync(m => m.IdMedicament == id, token);
    }
}