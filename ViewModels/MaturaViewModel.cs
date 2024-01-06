using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("matury")]
    public class MaturaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime DataPrzystapieniaDoMatury { get; set; }

		[ForeignKey("PodanieNaIStopien")]
		public long FkIdPodanieNaIStopien { get; set; }
		public PodanieNaIStopienViewModel PodanieNaIStopien { get; set; }
	}
}
