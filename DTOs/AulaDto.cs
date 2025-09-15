namespace NextFit.DTOs 
{ 
    public record AulaCreateDto(string Tipo, DateTime DataHora, int CapacidadeMaxima); 
    public record AulaDto(Guid Id, string Tipo, DateTime DataHora, int CapacidadeMaxima); 
}