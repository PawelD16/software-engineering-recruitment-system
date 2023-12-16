using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels.CollegeStructures
{
    [Table("wydzialy")]
    public class WydzialViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        public string NumerWydzialu { get; set; }

        [Required]
        [StringLength(255)]
        public string NazwaWydzialu { get; set; }

        [ForeignKey("Miasto")]
        public long MiastoId { get; set; }
        public MiastoViewModel Miasto { get; set; }
    }
}
