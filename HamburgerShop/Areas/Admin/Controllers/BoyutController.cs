using HamburgerShop.Models.Data.Classes;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerShop.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class BoyutController : Controller
    {
        private readonly UygulamaDbContext _db;

        public BoyutController(UygulamaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var boyutlarListesi = _db.Boyutlar.ToList();
            return View(boyutlarListesi);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Ekle(Boyut boyut)
        {
            if (_db.Boyutlar.FirstOrDefault(u => u.BoyutAdi == boyut.BoyutAdi) != null || boyut.BoyutFiyati <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _db.Boyutlar.Add(boyut);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekBoyut = _db.Boyutlar.Find(id);
            TempData["Id"] = guncellenecekBoyut!.BoyutId;
            return View(guncellenecekBoyut);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Guncelle(Boyut boyut)
        {
            var guncellenecekBoyut = _db.Boyutlar.Find(TempData["Id"]);

            var digerBoyutlar = _db.Boyutlar.Except(_db.Boyutlar.Where(u => u.BoyutId == guncellenecekBoyut!.BoyutId)).ToList();

            if (digerBoyutlar.Any(u => u.BoyutAdi == guncellenecekBoyut!.BoyutAdi) || guncellenecekBoyut!.BoyutFiyati <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                guncellenecekBoyut!.BoyutAdi = boyut.BoyutAdi;
                guncellenecekBoyut.BoyutFiyati = boyut.BoyutFiyati;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(guncellenecekBoyut);
        }

        public IActionResult Sil(int id)
        {
            var silinecekBoyut = _db.Boyutlar.Find(id);
            _db.Boyutlar.Remove(silinecekBoyut!);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
