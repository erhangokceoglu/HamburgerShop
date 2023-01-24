using System.ComponentModel.DataAnnotations;

namespace HamburgerShop.Models.Data.Classes
{
    public class Icecek
    {
        public int IcecekId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string IcecekAdi { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal IcecekFiyati { get; set; }
    }
}
