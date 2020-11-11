using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrunlerForm.Data;

namespace UrunlerForm
{
    public partial class UrunlerForm : Form
    {
        Duzenleme duzenleme;
        UrunVeri db;
        Urun urun;
        BindingList<Urun> blUrunler;
        public UrunlerForm()
        {
            VerileriOku();
            blUrunler = new BindingList<Urun>(db.Urunler);
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false;
            dgvUrunler.DataSource = blUrunler;
        }
        private void VerileriOku()
        {
            try
            {
                string json = File.ReadAllText("veri.json");
                db = JsonConvert.DeserializeObject<UrunVeri>(json);
            }
            catch (Exception)
            {

                db = new UrunVeri();
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAd.Text.Trim();
            bool urunVarMi = blUrunler.Any(x => x.UrunAd.ToLower() == urunAd.ToLower());
            if (txtUrunAd.Text != string.Empty && nudFiyat.Value > -1 && nudStokAdet.Value >= 0 && urunVarMi == false)
            {
                urun = new Urun
                {
                    UrunAd = urunAd,
                    BirimFiyat = nudFiyat.Value,
                    StokAdedi = (int)nudStokAdet.Value,
                    DegisimZamani = DateTime.Now
                };
                blUrunler.Add(urun);
            }
            else if (urunVarMi)
            {
                MessageBox.Show("Var olan ürün girdiniz.");

            }
            else
            {
                MessageBox.Show("Eksik veya hatalı bilgi girdiniz.");
            }
            dgvUrunler.DataSource = blUrunler;
        }

        private void dgvUrunler_DoubleClick(object sender, EventArgs e)
        {
            DuzenleFormAc();
        }

        private void DuzenleFormAc()
        {
            Duzenle();
        }

        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VerileriKaydet();
        }

        private void VerileriKaydet()
        {
            string json = JsonConvert.SerializeObject(db, Formatting.Indented);
            File.WriteAllText("veri.json", json);
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            Duzenle();
        }

        private void Duzenle()
        {
            if (dgvUrunler.SelectedRows.Count == 0)
                return;

            Urun seciliUrun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
            Duzenleme frmDuzenle = new Duzenleme(db, seciliUrun);
            frmDuzenle.UrunDuzenlendi += FrmDuzenle_UrunDuzenlendi;
            frmDuzenle.Show();
            //blUrunler.ResetBindings();

        }

        private void FrmDuzenle_UrunDuzenlendi(object sender, EventArgs e)
        {
            blUrunler.ResetBindings();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            UrunSil();
        }

        private void UrunSil()
        {
            DialogResult dr = MessageBox.Show("Seçili detayları silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                blUrunler.Remove((Urun)dgvUrunler.SelectedRows[0].DataBoundItem);
            }
            blUrunler.ResetBindings();
        }

        private void dgvUrunler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                UrunSil();
            }
        }

        private void dgvUrunler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count > 0)
            {
                btnDuzenle.Enabled = btnSil.Enabled = true;
            }
            else
            {
                btnDuzenle.Enabled = btnSil.Enabled = false;
            }
        }
    }
}
