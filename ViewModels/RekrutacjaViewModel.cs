using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("rekrutacje")]
    public class RekrutacjaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime DataOtwarciaRekrutacji { get; set; }

        public DateTime DataZamknieciaRekrutacji { get; set; }

        public DateTime DataZamknieciaPrzyjec { get; set; }

        [ForeignKey("StatusRekrutacji")]
        public string StatusRekrutacjiEnumLiteral { get; set; }
        public StatusRekrutacjiViewModel StatusRekrutacji { get; set; }

        [ForeignKey("StopienStudiow")]
        public string StopienStudiowEnumLiteral { get; set; }
        public StopienStudiowViewModel StopienStudiow { get; set; }

        [ForeignKey("SemestrRekrutacji")]
        public string SemestrRekrutacjiEnumLiteral { get; set; }
        public SemestrRekrutacjiViewModel SemestrRekrutacji { get; set; }
    }
}
