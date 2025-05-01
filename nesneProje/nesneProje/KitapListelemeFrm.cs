using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace nesneProje
{
    public partial class KitapListelemeFrm : Form
    {
        public KitapListelemeFrm()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        DataSet daset = new DataSet();
        private void kitapListele()
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from kitap", baglanti);
            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            baglanti.Close();
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("update kitap set kitapAdi=@kitapAdi, yazari=@yazari,yayinEvi=@yayinEvi, sayfaSayisi=@sayfaSayisi,turu=@turu,stokSayisi=@stokSayisi, rafNo=@rafNo,aciklama=@aciklama where barkodNo=@barkodNo)", baglanti);
            komut.Parameters.AddWithValue("@barkodNo", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapAdi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@sayfaSayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@turu", cmbKitapTuru.Text);
            komut.Parameters.AddWithValue("@stokSayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@rafno",txtRafNo.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi gerçekleşti!!!!");
            daset.Tables["kitap"].Clear();
            kitapListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                } 
            }
        }

        private void KitapListelemeFrm_Load(object sender, EventArgs e)
        {
            kitapListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kaydı Sileceğinizden Emin Misiniz ?!?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti = vtIslemleri.baglan();

                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("delete from kitap where barkodNo=@barkodNo", baglanti);
                komut.Parameters.AddWithValue("@barkodNo", dataGridView1.CurrentRow.Cells["barkodNo"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme İşlemi gerçekleşti!!!!");
                daset.Tables["kitap"].Clear();
                kitapListele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = " ";
                    }
                }


            }
        }

      

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select * from kitap where barkodNo like '" + txtBarkodAra + "'", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapAdi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinevi.Text = read["yayinEvi"].ToString();
                txtSayfaSayisi.Text = read["sayfaSayisi"].ToString();
                cmbKitapTuru.Text = read["turu"].ToString();
                txtStokSayisi.Text = read["stokSayisi"].ToString();
                txtRafNo.Text = read["rafNo"].ToString();
                txtAciklama.Text = read["aciklama"].ToString();

            }
            baglanti.Close();
        }

        private void txtBarkodAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["kitap"].Clear();
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from kitap where barkodNo like '%" + txtBarkodAra.Text + "%'", baglanti);
            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            baglanti.Close();

         
        }
    }
}
