using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using projektowaniaOprogramowania.ViewModels.Users;
using projektowaniaOprogramowania.ViewModels;

namespace projektowaniaOprogramowania.Models.Other
{
    [Table("pracownicy_dzialu_rekrutacji_na_podania_kandydata")]
    public class PracownikDzialuRekrutacjiNaPodanieKandydataModel
    {
        [ForeignKey("PodanieKandydata")]
        public long PkFkIdPodanieKandydata { get; set; }
        public PodanieKandydataModel PodanieKandydata { get; set; }

        [Key]
        [ForeignKey("PracownikDzialuRekrutacji")]
        public long PkFkIdPracownikDzialuRekrutacji { get; set; }
        public PracownikDzialuRekrutacjiModel PracownikDzialuRekrutacji { get; set; }
    }
}
