using System.ComponentModel.DataAnnotations;

namespace DLL.BLL.Models
{
    public class Cliente
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Digite o primeiro nome")]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "Digite o último nome")]
        [Display(Name = "Último Nome")]
        public string UltimoNome { get; set; }

        [Required(ErrorMessage = "Digite o Endereço")]
        [Display(Name = "Endereço")]
        [StringLength(50)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Digite a cidade")]
        [Display(Name = "Cidade")]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Código Postal")]
        public string CEP { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
