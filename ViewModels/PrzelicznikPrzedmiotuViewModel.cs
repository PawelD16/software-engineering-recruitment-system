using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_przedmiotu")]
    public class PrzelicznikPrzedmiotuViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Range(0, 1)]
        public float Wspolczynnik { get; set; }

        [ForeignKey("Przedmiot")]
        public long FkIdPrzedmiot { get; set; }
        public PrzedmiotViewModel Przedmiot { get; set; }

        [ForeignKey("PrzelicznikKierunkowy")]
        public long FkIdPrzelicznikKierunkowy { get; set; }
        public PrzelicznikKierunkowyViewModel PrzelicznikKierunkowy { get; set; }
    }
}
