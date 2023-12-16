using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_osiagniec")]
    public class PrzelicznikOsiagniecViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int PrzyznawanePunkty { get; set; }

        [ForeignKey("PrzelicznikKierunkowy")]
        public long FkIdPrzelicznikKierunkowy { get; set; }
        public PrzelicznikKierunkowyViewModel PrzelicznikKierunkowy { get; set; }

        [ForeignKey("KategoriaOsiagniecia")]
        public long FkIdKategoriaOsiagniecia { get; set; }
        public KategoriaOsiagnieciaViewModel KategoriaOsiagniecia { get; set; }
    }
}
