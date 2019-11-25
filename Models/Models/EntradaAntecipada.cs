using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("ENTRADA_ANTECIPADA")]
    public class EntradaAntecipada : BaseModel
    {
        [Required]
        [MaxLength(8)]
        public string NotaFiscal { get; set; }

        [Required]
        [MaxLength(4)]
        public string Fornecedor { get; set; }

        [ForeignKey("Fornecedor")]
        public virtual Fornecedor Fornecedores { get; set; }

        [MaxLength(1)]
        public string ClassificacaoFiscal { get; set; }

        [Required]
        [MaxLength(14)]
        public string Produto { get; set; }

        [ForeignKey("Produto")]
        public virtual Produto Produtos { get; set; }

        [Required]
        [MaxLength(2)]
        public string Secao { get; set; }

        [MaxLength(14)]
        public string ProdutoPrincipal { get; set; }

        [MaxLength(30)]
        [Required]
        public string DescricaoProduto { get; set; }


        [MaxLength(20)]
        [Required]
        public string DescricaoEtiqueta1 { get; set; }


        [MaxLength(20)]
        public string DescricaoEtiqueta2 { get; set; }


        public double? PrecoVista { get; set; } = 0.00;

        public double? PrecoPrazo { get; set; } = 0.00;

        [Required]
        [MaxLength(2)]
        public string Loja { get; set; }

        public double? PrecoCusto { get; set; } = 0.00;
        public int? Quantidade { get; set; } = 0;

        [Column(TypeName = "date")]
        public DateTime? DataEntrada { get; set; }

    }
}

