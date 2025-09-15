using Microsoft.EntityFrameworkCore;
using NextFit.Data;
using NextFit.Models;

namespace NextFit.Repositories
{
    public class AulaRepository : IAulaRepository
    {
        private readonly AcademiaContext _ctx;
        public AulaRepository(AcademiaContext ctx) => _ctx = ctx;

        public async Task<Aula> AddAsync(Aula aula)
        {
            _ctx.Aulas.Add(aula);
            await _ctx.SaveChangesAsync();
            return aula;
        }

        public Task<List<Aula>> GetAllAsync() =>
            _ctx.Aulas
                .Include(a => a.Agendamentos)
                .ToListAsync();

        public Task<Aula?> GetByIdAsync(Guid id) =>
            _ctx.Aulas
                .Include(a => a.Agendamentos)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task UpdateAsync(Aula entity)
        {
            _ctx.Aulas.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _ctx.Aulas.FindAsync(id);
            if (e == null) return;
            _ctx.Aulas.Remove(e);
            await _ctx.SaveChangesAsync();
        }
    }
}
