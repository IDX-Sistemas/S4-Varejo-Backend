using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_for")]
    public class Fornecedor : BaseModel
    {
        
        public Fornecedor()
        {
            this.NotaFiscal = new HashSet<DocumentoEntrada>();
        }

        [Column("COD_FOR")]
        [Required]
        [MaxLength(4)]
        public string Codigo { get; set; }

        [Column("RAZ_SOC")]
        [MaxLength(30)]
        public string RazaoSocial { get; set; }

        [Column("END_FOR")]
        [MaxLength(40)]
        public string Endereco { get; set; }

        [Column("BAI_FOR")]
        [MaxLength(15)]
        public string Bairro { get; set; }

        [Column("CID_FOR")]
        [MaxLength(20)]
        public string Cidade { get; set; }

        [Column("CEP_FOR")]
        [MaxLength(9)]
        public string Cep { get; set; }

        [Column("EST_FOR")]
        [MaxLength(2)]
        public string Estado { get; set; }

        [Column("CGC_FOR")]
        [MaxLength(18)]
        public string Cnpj { get; set; }

        [Column("INS_FOR")]
        [MaxLength(15)]
        public string Inscricao { get; set; }

        [Column("TEL_FOR")]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [Column("FAX_FOR")]
        [MaxLength(15)]
        public string Fax { get; set; }

        [Column("NOM_REP")]
        [MaxLength(30)]
        public string NomeRep { get; set; }

        [Column("END_REP")]
        [MaxLength(40)]
        public string EnderecoRep { get; set; }

        [Column("BAI_REP")]
        [MaxLength(15)]
        public string BairroRep { get; set; }

        [Column("CID_REP")]
        [MaxLength(20)]
        public string CidadeRep { get; set; }

        [Column("EST_REP")]
        [MaxLength(2)]
        public string EstadoRep{ get; set; }

        [Column("CEP_REP")]
        [MaxLength(9)]
        public string CepRep { get; set; }

        [Column("TEL_REP")]
        [MaxLength(15)]
        public string TelefoneRep { get; set; }

        [Column("FAX_REP")]
        [MaxLength(15)]
        public string FaxRep { get; set; }

        [Column("CON_COM")]
        [MaxLength(30)]
        public string CondicaoComercial { get; set; }

        [Column("DES_COM")]
        [MaxLength(30)]
        public string DescontoComercial { get; set; }

        [Column("DAT_CAD", TypeName = "date")]
        public DateTime? DataCadastro { get; set; } = DateTime.Now.Date;

        [Column("OBS_001")]
        [MaxLength(30)]
        public string Observacao1 { get; set; }

        [Column("OBS_002")]
        [MaxLength(30)]
        public string Observacao2 { get; set; }


        public virtual ICollection<DocumentoEntrada> NotaFiscal { get; set; }
    }
}
