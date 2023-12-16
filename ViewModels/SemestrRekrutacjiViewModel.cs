using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("semestry_rekrutacji")]
    public class SemestrRekrutacjiViewModel
    {
        [Key]
        [StringLength(20)]
        public string EnumLiteral { get; set; }
    }
}
