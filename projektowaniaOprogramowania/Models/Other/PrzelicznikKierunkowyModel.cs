using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_kierunkowe")]
    public class PrzelicznikKierunkowyModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Range(0, 535)]
        public int MaksymalnaWartoscPrzelicznika { get; set; }

        // [ForeignKey("Kierunek")]
        // public long FkIdKierunek { get; set; }
        // public KierunekViewModel Kierunek { get; set; }
        // [ForeignKey("PrzelicznikPrzedmiotu")]
        // public long FkIdPrzelicznikPrzedmiotu { get; set; }

        public ICollection<PrzelicznikPrzedmiotuModel> PrzelicznikPrzemiotu { get; set; } =
            new List<PrzelicznikPrzedmiotuModel>();

        // [ForeignKey("PrzelicznikDorobku")]
        // public long FkIdPrzelicznikDorobku { get; set; }

        public ICollection<PrzelicznikDorobkuModel> PrzelicznikDorobku { get; set; } =
            new List<PrzelicznikDorobkuModel>();
    }
}
