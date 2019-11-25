using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_fun")]
    public class Funcionario : BaseModel
    {
        [Column("COD_CLI")]
        [Required(ErrorMessage="Informe o código!")]
        [MaxLength(5)]
        public string Codigo { get; set; }

        [Column("NOM_CLI")]
        [Required(ErrorMessage = "Informe o nome!")]
        [MaxLength(40)]
        public string Nome { get; set; }

        [Column("COD_LOC")]
        [Required(ErrorMessage = "Informe a loja!")]
        [MaxLength(2)]
        public string Loja { get; set; }

        [Column("REG_CLI")]
        [MaxLength(13)]
        public string Rg { get; set; }

        [Column("CIC_CLI")]
        [MaxLength(15)]
        public string Cpf { get; set; }

        [Column("DAT_NAS", TypeName = "date")]
        public DateTime? DataNascimento { get; set; }

        [Column("END_CLI")]
        [MaxLength(40)]
        public string Endereco { get; set; }

        [Column("BAI_CLI")]
        [MaxLength(18)]
        public string Bairro { get; set; }

        [Column("CID_CLI")]
        [MaxLength(20)]
        public string Cidade { get; set; }

        [Column("EST_CLI")]
        [MaxLength(2)]
        public string Estado { get; set; }

        [Column("CEP_CLI")]
        [MaxLength(8)]
        public string Cep { get; set; }

        [Column("TEL_CLI")]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [Column("DAT_CAD", TypeName = "date")]
        public DateTime? DataAdmissao { get; set; }

        [Column("CAR_CLI")]
        [MaxLength(15)]
        public string Cargo { get; set; }

        [Column("CRP_CLI")]
        [MaxLength(15)]
        public string Ctps { get; set; }

        [Column("VAL_SAL")]
        public double? Salario { get; set; } = 0;
    }
}
