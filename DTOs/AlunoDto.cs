using NextFit.Models;

namespace NextFit.DTOs 
{ 
    public record AlunoCreateDto(string Nome, TipoPlano Plano);
    public record AlunoDto(Guid Id, string Nome, string Plano); 
}