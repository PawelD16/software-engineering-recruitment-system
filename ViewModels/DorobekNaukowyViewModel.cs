using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("dorobei_naukowe")]
    public class DorobekNaukowyViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime DataUzyskaniaDorobku { get; set; }

        [ForeignKey("PodanieNaIiStopien")]
        public long FkIdPodanieNaIiStopien { get; set; }
        public PodanieNaIIStopienViewModel PodanieNaIiStopien { get; set; }

        [ForeignKey("KategoriaDorobku")]
        public long FkIdKategoriaDorobku { get; set; }
        public KategoriaDorobkuViewModel KategoriaDorobku { get; set; }
    }
}
