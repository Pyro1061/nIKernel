using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Produto
{
    public class ProdutoModel
    {
        public int prd_id { get; set; }
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Código é obrigatório")]
        [StringLength(50, ErrorMessage = "Código deve ter no máximo 50 caracteres")]
        public string prd_cod { get; set; } = string.Empty;

        [Display(Name = "GTIN/EAN")]
        [StringLength(14, ErrorMessage = "GTIN/EAN deve ter no máximo 14 dígitos")]
        [RegularExpression(@"^\d*$", ErrorMessage = "GTIN/EAN deve conter apenas dígitos")]
        public string prd_gtin_ean { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(250, ErrorMessage = "Descrição deve ter no máximo 250 caracteres")]
        public string prd_descricao { get; set; } = string.Empty;

        [Display(Name = "Unidade de Compra")]
        [Required(ErrorMessage = "Unidade de compra é obrigatória")]
        [StringLength(20, ErrorMessage = "Unidade de compra inválida")]
        public string prd_un_compra { get; set; } = string.Empty;

        [Display(Name = "Unidade de Venda")]
        [Required(ErrorMessage = "Unidade de venda é obrigatória")]
        [StringLength(20, ErrorMessage = "Unidade de venda inválida")]
        public string prd_un_venda { get; set; } = string.Empty;

        [Display(Name = "Preço de Compra")]
        [Range(0, 9999999.99, ErrorMessage = "Preço de compra deve ser >= 0")]
        [DataType(DataType.Currency)]
        public decimal prd_preco_compra { get; set; }

        [Display(Name = "Margem de Lucro (%)")]
        [Range(0, 10000, ErrorMessage = "Margem deve ser um percentual válido")]
        public decimal prd_margem_venda { get; set; }

        [Display(Name = "Preço de Venda")]
        [Range(0, 9999999.99, ErrorMessage = "Preço de venda deve ser >= 0")]
        [DataType(DataType.Currency)]
        public decimal prd_preco_venda { get; set; }

        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "Ativo é obrigatório")]
        [RegularExpression("^[SN]$", ErrorMessage = "Ativo deve ser 'S' ou 'N'")]
        public string prd_ativo { get; set; } = "S"; // Alterado de bool para string

        [Display(Name = "Data de Criação")]
        public DateTime prd_data_criacao { get; set; }
    }
}