namespace NextFit.Models
{
    public class Agendamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AlunoId { get; set; }
        public Guid AulaId { get; set; }
        public Aula Aula { get; set; } = null!;
        public DateTime DataAgendamento { get; set; } = DateTime.UtcNow;
    }
}