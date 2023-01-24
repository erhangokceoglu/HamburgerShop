using System.Drawing;

namespace HamburgerShop.Models.Data.Classes
{
    public class Siparis
    {
        public int SiparisId { get; set; }

        public byte SiparisAdet { get; set; }

        public decimal ToplamTutar { get; set; }

        public Boyut Boyut { get; set; } = null!;

        public List<EkstraMalzeme>? EkstraMalzemeler { get; set; }

        public Hamburger Hamburger { get; set; } = null!;

        public Icecek? Icecek { get; set; }
    }
}
