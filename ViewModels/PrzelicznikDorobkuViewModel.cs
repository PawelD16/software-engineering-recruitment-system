using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przeliczniki_dorobku")]
    public class PrzelicznikDorobkuViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int PrzyznawanePunkty { get; set; }

        [ForeignKey("KategoriaDorobku")]
        public long FkIdKategoriaDorobku { get; set; }
        public KategoriaDorobkuViewModel KategoriaDorobku { get; set; }

        [ForeignKey("PrzelicznikKierunkowy")]
        public long FkIdPrzelicznikKierunkowy { get; set; }
        public PrzelicznikKierunkowyViewModel PrzelicznikKierunkowy { get; set; }
    }
}
