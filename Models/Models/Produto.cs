using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_ite")]
    public class Produto : BaseModel
    {
        [Column("COD_ITE")]
        [MaxLength(14)]
        public string Codigo { get; set; }

        [Column("DES_ITE")]
        [MaxLength(30)]
        public string Descricao { get; set; }

        [Column("UNI_ITE")]
        [MaxLength(2)]
        public string Unidade { get; set; }

        [Column("DES_CD1")]
        [MaxLength(18)]
        public string DescricaoEtiqueta1 { get; set; }
        
        [Column("COD_TAB")]
        [MaxLength(4)]
        public string Tabela { get; set; }

        [Column("DES_CD2")]
        [MaxLength(18)]
        public string DescricaoEtiqueta2 { get; set; }

        [Column("VAL_001")]
        public double? ValorVista { get; set; } = 0.00;

        [Column("VAL_002")]
        public double? ValorPrazo { get; set; } = 0.00;

        [Column("VAL_CUS")]
        public double? ValorCusto { get; set; } = 0.00;

        [Column("STA_ITE")]
        [MaxLength(1)]
        public string Status { get; set; }

        [Column("VAL_PRO")]
        public double? ValorPromocao { get; set; } = 0.00;

        [Column("ULT_FOR")]
        [MaxLength(4)]
        public string Fornecedor { get; set; }

        [Column("ULT_COM", TypeName = "date")]
        public DateTime? UltimaCompra { get; set; }

        [Column("CON_MED")]
        public int? ConsumoMedio { get; set; } = 0;

        [Column("ITE_PRI")]
        [MaxLength(14)]
        public string ItemPrincipal { get; set; }

        [Column("CUS_MED")]
        public double? CustoMedio { get; set; } = 0.00;

        [Column("VEN_MES")]
        public int? VendaMes { get; set; } = 0;

        [Column("COM_MES")]
        public int? CompraMes { get; set; } = 0;

        [Column("TRA_MES")]
        public int? TransferenciaMes { get; set; } = 0;

        [Column("COD_SEC")]
        [MaxLength(2)]
        public string Secao { get; set; }
    }
}
