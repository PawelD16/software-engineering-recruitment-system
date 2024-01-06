using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.ViewModels.Users
{
    //[Table("pracownicy_dzialu_rekrutacji")]
    public class PracownikDzialuRekrutacjiViewModel : PracownikViewModel
    {
        /*
        [Key]
        [ForeignKey("Pracownik")]
        public string PkFkIdOsoba { get; set; }
        public PracownikViewModel Pracownik { get; set; }
        */
        [ForeignKey("Wydzial")]
        public long FkIdWydzial { get; set; }
        public WydzialViewModel Wydzial { get; set; }
    }
}
