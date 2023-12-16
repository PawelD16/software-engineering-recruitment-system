using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels.CollegeStructures
{
    [Table("stopnie_studiow")]
    public class StopienStudiowViewModel
    {
        [Key]
        [StringLength(20)]
        public string EnumLiteral { get; set; }
    }
}
