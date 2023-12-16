using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("statusy_rekrutacji")]
    public class StatusRekrutacjiViewModel
    {
        [Key]
        [StringLength(20)]
        public string EnumLiteral { get; set; }
    }
}
