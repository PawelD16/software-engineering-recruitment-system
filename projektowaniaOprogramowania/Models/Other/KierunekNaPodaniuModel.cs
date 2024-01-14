using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("kierunki_na_podaniach")]
    public class KierunekNaPodaniuModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int Priorytet { get; set; }

        [ForeignKey("PodanieKandydata")]
        public long FkIdPodanieKandydata { get; set; }
        public PodanieKandydataModel PodanieKandydata { get; set; }

        [ForeignKey("Kierunek")]
        public long FkIdKierunek { get; set; }
        public KierunekModel Kierunek { get; set; }

	}
}
