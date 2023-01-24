using System;

namespace HamburgerShop.Models.Data.Classes
{
    public static class DataSeed
    {
        public static void InsertData(ModelBuilder builder)
        {
            builder.Entity<Hamburger>()
                .HasData
            (
                    new Hamburger
                    {
                        HamburgerId = 1,
                        HamburgerAdi = "Cheese Burger",
                        HamburgerFiyati = 60m,
                        HamburgerFotograf = "cheeseburger.jpg",
                    },
                    new Hamburger
                    {
                        HamburgerId = 2,
                        HamburgerAdi = "BigKing Burger",
                        HamburgerFiyati = 90m,
                        HamburgerFotograf = "big-king.jpg",
                    },
                    new Hamburger
                    {
                        HamburgerId = 3,
                        HamburgerAdi = "KingBeef Burger",
                        HamburgerFiyati = 110m,
                        HamburgerFotograf = "kingbeefburger.jpg",
                    },
                    new Hamburger
                    {
                        HamburgerId = 4,
                        HamburgerAdi = "Kofte Burger",
                        HamburgerFiyati = 85m,
                        HamburgerFotograf = "kofteburger.jpg",
                    },
                    new Hamburger
                    {
                        HamburgerId = 5,
                        HamburgerAdi = "Tavuk Burger",
                        HamburgerFiyati = 50m,
                        HamburgerFotograf = "tavukburger.jpg",
                    }
                );

            builder.Entity<Icecek>()
                .HasData
                (
                    new Icecek
                    {
                        IcecekId = 1,
                        IcecekAdi = "Kola",
                        IcecekFiyati = 25m
                    },
                    new Icecek
                    {
                        IcecekId = 2,
                        IcecekAdi = "Fanta",
                        IcecekFiyati = 25m
                    },
                    new Icecek
                    {
                        IcecekId = 3,
                        IcecekAdi = "Ayran",
                        IcecekFiyati = 20m
                    },
                    new Icecek
                    {
                        IcecekId = 4,
                        IcecekAdi = "Su",
                        IcecekFiyati = 15m
                    },
                    new Icecek
                    {
                        IcecekId = 5,
                        IcecekAdi = "Soda",
                        IcecekFiyati = 18m
                    }
                );

            builder.Entity<EkstraMalzeme>()
                .HasData
                (
                    new EkstraMalzeme
                    {
                        EkstraMalzemeId = 1,
                        EkstraMalzemeAdi = "Hardal Sos",
                        EkstraMalzemeFiyati = 5m,
                    },
                    new EkstraMalzeme
                    {
                        EkstraMalzemeId = 2,
                        EkstraMalzemeAdi = "Acı Sos",
                        EkstraMalzemeFiyati = 6m,
                    },
                    new EkstraMalzeme
                    {
                        EkstraMalzemeId = 3,
                        EkstraMalzemeAdi = "Mayonez",
                        EkstraMalzemeFiyati = 4m,
                    },
                    new EkstraMalzeme
                    {
                        EkstraMalzemeId = 4,
                        EkstraMalzemeAdi = "Ketcap",
                        EkstraMalzemeFiyati = 4m,
                    },
                    new EkstraMalzeme
                    {
                        EkstraMalzemeId = 5,
                        EkstraMalzemeAdi = "Ranch Sos",
                        EkstraMalzemeFiyati = 7m,
                    }
                );

            builder.Entity<Boyut>()
                .HasData
                (
                    new Boyut
                    {
                        BoyutId = 1,
                        BoyutAdi = "100gr",
                        BoyutFiyati = 0m
                    },
                    new Boyut
                    {
                        BoyutId = 2,
                        BoyutAdi = "150gr",
                        BoyutFiyati = 24m
                    },
                    new Boyut
                    {
                        BoyutId = 3,
                        BoyutAdi = "200gr",
                       BoyutFiyati = 48m
                    }
                 );
        }
    }
}
