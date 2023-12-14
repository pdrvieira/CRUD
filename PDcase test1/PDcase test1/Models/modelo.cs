using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDcase_test1.Models
{
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é requerido.")]
        public string Nome { get; set; }
    }

    public class PlanoDeSaude
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é requerido.")]
        public string Nome { get; set; }
    }

    public class FichaPaciente
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome Paciente")]
        [Required(ErrorMessage = "O código é requerido.")]
        public string NomePaciente { get; set; }

        [Display(Name = "Número do Plano")]
        [Required(ErrorMessage = "O código é requerido.")]
        public string NumeroCarteiraPlano { get; set; }

        [Display(Name = "Especialidade")]
        public int EspecialidadeId { get; set; }
        [ForeignKey("EspecialidadeId")]
        public Especialidade Especialidade { get; set; }

        [Display(Name = "Plano de Saúde")]
        public int PlanoDeSaudeId { get; set; }
        [ForeignKey("PlanoDeSaudeId")]
        public PlanoDeSaude PlanoDeSaude { get; set; }
    }

}
