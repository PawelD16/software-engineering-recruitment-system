using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("podanie_kandydata")]
    public class PodanieKandydataViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime DataZlozeniaPodania { get; set; }

        [Required]
        public bool CzyAktywny { get; set; }

        [ForeignKey("Rekrutacja")]
        public long FkIdRekrutacja { get; set; }
        public RekrutacjaViewModel Rekrutacja { get; set; }

        [ForeignKey("Kandydat")]
        public long FkIdKandydat { get; set; }
        public KandydatViewModel Kandydat { get; set; }
    }
}
