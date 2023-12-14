using Microsoft.EntityFrameworkCore;
using PDcase_test1.Models;


namespace PDcase_test1
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<PlanoDeSaude> PlanosDeSaude { get; set; }
        public DbSet<FichaPaciente> FichaPacientes { get; set; }
   
    }
}
