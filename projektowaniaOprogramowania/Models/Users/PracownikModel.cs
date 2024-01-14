using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace projektowaniaOprogramowania.ViewModels.Users
{
    //[Table("pracownicy")]
    public class PracownikModel : OsobaModel
    {
        //[Key]
        [StringLength(10)]
        public string NumerPracownika { get; set; }

        [Required]
        public DateTime DataZatrudnienia { get; set; }

        [Required]
        [StringLength(20)] // Maybe use another table or a simple enum?
        public string TypPracownika { get; set; }

        /*
        [ForeignKey("Osoba")]
        public long FkIdOsoba { get; set; }

        public OsobaViewModel Osoba { get; set; }
        */
    }
}
