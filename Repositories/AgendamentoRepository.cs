using Microsoft.EntityFrameworkCore;
using NextFit.Data;
using NextFit.Models;

namespace NextFit.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AcademiaContext _ctx;
        public AgendamentoRepository(AcademiaContext ctx) => _ctx = ctx;

        public async Task<Agendamento> AddAsync(Agendamento agendamento)
        {
            _ctx.Agendamentos.Add(agendamento);
            await _ctx.SaveChangesAsync();
            return agendamento;
        }

        public Task<List<Agendamento>> GetByAlunoAsync(Guid alunoId) =>
            _ctx.Agendamentos
                .Include(a => a.Aula)
                .Where(a => a.AlunoId == alunoId)
                .ToListAsync();

        public Task<int> CountByAulaAsync(Guid aulaId) =>
            _ctx.Agendamentos.CountAsync(a => a.AulaId == aulaId);

        public Task<List<Agendamento>> GetAllAsync() =>
            _ctx.Agendamentos
                .Include(a => a.Aula)
                .ToListAsync();

        public Task<Agendamento?> GetByIdAsync(Guid id) =>
            _ctx.Agendamentos
                .Include(a => a.Aula)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task UpdateAsync(Agendamento entity)
        {
            _ctx.Agendamentos.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _ctx.Agendamentos.FindAsync(id);
            if (e == null) return;
            _ctx.Agendamentos.Remove(e);
            await _ctx.SaveChangesAsync();
        }

    }
}
