using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrunlerForm.Data
{
    public class Urun
    {
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public string BirimFiyatTL => BirimFiyat.ToString("0.00TL");
        public int StokAdedi { get; set; }
        public StokDurumu StokDurumu => StokAdedi > 0 ? StokDurumu.Var : StokDurumu.Yok;
        public override string ToString()
        {
            return $"{UrunAd} ({BirimFiyat:0.00} ₺)";
        }
        public DateTime? DegisimZamani { get; set; }
        

    }
}
