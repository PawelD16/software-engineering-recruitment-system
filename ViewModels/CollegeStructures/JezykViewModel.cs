using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels.CollegeStructures
{
    [Table("jezyki")]
    public class JezykViewModel
    {
        [Key]
        [StringLength(20)]
        public string EnumLiteral { get; set; }
    }
}
