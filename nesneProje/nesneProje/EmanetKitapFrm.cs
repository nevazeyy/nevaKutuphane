using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nesneProje
{
    public partial class EmanetKitapFrm : Form
    {
        public EmanetKitapFrm()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut; 
        DataSet daset = new DataSet();
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void sepetListele()
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from sepet", baglanti);
            adtr.Fill(daset, "sepet");
            sepet.DataSource = daset.Tables["sepet"];
            baglanti.Close();


            //baglanti = vtIslemleri.baglan();

            //baglanti.Open();
            //MySqlDataAdapter adtr = new MySqlDataAdapter("select * from kitap", baglanti);
            //adtr.Fill(daset, "kitap");
            //dataGridView1.DataSource = daset.Tables["kitap"];
            //baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("insert into sepet(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(txtKitapSayisi.Text));
            komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İstediğiniz Kitaplar Başarıyla Sepetinize Eklenmiştir :]", "Ekleme İşlemi");
            daset.Tables["sepet"].Clear();


            sepetListele();
            
        }
   
        private void EmanetKitapFrm_Load(object sender, EventArgs e)
        {
            sepetListele();
            
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select * from uye where tc like '"+txtTcAra.Text+"'",baglanti);
            MySqlDataReader read = komut.ExecuteReader();

       

            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();

            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut2 = new MySqlCommand("select sum(kitapsayisi) from emanetkitaplar", baglanti);
            //lblKayitliKitapSayisi.Text = komut2.ExecuteScalar().ToString();
            baglanti.Close();

            if (txtTcAra.Text == " ")
            {
                foreach (Control item in grpUyeBilgi.Controls)
                {
                    item.Text = " ";

                }
            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select * from kitap where barkodno like '"+txtBarkodNo.Text+"'",baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinevi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
               
                
            }
            baglanti.Close();
            if (txtBarkodNo.Text == " ")
            {

                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = " ";
                        }
                    }
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            baglanti = vtIslemleri.baglan();

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("delete from sepet where barkodno='" + sepet.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi yapıldı ! iyi günlerr :)","Silme İşlemi");
            daset.Tables["sepet"].Clear();
            sepetListele();
     
        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {
      
              
                    if (txtTcAra.Text != " " && txtAdSoyad.Text != " " && txtYas.Text != " " && txtTelefon.Text != " ")
                    {
                        for (int i = 0; i < sepet.Rows.Count-1; i++)
                        {
                            baglanti = vtIslemleri.baglan();

                            baglanti.Open();
                            MySqlCommand komut = new MySqlCommand("insert into emanetkitaplar(tc,adsoyad,yas,telefon,barkodno,kitap, yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi, iadetarihi) values(@tc,@adsoyad,@yas,@telefon,@barkodno,@kitap,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
                            komut.Parameters.AddWithValue("@tc", txtTcAra.Text);
                            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            komut.Parameters.AddWithValue("@yas", txtYas.Text);
                            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            komut.Parameters.AddWithValue("@barkodno", sepet.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapadi", sepet.Rows[i].Cells["kitapadi"].Value.ToString());
                            komut.Parameters.AddWithValue("@yazari", sepet.Rows[i].Cells["yazari"].Value.ToString());
                            komut.Parameters.AddWithValue("@yayinevi", sepet.Rows[i].Cells["yayinevi"].Value.ToString());
                            komut.Parameters.AddWithValue("@sayfasayisi", sepet.Rows[i].Cells["sayfasayisi"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(sepet.Rows[i].Cells["kitapsayisi"].Value.ToString()));
                            komut.Parameters.AddWithValue("@teslimtarihi", sepet.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("@iadetarihi", sepet.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                            MySqlCommand komut2 = new MySqlCommand("update uye set okukitapsayisi=okukitapsayisi+'" + int.Parse(sepet.Rows[i].Cells["kitapsayisi"].Value.ToString()) +"' where tc= '"+txtTcAra.Text+ "' ", baglanti);
                            komut2.ExecuteNonQuery();
                            MySqlCommand komut3 = new MySqlCommand("update kitap set stokSayisi=stokSayisi- +'" + int.Parse(sepet.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "' where barkodno= '" + sepet.Rows[i].Cells["barkodno"] + "' ", baglanti);
                            komut3.ExecuteNonQuery();

                            baglanti.Close();

                        }
                        baglanti = vtIslemleri.baglan();

                        baglanti.Open();
                        MySqlCommand komut4 = new MySqlCommand("delete from sepet", baglanti);
                        komut4.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("KİTAPLAR EMANET EDİLDİ :)");
                        daset.Tables["sepet"].Clear();
                        sepetListele();
                        txtTcAra.Text = " ";
                     
                    }
                    else
                    {
                        MessageBox.Show("Önce Üye İsimi Seçmenizz Gerekiir!!!", "Uyarı");     
                    }
                
 

            
          
            
            
            
            


        }
    }
}
