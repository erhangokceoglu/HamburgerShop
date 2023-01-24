using Microsoft.AspNetCore.Mvc;

namespace HamburgerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly UygulamaDbContext _db;

        public DashboardController(UygulamaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var siparislerListesi = _db.Siparisler.Include(o => o.Hamburger).Include(o => o.Icecek).Include(o => o.Boyut).Include(o => o.EkstraMalzemeler).OrderByDescending(s => s).ToList();
            return View(siparislerListesi);
        }
    }
}
