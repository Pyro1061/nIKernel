using System.ComponentModel.DataAnnotations;

namespace Web.Models.Categoria
{
    public class CtgCategoriaModel
    {
        public int ctg_id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(30, ErrorMessage = "Descrição deve ter no máximo 30 caracteres")]
        public string ctg_dcc { get; set; } = string.Empty;

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status é obrigatório")]
        [RegularExpression("^[SN]$", ErrorMessage = "Status deve ser 'S' ou 'N'")]
        public string ctg_sta { get; set; } = "S";
    }
}
