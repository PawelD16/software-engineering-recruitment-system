﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
   //[Table("podania_na_I_stopien")]
    public class PodanieNaStudiaIStopniaModel : PodanieKandydataModel
    {
        /*
        [Key]
        [ForeignKey("PodanieKandydata")]
        public long FkIdPodanieKandydata { get; set; }
        public PodanieKandydataViewModel PodanieKandydata { get; set; }
        */
    }
}
