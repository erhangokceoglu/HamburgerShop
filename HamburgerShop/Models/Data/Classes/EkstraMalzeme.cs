using System.ComponentModel.DataAnnotations;

namespace HamburgerShop.Models.Data.Classes
{
    public class EkstraMalzeme
    {
        public int EkstraMalzemeId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string EkstraMalzemeAdi { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal EkstraMalzemeFiyati { get; set; }

        public Siparis? Siparis { get; set; }
    }
}
