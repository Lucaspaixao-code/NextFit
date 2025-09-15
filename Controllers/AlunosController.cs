using NextFit.Models;
using NextFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using NextFit.DTOs;
using NextFit.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NextFit.API.Controllers
{
    public class AlunosController : BaseController<Aluno, IAlunoRepository>
    {
        private readonly IAcademiaService _academiaService;
        public AlunosController(IAlunoRepository repository,
                                IAcademiaService academiaService) 
            : base(repository)
        {
            _academiaService = academiaService;
        }

        protected override Guid GetEntityId(Aluno entity) => entity.Id;

        protected override void UpdateEntity(Aluno existing, Aluno updated)
        {
            existing.Nome = updated.Nome;
            existing.Plano = updated.Plano;
        }

        [HttpPost]
        public ActionResult<Aluno> Create([FromBody] AlunoCreateDto aluno)
        {
            var created = _academiaService.CadastrarAlunoAsync(aluno.Nome, aluno.Plano);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(created.Result) }, created);
        }
    }
}
