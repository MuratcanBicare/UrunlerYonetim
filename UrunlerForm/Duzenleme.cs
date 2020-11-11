using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrunlerForm.Data;

namespace UrunlerForm
{

    public partial class Duzenleme : Form
    {
        public event EventHandler UrunDuzenlendi;
        private readonly UrunVeri db;
        private readonly Urun urun;
        public Duzenleme(UrunVeri urunVeri, Urun urun)
        {
            db = urunVeri;
            this.urun = urun;
            InitializeComponent();
            Text = urun.UrunAd;
            txtUrunAd.Text = urun.UrunAd;
            nudFiyat.Value = urun.BirimFiyat;
            nudStokAdet.Value = urun.StokAdedi;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (UrunVarMi())
            {
                if (nudFiyat.Value != urun.BirimFiyat || nudStokAdet.Value != urun.StokAdedi)
                    DuzenlemeMethod();
                else
                    MessageBox.Show("Ürün mevcut.");
                
                return;
            }
            urun.UrunAd = txtUrunAd.Text;
            DuzenlemeMethod();
            Close();
           
        }

        private void DuzenlemeMethod()
        {
            urun.StokAdedi = (int)nudStokAdet.Value;
            urun.BirimFiyat = nudFiyat.Value;
            urun.DegisimZamani = DateTime.Now;
            UrunDuzenlendiginde(EventArgs.Empty);
        }

        private void UrunDuzenlendiginde(EventArgs args)
        {
            UrunDuzenlendi?.Invoke(this, args);
        }

        private bool UrunVarMi()
        {
            return db.Urunler.Any(x => x.UrunAd.ToLower() == txtUrunAd.Text.ToLower());
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
