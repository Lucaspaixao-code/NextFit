using Microsoft.EntityFrameworkCore;
using NextFit.Models;
namespace NextFit.Data
{ 
    public class AcademiaContext : DbContext 
    { 
        public AcademiaContext(DbContextOptions<AcademiaContext> options) : base(options) { } 
        public DbSet<Aluno> Alunos { get; set; } = null!; 
        public DbSet<Aula> Aulas { get; set; } = null!; 
        public DbSet<Agendamento> Agendamentos { get; set; } = null!; 
    } 
}