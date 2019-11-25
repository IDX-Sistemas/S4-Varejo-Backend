using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace IdxSistemas.Models
{
    public class BaseModel : IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("sql_rowid")]
        public long RowId { get; set; }

        [Column("sql_deleted")]
        [DefaultValue("F")]
        public string RowDeleted { get; set; } = "F";
    }
}