using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_dorobku")]
    public class PrzelicznikDorobkuModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int PrzyznawanePunkty { get; set; }

        [ForeignKey("KategoriaDorobku")]
        public long FkIdKategoriaDorobku { get; set; }
        public KategoriaDorobkuModel KategoriaDorobku { get; set; }

        [ForeignKey("PrzelicznikKierunkowy")]
        public long FkIdPrzelicznikKierunkowy { get; set; }
        public PrzelicznikKierunkowyModel PrzelicznikKierunkowy { get; set; }
    }
}
