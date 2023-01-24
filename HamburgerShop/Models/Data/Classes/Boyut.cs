using System.ComponentModel.DataAnnotations;

namespace HamburgerShop.Models.Data.Classes
{
    public class Boyut
    {
        public int BoyutId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string BoyutAdi { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal BoyutFiyati { get; set; }
    }
}
