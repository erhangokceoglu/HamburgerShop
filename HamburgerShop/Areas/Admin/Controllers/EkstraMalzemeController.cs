using HamburgerShop.Models.Data.Classes;
using HamburgerShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EkstraMalzemeController : Controller
    {
        private readonly UygulamaDbContext _db;

        public EkstraMalzemeController(UygulamaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var ekstraMalzemelerListesi = _db.EkstraMalzemeler.ToList();
            return View(ekstraMalzemelerListesi);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Ekle(EkstraMalzeme ekstraMalzeme)
        {
            if (_db.EkstraMalzemeler.FirstOrDefault(u => u.EkstraMalzemeAdi == ekstraMalzeme.EkstraMalzemeAdi) != null  || ekstraMalzeme.EkstraMalzemeFiyati <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _db.EkstraMalzemeler.Add(ekstraMalzeme);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekEkstraMalzeme = _db.EkstraMalzemeler.Find(id);
            TempData["Id"] = guncellenecekEkstraMalzeme!.EkstraMalzemeId;
            return View(guncellenecekEkstraMalzeme);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Guncelle(EkstraMalzeme ekstraMalzeme)
        {
            var guncellenecekEkstraMalzeme = _db.EkstraMalzemeler.Find(TempData["Id"]);

            var digerEsktraMalzemeler = _db.EkstraMalzemeler.Except(_db.EkstraMalzemeler.Where(u => u.EkstraMalzemeId == guncellenecekEkstraMalzeme!.EkstraMalzemeId)).ToList();

            if (digerEsktraMalzemeler.Any(u => u.EkstraMalzemeAdi == guncellenecekEkstraMalzeme!.EkstraMalzemeAdi) || guncellenecekEkstraMalzeme!.EkstraMalzemeFiyati <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                guncellenecekEkstraMalzeme!.EkstraMalzemeAdi = ekstraMalzeme.EkstraMalzemeAdi;
                guncellenecekEkstraMalzeme.EkstraMalzemeFiyati = ekstraMalzeme.EkstraMalzemeFiyati;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(guncellenecekEkstraMalzeme);
        }

        public IActionResult Sil(int id)
        {
            var silinecekEkstraMalzeme = _db.EkstraMalzemeler.Find(id);
            _db.EkstraMalzemeler.Remove(silinecekEkstraMalzeme!);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
