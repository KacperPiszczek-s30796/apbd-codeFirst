using CodeFirstAproach.DAL;
using CodeFirstAproach.Repositories.abstractions;

namespace CodeFirstAproach.Repositories;

public class DoctorRepository: IDoctorRepository
{
    private DbContext1 _context1;

    public DoctorRepository(DbContext1 context1)
    {
        _context1 = context1;
    }

}