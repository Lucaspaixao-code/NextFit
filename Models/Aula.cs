using System.ComponentModel.DataAnnotations;
namespace NextFit.Models 
{ 
    public class Aula 
    { 
        public Guid Id { get; set; } = Guid.NewGuid(); 
        [Required] public string Tipo { get; set; } = string.Empty; 
        public DateTime DataHora { get; set; } 
        public int CapacidadeMaxima { get; set; } 
        public List<Agendamento> Agendamentos { get; set; } = new(); 
    } 
}