using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    //[Table("podania_na_II_stopien")]
    public class PodanieNaIIStopienViewModel : PodanieKandydataViewModel
    {
        /*
        [Key]
        [ForeignKey("PodanieKandydata")]
        public long PkFkIdPodanieKandydata { get; set; }
        public PodanieKandydataViewModel PodanieKandydata { get; set; }
        */

        [Required]
        public float SredniaZTokuStudiow { get; set; }

        [Required]
        public float OcenaZPracyDyplomowej { get; set; }
    }
}
