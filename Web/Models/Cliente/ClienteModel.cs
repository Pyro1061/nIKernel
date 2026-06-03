using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace nIKernel.Models.Cliente
{
    public class ClienteModel
    {
        public int CL_id {get; set;}

        [Required(ErrorMessage = "A inserção do CPF ou CNPJ é obrigatória.")]
        public string CL_cpf_cnpj {get; set;} = String.Empty;

        public string CL_rg_ie {get; set;} = String.Empty;

        public string CL_nome {get; set;} = String.Empty;

        public string CL_apelido {get; set;} = String.Empty;

        [Required(ErrorMessage = "A inserção do Status atual do cliente é obrigatória.")]
        [StringLength(1)]
        public string CL_status { get; set; } = "A";

        public DateTime CL_data_inclusao {get; set;}

        // ENDEREÇO DE CLIENTE 
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9)]
        public string END_CEP { get; set; } = String.Empty;

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(150)]
        public string END_LOG { get; set; } = String.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(10)]
        public string END_NUM { get; set; } = String.Empty;

        [StringLength(50)]
        public string? END_CPL { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100)]
        public string END_BAI { get; set; } = String.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100)]
        public string END_CID { get; set; } = String.Empty;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2)]
        public string END_EST { get; set; } = String.Empty;
    } 
}