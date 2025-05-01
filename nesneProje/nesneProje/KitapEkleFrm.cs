using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace nesneProje
{
    public partial class KitapEkleFrm : Form
    {
        public KitapEkleFrm()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        private void b_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("insert into Kitap(barkodNo, kitapAdi, yazari, yayinEvi, sayfaSayisi, turu, stokSayisi, rafNo, aciklama, kayitTarihi) values(@barkodNo,@kitapAdi,@yazari,@yayinEvi,@sayfaSayisi,@turu,@stokSayisi,@rafNo ,@aciklama, @kayitTarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodNo", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapAdi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinEvi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfaSayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@turu", cmbKitapTuru.Text);
            komut.Parameters.AddWithValue("@stokSayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@rafNo", txtRafNo.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.Parameters.AddWithValue("@kayitTarihi", DateTime.Now.ToShortDateString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Kaydı Yapıldı :) ");
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {        
                        item.Text = " ";       
                }
            }




        }
    }
    
}
