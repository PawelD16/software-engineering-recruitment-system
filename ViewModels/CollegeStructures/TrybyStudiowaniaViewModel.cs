using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels.CollegeStructures
{
    [Table("tryby_studiowania")]
    public class TrybStudiowaniaViewModel
    {
        [Key]
        [StringLength(20)]
        public string EnumLiteral { get; set; }
    }
}
