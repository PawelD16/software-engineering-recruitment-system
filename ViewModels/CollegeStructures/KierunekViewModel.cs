using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels.CollegeStructures
{
    [Table("kierunki")]
    public class KierunekViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NazwaKierunku { get; set; }

        [Required]
        [StringLength(64)]
        public string SkrotKierunku { get; set; }

        [Required]
        [StringLength(64)]
        public string ProfilKierunku { get; set; }

        [Required]
        public int Czesne { get; set; }

        [Required]
        public int CzesneDlaCudzoziemcow { get; set; }

        [Required]
        public int CzasTrwaniaWSemestrach { get; set; }

        [Required]
        [StringLength(255)]
        public string Dyscyplina { get; set; }
        
        [Required]
        public string Opis { get; set; }

        [Required]
        [StringLength(20)]
        public Jezyk Jezyk { get; set; }

        [Required]
        [StringLength(20)]
        public StopienStudiow StopienStudiow { get; set; }

        [Required]
        public TrybStudiowania TrybStudiowania { get; set; }

        [ForeignKey("Wydzial")]
        public long FkIdWydzial { get; set; }
        public WydzialViewModel Wydzial { get; set; }
        
        [ForeignKey("Przelicznik")]
        public long FkIdPrzelicznik { get; set; }
        public PrzelicznikKierunkowyViewModel Przelicznik{ get; set; }
        
		// not mapped, for I/O not as model values

		[NotMapped]
		public int CalculatedRecruitationPoints { get; set; }

		[NotMapped]
		public float CalculatedProbabilityOfSucessfulRecruitation { get; set; }
	}

    public enum TrybStudiowania
    {
        Stacjonarne,
        Zdalne,
        Mieszane
    }
}
