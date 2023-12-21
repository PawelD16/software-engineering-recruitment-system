using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("pracownicy_dzialu_rekrutacji_na_podania_kandydata")]
    public class PracownikDzialuRekrutacjiNaPodanieKandydataViewModel
    {
        [ForeignKey("PodanieKandydata")]
        public long PkFkIdPodanieKandydata { get; set; }
        public PodanieKandydataViewModel PodanieKandydata { get; set; }

        [Key]
        [ForeignKey("PracownikDzialuRekrutacji")]
        public string PkFkIdPracownikDzialuRekrutacji { get; set; }
        public PracownikDzialuRekrutacjiViewModel PracownikDzialuRekrutacji { get; set; }
    }
}
