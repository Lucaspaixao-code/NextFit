namespace NextFit.DTOs
{ 
    public record AgendamentoCreateDto(Guid AlunoId, Guid AulaId);

    public record RelatorioDto(Guid AlunoId, int ano, int mes);
    public record AgendarAulaDto (bool Success, string Message);
    public record RelatorioAlunoDto(int Total, IEnumerable<string> MaisFrequentes);
}