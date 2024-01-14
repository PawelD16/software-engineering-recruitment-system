using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_przedmiotu")]
    public class PrzelicznikPrzedmiotuModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Range(0, 1)]
        public float Wspolczynnik { get; set; }

        [ForeignKey("Przedmiot")]
        public long FkIdPrzedmiot { get; set; }
        public PrzedmiotModel Przedmiot { get; set; }

        [ForeignKey("PrzelicznikKierunkowy")]
        public long FkIdPrzelicznikKierunkowy { get; set; }
        public PrzelicznikKierunkowyModel PrzelicznikKierunkowy { get; set; }
    }
}
