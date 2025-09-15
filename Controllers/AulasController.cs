using NextFit.Models;
using NextFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using NextFit.DTOs;
using NextFit.Services;

namespace NextFit.API.Controllers
{
    public class AulasController : BaseController<Aula, IAulaRepository>
    {
        private readonly IAcademiaService _academiaService;
        public AulasController(IAulaRepository repository,
                                IAcademiaService academiaService)
            : base(repository)
        {
            _academiaService = academiaService;
        }

        protected override Guid GetEntityId(Aula entity) => entity.Id;

        protected override void UpdateEntity(Aula existing, Aula updated)
        {
            existing.Tipo = updated.Tipo;
            existing.DataHora = updated.DataHora;
            existing.CapacidadeMaxima = updated.CapacidadeMaxima;
        }

        [HttpPost]
        public ActionResult<Aula> Create([FromBody] AulaCreateDto aula)
        {
            var created = _academiaService.CadastrarAulaAsync(aula.Tipo, aula.DataHora, aula.CapacidadeMaxima);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(created.Result) }, created);
        }
    }
}
