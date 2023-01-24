using System.ComponentModel.DataAnnotations;

namespace HamburgerShop.ViewModels
{
    public class HamburgerViewModel
    {
        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string HamburgerAdi { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal HamburgerFiyati { get; set; }

        public IFormFile? HamburgerResmi { get; set; }
    }
}
