using System.ComponentModel.DataAnnotations;

namespace HamburgerShop.Models.Data.Classes
{
    public class Hamburger
    {
        public int HamburgerId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string HamburgerAdi { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal HamburgerFiyati { get; set; }

        public string? HamburgerFotograf { get; set; }
    }
}
