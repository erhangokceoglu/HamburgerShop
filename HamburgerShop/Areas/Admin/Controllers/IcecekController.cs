using HamburgerShop.Models.Data.Classes;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IcecekController : Controller
    {
        private readonly UygulamaDbContext _db;

        public IcecekController(UygulamaDbContext db)
        {
            _db = db;            
        }

        public IActionResult Index()
        {
            var iceceklerlerListesi = _db.Icecekler.ToList();
            return View(iceceklerlerListesi);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Ekle(Icecek icecek)
        {
            if (_db.Icecekler.FirstOrDefault(u => u.IcecekAdi == icecek.IcecekAdi) != null || icecek.IcecekFiyati <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _db.Icecekler.Add(icecek);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekIcecek = _db.Icecekler.Find(id);
            TempData["Id"] = guncellenecekIcecek!.IcecekId;
            return View(guncellenecekIcecek);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Guncelle(Icecek icecek)
        {
            var guncellenecekIcecek = _db.Icecekler.Find(TempData["Id"]);

            var digerIcecekler = _db.Icecekler.Except(_db.Icecekler.Where(u => u.IcecekId== guncellenecekIcecek!.IcecekId)).ToList();

            if (digerIcecekler.Any(u => u.IcecekAdi == guncellenecekIcecek!.IcecekAdi) || guncellenecekIcecek!.IcecekFiyati <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                guncellenecekIcecek!.IcecekAdi = icecek.IcecekAdi;
                guncellenecekIcecek.IcecekFiyati = icecek.IcecekFiyati;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(guncellenecekIcecek);
        }

        public IActionResult Sil(int id)
        {
            var silinecekIcecek = _db.Icecekler.Find(id);
            _db.Icecekler.Remove(silinecekIcecek!);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
