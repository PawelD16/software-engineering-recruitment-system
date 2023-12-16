using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("podania_na_I_stopien")]
    public class PodanieNaIStopienViewModel
    {
        [Key]
        [ForeignKey("PodanieKandydata")]
        public long FkIdPodanieKandydata { get; set; }
        public PodanieKandydataViewModel PodanieKandydata { get; set; }

        [ForeignKey("Matura")]
        public long FkIdMatura { get; set; }
        public MaturaViewModel Matura { get; set; }
    }
}
