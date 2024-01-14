using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace projektowaniaOprogramowania.ViewModels
{
    [Table("przedmioty")]
    public class PrzedmiotModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NazwaPrzedmiotu { get; set; }
        
        public ICollection<PrzelicznikPrzedmiotuModel> PrzelicznikiPrzedmiotow { get; } =
            new List<PrzelicznikPrzedmiotuModel>();
        
        // [ForeignKey("Przelicznik")]
        // public long FkIdPrzelicznik { get; set; }
        //
        // public PrzedmiotViewModel Przelicznik;
    }
}
