using NextFit.Models;
namespace NextFit.Repositories
{ 
    public interface IAgendamentoRepository : IRepository<Agendamento>
    { 
        Task<List<Agendamento>> GetByAlunoAsync(Guid alunoId); 
        Task<int> CountByAulaAsync(Guid aulaId); 
    } 
}