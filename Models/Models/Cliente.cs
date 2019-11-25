using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IdxSistemas.Models
{
    [Table("cad_cli")]
    public class Cliente : BaseModel
    {

         public Cliente()
        {
            this.PlanoCartao = new HashSet<Operadora>();
        }

        [Column("COD_CLI")]
        [Required]
        [MaxLength(5)]
        public string Codigo { get; set; }

        [Column("COD_LOC")]
        [Required]
        [MaxLength(2)]
        public string Loja { get; set; }

        [Column("NOM_CLI")]
        [MaxLength(40)]
        public string Nome { get; set; }

        [Column("END_CLI")]
        [MaxLength(40)]
        public string Endereco { get; set; }

        [Column("BAI_CLI")]
        [MaxLength(18)]
        public string Bairro { get; set; }

        [Column("CID_CLI")]
        [MaxLength(20)]
        public string Cidade { get; set; }

        [Column("CEP_CLI")]
        [MaxLength(8)]
        public string Cep { get; set; }

        [Column("EST_CLI")]
        [MaxLength(2)]
        public string Estado { get; set; }

        [Column("REG_CLI")]
        [MaxLength(13)]
        public string Rg { get; set; }

        [Column("CIC_CLI")]
        [MaxLength(15)]
        public string Cpf { get; set; }

        [Column("TEL_CLI")]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [Column("DAT_NAS")]
        public DateTime? DataNascimento { get; set; }

        [Column("EST_CIV")]
        [MaxLength(15)]
        public string EstadoCivil { get; set; }

        [Column("CNJ_CLI")]
        [MaxLength(40)]
        public string Conjuge { get; set; }

        [Column("QUT_FIL")]
        public int? QuantidadeFilhos { get; set; } = 0;

        [Column("DAT_CAD")]
        public DateTime? DataCadastro { get; set; } = DateTime.Now.Date;

        [Column("DAT_ULT")]
        public DateTime? DataUltimaCompra { get; set; }

        [Column("VAL_ULT")]
        public double? ValorUltimaCompra { get; set; } = 0;

        [Column("VAL_CRE")]
        public double? ValorCredito { get; set; } = 0;

        [Column("CON_CEI")]
        [MaxLength(2)]
        public string Conceito { get; set; }

        [Column("OBS_1")]
        [MaxLength(25)]
        public string Observacao1 { get; set; }

        [Column("OBS_2")]
        [MaxLength(25)]
        public string Observacao2 { get; set; }

        [Column("CLI_SPC")]
        [MaxLength(1)]
        public string Spc { get; set; }

        [Column("TIP_CLI")]
        [MaxLength(1)]
        public string TipoCliente { get; set; }

        [Column("EMA_CLI")]
        [MaxLength(35)]
        public string Email { get; set; }

        [Column("EMP_CLI")]
        [MaxLength(40)]
        public string Empresa { get; set; }

        [Column("END_EMP")]
        [MaxLength(40)]
        public string EnderecoEmpresa { get; set; }

        [Column("CID_EMP")]
        [MaxLength(20)]
        public string CidadeEmpresa { get; set; }

        [Column("TEL_EMP")]
        [MaxLength(15)]
        public string TelefoneEmpresa { get; set; }

        [Column("CAR_EMP")]
        [MaxLength(15)]
        public string CargoEmpresa { get; set; }

        [Column("SAL_EMP")]
        public double? SalarioEmpresa { get; set; } = 0;

        [Column("CAR_001")]
        [MaxLength(1)]
        public string Carta1 { get; set; }

        [Column("CAR_002")]
        [MaxLength(2)]
        public string Carta2 { get; set; }

        [Column("DAT_ALT")]
        public DateTime? DataAlteracao { get; set; }


        public virtual ICollection<Operadora> PlanoCartao { get; set; }
    }
}
