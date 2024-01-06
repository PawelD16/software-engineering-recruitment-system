namespace projektowaniaOprogramowania.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //[Table("osoby")]
    public class OsobaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        public string Haslo { get; set; }

        [StringLength(11)]
        public string Pesel { get; set; }
        // pesel or numerPaszportu must not be empty/null, but how?

        [StringLength(10)]
        public string NumerPaszportu { get; set; }

        [Required]
        [StringLength(128)]
        public string Imie { get; set; }

        [Required]
        [StringLength(128)]
        public string Nazwisko { get; set; }

        [Required]
        public DateTime DataZarejestrowania { get; set; }

        [Required]
        public bool CzyEmailPotwierdzony { get; set; }
    }
}
