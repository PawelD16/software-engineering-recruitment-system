using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("kategorie_osiagniecia")]
    public class KategoriaOsiagnieciaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NazwaKategorii { get; set; }
    }
}
