using System.ComponentModel.DataAnnotations;
namespace NextFit.Models 
{ 
    public class Aluno 
    { 
        public Guid Id { get; set; } = Guid.NewGuid(); 
        [Required] public string Nome { get; set; } = string.Empty; 
        public TipoPlano Plano { get; set; } 
        public List<Agendamento> Agendamentos { get; set; } = new(); 
    } 
}