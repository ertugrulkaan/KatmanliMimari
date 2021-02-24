using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Urun Eklendi";
        public static string ProductCannotAdded = "Urun Eklenemedi";
        public static string ProductNameInvalid = "Urun ismi gecersiz";
        public static string ProductDeleted = "Urun Silindi";
        public static string MaintenanceTime = "Bakim saati islem yapilamaz";
        public static string ProductsListed = "Urunler listelendi";
        public static string ProductListed = "Urun gosterildi";

        public static string ProductCountOfCategoryError = "Bir kategorideki maksimum urun sayisina ulasildi.";

        public static string ProductUpdated = "Urun Guncellendi";
        public static string ProductCannotUpdated = "Urun Guncellenemedi";

        public static string ProductExitsError = "Urun zaten sistemde kayitlidir";

        public static string CategoryCountLimitExceeded = "Kategori limiti asildi";
    }
}
