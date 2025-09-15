
using NextFit.DTOs;
using NextFit.Models;
using NextFit.Repositories;

namespace NextFit.Services
{ 
    public class AcademiaService : IAcademiaService
    {
        private readonly IAlunoRepository _alunoRepo;
        private readonly IAulaRepository _aulaRepo;
        private readonly IAgendamentoRepository _agRepo;

        private readonly Dictionary<TipoPlano, int> _limitePlano = new()
        {
            { TipoPlano.Mensal, 12 },
            { TipoPlano.Trimestral, 20 },
            { TipoPlano.Anual, 30 }
        };

        public AcademiaService(IAlunoRepository alunoRepo, IAulaRepository aulaRepo, IAgendamentoRepository agRepo)
        {
            _alunoRepo = alunoRepo;
            _aulaRepo = aulaRepo;
            _agRepo = agRepo;
        }

        public async Task<Aluno> CadastrarAlunoAsync(string nome, TipoPlano plano)
        {
            var aluno = new Aluno { Nome = nome, Plano = plano };
            return await _alunoRepo.AddAsync(aluno);
        }

        public async Task<Aula> CadastrarAulaAsync(string tipo, DateTime dataHora, int capacidade)
        {
            var aula = new Aula { Tipo = tipo, DataHora = dataHora, CapacidadeMaxima = capacidade };
            return await _aulaRepo.AddAsync(aula);
        }

        public async Task<AgendarAulaDto> AgendarAulaAsync(Guid alunoId, Guid aulaId)
        {
            var aluno = await _alunoRepo.GetByIdAsync(alunoId);
            if (aluno == null) return new AgendarAulaDto(false, "Aluno não encontrado.");

            var aula = await _aulaRepo.GetByIdAsync(aulaId);
            if (aula == null) return new AgendarAulaDto(false, "Aula não encontrada.");

            var inscritos = await _agRepo.CountByAulaAsync(aulaId);
            if (inscritos >= aula.CapacidadeMaxima)
                return new AgendarAulaDto(false, "Aula cheia.");

            var agsAluno = await _agRepo.GetByAlunoAsync(alunoId);
            if (agsAluno.Any(a => a.AulaId == aulaId))
                return new AgendarAulaDto(false, "Aluno já agendado nesta aula.");

            var mes = aula.DataHora.Month;
            var ano = aula.DataHora.Year;
            var totalNoMes = agsAluno.Count(a => a.Aula.DataHora.Month == mes && a.Aula.DataHora.Year == ano);

            if (!_limitePlano.TryGetValue(aluno.Plano, out var limite))
                limite = 0;

            if (totalNoMes >= limite)
                return new AgendarAulaDto(false, $"Limite do plano atingido ({limite} aulas/mês).");

            var agendamento = new Agendamento
            {
                AlunoId = alunoId,
                AulaId = aulaId,
                DataAgendamento = DateTime.UtcNow
            };

            await _agRepo.AddAsync(agendamento);
            return new AgendarAulaDto(true, "Agendamento confirmado.");
        }

        public async Task<RelatorioAlunoDto> RelatorioAlunoAsync(Guid alunoId, int mes, int ano)
        {
            var aluno = await _alunoRepo.GetByIdAsync(alunoId);
            if (aluno == null) return new RelatorioAlunoDto(0, Enumerable.Empty<string>());

            var ags = await _agRepo.GetByAlunoAsync(alunoId);
            var filtrados = ags.Where(a => a.Aula.DataHora.Month == mes && a.Aula.DataHora.Year == ano).ToList();
            var total = filtrados.Count;

            var mais = filtrados
                .GroupBy(a => a.Aula.Tipo)
                .OrderByDescending(g => g.Count())
                .Select(g => $"{g.Key} ({g.Count()}x)")
                .ToList();

            return new RelatorioAlunoDto(total, mais);
        }
    }
}
