using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_osiagniec")]
    public class PrzelicznikOsiagniecModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int PrzyznawanePunkty { get; set; }

        [ForeignKey("PrzelicznikKierunkowy")]
        public long FkIdPrzelicznikKierunkowy { get; set; }
        public PrzelicznikKierunkowyModel PrzelicznikKierunkowy { get; set; }

        [ForeignKey("KategoriaOsiagniecia")]
        public long FkIdKategoriaOsiagniecia { get; set; }
        public KategoriaOsiagnieciaModel KategoriaOsiagniecia { get; set; }
    }
}
