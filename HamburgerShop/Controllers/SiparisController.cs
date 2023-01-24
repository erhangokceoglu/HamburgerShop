using HamburgerShop.Models.Data.Classes;
using HamburgerShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static NuGet.Packaging.PackagingConstants;

namespace HamburgerShop.Controllers
{
    public class SiparisController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly Siparis _siparis;

        public SiparisController(UygulamaDbContext db, Siparis siparis)
        {
            _db = db;
            _siparis = siparis;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.HamburgerList = _db.Hamburgerler.ToList();
            indexViewModel.IcecekList = _db.Icecekler.ToList();
            indexViewModel.Ekstralar = _db.EkstraMalzemeler.Select(e => new SelectListItem()
            {
                Text = e.EkstraMalzemeAdi + "-" + e.EkstraMalzemeFiyati+ " ₺",
                Value = e.EkstraMalzemeId.ToString(),
            }).ToList();
            indexViewModel.BoyutList = _db.Boyutlar.ToList();
            return View(indexViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel vm)
        {
            List<EkstraMalzeme> ekstraList = new List<EkstraMalzeme>();
            var secilenHamburger = _db.Hamburgerler.Where(m => m.HamburgerId == vm.Siparis.Hamburger.HamburgerId).FirstOrDefault();
            var secilenIcecek = _db.Icecekler.Where(m => m.IcecekId == vm.Siparis.Icecek!.IcecekId).FirstOrDefault();
            foreach (var item in vm.Ekstralar)
            {
                if (item.Selected)
                {
                    ekstraList.Add(_db.EkstraMalzemeler.FirstOrDefault(e => e.EkstraMalzemeId == Convert.ToInt32(item.Value))!);
                }
            }
            _siparis.SiparisAdet = vm.HamburgerAdet;
            _siparis.Hamburger = secilenHamburger!;
            _siparis.EkstraMalzemeler = ekstraList;
            _siparis.Boyut = _db.Boyutlar.Find(vm.BoyutId)!;
            _siparis.Icecek = secilenIcecek;
            _siparis.Hamburger.HamburgerFotograf = secilenHamburger!.HamburgerFotograf;
            _siparis.ToplamTutar = 
            (_siparis.Hamburger.HamburgerFiyati + _siparis.Boyut.BoyutFiyati + _siparis.Icecek!.IcecekFiyati) * _siparis.SiparisAdet;
            _siparis.ToplamTutar += ekstraList.Sum(e => e.EkstraMalzemeFiyati);
            _db.Siparisler.Add(_siparis);
            _db.SaveChanges();
            return RedirectToAction("SiparisGoster");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SiparisEkle(Siparis siparis)
        {
            return View();
        }

        public IActionResult SiparisGoster()
        {
             var sonSiparis = _db.Siparisler
            .Include(o => o.Hamburger)
            .Include(o => o.Icecek)
            .Include(o => o.Boyut)
            .Include(o => o.EkstraMalzemeler)
            .OrderByDescending(o => o.SiparisId)
            .FirstOrDefault();
            return View(sonSiparis);
        }
    }
}
