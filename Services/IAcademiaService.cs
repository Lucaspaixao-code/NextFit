using NextFit.DTOs;
using NextFit.Models;
namespace NextFit.Services 
{ 
    public interface IAcademiaService 
    { 
        Task<Aluno> CadastrarAlunoAsync(string nome, TipoPlano plano); 
        Task<Aula> CadastrarAulaAsync(string tipo, DateTime dataHora, int capacidade);
        Task<AgendarAulaDto> AgendarAulaAsync(Guid alunoId, Guid aulaId);
        Task<RelatorioAlunoDto> RelatorioAlunoAsync(Guid alunoId, int mes, int ano);
    } 
}