﻿using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("PodanieNaIIStopien")]
        public long FkIdPodanieNaIIStopien { get; set; }
        public PodanieNaIIStopienViewModel PodanieNaIIStopien { get; set; }

        [ForeignKey("KategoriaDorobku")]
        public long FkIdKategoriaDorobku { get; set; }
        public KategoriaDorobkuViewModel KategoriaDorobku { get; set; }
    }
}
