using Microsoft.AspNetCore.Mvc;
using NextFit.Repositories;

namespace NextFit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository _repository;

        public BaseController(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, [FromBody] TEntity entity)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            UpdateEntity(existing, entity);
            await _repository.UpdateAsync(existing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // Métodos auxiliares abstratos
        protected abstract Guid GetEntityId(TEntity entity);
        protected abstract void UpdateEntity(TEntity existing, TEntity updated);
    }
}
