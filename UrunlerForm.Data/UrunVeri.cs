using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrunlerForm.Data
{
    public class UrunVeri
    {
        public List<Urun> Urunler { get; set; }
        public UrunVeri()
        {
            Urunler = new List<Urun>();
        }
    }
}
