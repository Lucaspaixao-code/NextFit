using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NextFit.DTOs;
using NextFit.Models;
using NextFit.Repositories;
using NextFit.Services;

namespace NextFit.API.Controllers
{
    public class AgendamentosController : BaseController<Agendamento, IAgendamentoRepository>
    {
        private readonly IAcademiaService _academiaService;
        public AgendamentosController(IAgendamentoRepository repository,
                                IAcademiaService academiaService)
            : base(repository)
        {
            _academiaService = academiaService;
        }

        protected override Guid GetEntityId(Agendamento entity) => entity.Id;

        protected override void UpdateEntity(Agendamento existing, Agendamento updated)
        {
            existing.AlunoId = updated.AlunoId;
            existing.AulaId = updated.AulaId;
            existing.DataAgendamento = updated.DataAgendamento;
            
        }

        [HttpPost]
        public ActionResult<AgendarAulaDto> create([FromBody] AgendamentoCreateDto agendamento)
        {
            return _academiaService.AgendarAulaAsync(agendamento.AlunoId, agendamento.AulaId).Result;
        }

        [HttpPost("Relatorio")]
        public ActionResult<RelatorioAlunoDto> Relatorio([FromBody] RelatorioDto relatorio)
        {
            return _academiaService.RelatorioAlunoAsync(relatorio.AlunoId, relatorio.mes, relatorio.ano).Result;
        }
    }
}