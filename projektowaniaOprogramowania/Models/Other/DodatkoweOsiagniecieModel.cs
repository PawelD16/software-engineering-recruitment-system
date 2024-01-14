using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("dodatkowe_osiagniecia")]
    public class DodatkoweOsiagniecieModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime DataZdobycia { get; set; }

        [Required]
        [StringLength(1000000)] //In Postgres you can set TEXT, but VisualParadigm doesn't support unlimited length
        public string Opis { get; set; }

        [ForeignKey("PodanieKandydata")]
        public long FkIdPodanieKandydata { get; set; }
        public PodanieKandydataModel PodanieKandydata { get; set; }

        [ForeignKey("PrzelicznikOsiagniec")]
        public long FkIdPrzelicznikOsiagniec { get; set; }
        public PrzelicznikOsiagniecModel PrzelicznikOsiagniec { get; set; }
    }
}
