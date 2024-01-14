using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("oceny")]
    public class OcenaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Range(0, 100)]
        public int WartoscProcentowa { get; set; }

        [ForeignKey("Matura")]
        public long MaturaId { get; set; }
        public MaturaModel Matura { get; set; }

        [ForeignKey("Przedmiot")]
        public long FkIdPrzedmiot { get; set; }
        public PrzedmiotModel Przedmiot { get; set; }
    }
}
