using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels.Users
{
    [Table("kandydaci")]
    public class KandydatViewModel
    {
        [Key]
        [StringLength(10)]
        public string NumerKandydata { get; set; }

        [ForeignKey("Osoba")]
        public long FkIdOsoba { get; set; }

        public OsobaViewModel Osoba { get; set; }
    }
}
