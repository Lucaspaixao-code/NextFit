using Microsoft.EntityFrameworkCore;
using NextFit.Data;
using NextFit.Models;

namespace NextFit.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AcademiaContext _ctx;
        public AlunoRepository(AcademiaContext ctx) => _ctx = ctx;

        public async Task<Aluno> AddAsync(Aluno aluno)
        {
            _ctx.Alunos.Add(aluno);
            await _ctx.SaveChangesAsync();
            return aluno;
        }

        public Task<List<Aluno>> GetAllAsync() =>
            _ctx.Alunos
                .Include(a => a.Agendamentos)
                .ToListAsync();

        public Task<Aluno?> GetByIdAsync(Guid id) =>
            _ctx.Alunos
                .Include(a => a.Agendamentos)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task UpdateAsync(Aluno entity)
        {
            _ctx.Alunos.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _ctx.Alunos.FindAsync(id);
            if (e == null) return;
            _ctx.Alunos.Remove(e);
            await _ctx.SaveChangesAsync();
        }
    }
}
