using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.BLL.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Nome { get; set; }

        [Required]
        [MinLength(1)]
        public string Descricao { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Nome}");
            return sb.ToString();
        }

    }
}
