using HamburgerShop.Models.Data.Classes;
using HamburgerShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HamburgerController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly Hamburger _hamburger;

        public HamburgerController(UygulamaDbContext db, Hamburger hamburger)
        {
            _db = db;
            _hamburger = hamburger;
        }

        public IActionResult Index()
        {
            //Hamburgerler listesini getirir.
            var hamburgerlerListesi = _db.Hamburgerler.ToList();
            return View(hamburgerlerListesi);
        }

        public IActionResult Ekle() // Ekleme view'ını Getirir
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(HamburgerViewModel hamburgerViewModel)
        //Hamburgeri db'ye ekle ve güncel Hamburger listesi görünümüne git (Hamburger hamburger)
        //Formdan gelen HamburgerViewModel nesnesini yakalayıp, özelliklerini hakiki hamburger'e atayıp db'ye ekleyeceğiz.

        {
            try
            {
                //resim yüklediyse resmin adını GUID + uzantısını formatında DB'de tut. Kendisi de images klasörüne kaydet
                if (hamburgerViewModel.HamburgerResmi != null)
                {
                    //Önce resmin uzantısını çekelim
                    //var uzanti = Path.GetExtension(hamburgerViewModel.HamburgerResmi.FileName);

                    //Daha sonra dosyamızın adını oluşturalım 
                    var dosyaAdi = hamburgerViewModel.HamburgerResmi.FileName;

                    //Sonra dosyanın kaydedileceği konum belirlenir
                    var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                    //Sonra dosya için bir akış ortamı oluşturulur. Kaydetmek için ortam hazırlıyoruz.
                    var akisOrtami = new FileStream(konum, FileMode.Create);

                    //Resmi o klasöre kaydet 
                    hamburgerViewModel.HamburgerResmi.CopyTo(akisOrtami);
                    akisOrtami.Close();

                    //Resmi o klasöre kaydettiniz
                    //_db'ye de sadece dosya adını ekle
                    _hamburger.HamburgerFotograf = dosyaAdi;
                }
                //Formdan gelenler
                //Automapper ile daha kolay olur.

                _hamburger.HamburgerAdi = hamburgerViewModel.HamburgerAdi;
                _hamburger.HamburgerFiyati = hamburgerViewModel.HamburgerFiyati;

                if (_db.Hamburgerler.FirstOrDefault(u => u.HamburgerAdi == _hamburger.HamburgerAdi) != null || hamburgerViewModel.HamburgerFiyati <= 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                //_db ye ekleme olacak.
                _db.Hamburgerler.Add(_hamburger);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        //Gönderilen Id'ye ait hamburgeri getir ve post etmek için tekrar view'a yolla
        public IActionResult Guncelle(int id)
        {
            Hamburger guncelenecekHamburger = _db.Hamburgerler.Find(id)!;
            HamburgerViewModel hamburgerViewModel = new HamburgerViewModel();
            hamburgerViewModel.HamburgerAdi = guncelenecekHamburger.HamburgerAdi;
            hamburgerViewModel.HamburgerFiyati = guncelenecekHamburger.HamburgerFiyati;
            TempData["Id"] = guncelenecekHamburger.HamburgerId;
            return View(hamburgerViewModel);
        }

        [HttpPost]
        public IActionResult Guncelle(HamburgerViewModel hamburgerViewModel)
        {
            try
            {
                var guncelenecekHamburger = _db.Hamburgerler.Find((int)TempData["Id"]!);

                if (hamburgerViewModel.HamburgerResmi != null)
                {
                    var dosyaAdi = hamburgerViewModel.HamburgerResmi.FileName;
                    if (guncelenecekHamburger!.HamburgerFotograf == dosyaAdi)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    //Sonra dosyanın kaydedileceği konum belirlenir
                    var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                    //Sonra dosya için bir akış ortamı oluşturulur. Kaydetmek için ortam hazırlıyoruz.
                    var akisOrtami = new FileStream(konum, FileMode.Create);

                    //Resmi o klasöre kaydet 
                    hamburgerViewModel.HamburgerResmi.CopyTo(akisOrtami);
                    akisOrtami.Close();

                    //Resmi o klasöre kaydettiniz
                    //_db'ye de sadece dosya adını ekle
                    guncelenecekHamburger.HamburgerFotograf = dosyaAdi;
                }
                guncelenecekHamburger!.HamburgerAdi = hamburgerViewModel.HamburgerAdi;
                guncelenecekHamburger.HamburgerFiyati = hamburgerViewModel.HamburgerFiyati;

                var digerHamburgerler = _db.Hamburgerler.Except(_db.Hamburgerler.Where(u => u.HamburgerId == guncelenecekHamburger.HamburgerId)).ToList();

                if (digerHamburgerler.Any(u => u.HamburgerAdi == guncelenecekHamburger.HamburgerAdi) || guncelenecekHamburger.HamburgerFiyati <= 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                _db.Hamburgerler.Update(guncelenecekHamburger);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction(nameof(Index)); //Güncelledikten sonra yine hamburger listesine git
        }
        public IActionResult Sil(int id)
        {
            Hamburger silinecekHamburger = _db.Hamburgerler.Find(id)!;
            if (silinecekHamburger == null)
            {
                return NotFound();
            }
            _db.Hamburgerler.Remove(silinecekHamburger);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResimKaldir(int id)
        {
            Hamburger resmiSilinecekHamburger = _db.Hamburgerler.Find(id)!;
            if (resmiSilinecekHamburger.HamburgerFotograf == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var resimAdi = resmiSilinecekHamburger.HamburgerFotograf;
            resmiSilinecekHamburger.HamburgerFotograf = null;
            _db.Hamburgerler.Update(resmiSilinecekHamburger);
            _db.SaveChanges();

            //Bu resmi kullanan başka hamburger yoksa resmi de sil.
            var silinecekHamburgerlerHaricindekiHamburgerler = _db.Hamburgerler.Except(_db.Hamburgerler.Where(u => u.HamburgerId == resmiSilinecekHamburger.HamburgerId));
            var resmiKullananBaskaYok = silinecekHamburgerlerHaricindekiHamburgerler.All(u => u.HamburgerFotograf != resimAdi);

            if (resmiKullananBaskaYok)
            {
                //resimler klasörünün olduğu yerdeki dosyaları al.
                string[] dosyalar = Directory.GetFiles("C:\\Users\\Hp\\source\\repos\\HamburgerShop\\HamburgerShop\\wwwroot\\images\\");

                //Bu dosyalardan silmek istediğimizi bulup sileceğiz.
                foreach (var item in dosyalar)
                {
                    var resimIsmiDizisi = item.Split("\\");

                    //Artık dosya adı bu resim dizisinin son elemanı oldu
                    if (resimIsmiDizisi[resimIsmiDizisi.Length - 1] == resimAdi)
                    {
                        System.IO.File.Delete(item);
                        break;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
