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

namespace nesneProje
{
    public partial class UyeLidtelemeFrm : Form
    {
        public UyeLidtelemeFrm()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
        }


        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select * from uye where tc like '"+txtTc+"'" ,baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAd.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                cmbCinsiyet.Text = read["cinsiyet"].ToString();
                txtTel.Text = read["telefon"].ToString();
                txtAdres.Text = read["adres"].ToString();
                txtMail.Text = read["email"].ToString();
                txtOkuKitapSayisi.Text = read["okukitapsayisi"].ToString();

            }
            baglanti.Close();
        }
        DataSet daset = new DataSet();
        private void txtAraTc_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["uye"].Clear();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from uye where tc like '%" + txtAraTc.Text + "%'", baglanti);
            adtr.Fill(daset, "uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
        
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kaydı Sileceğinizden Emin Misiniz ?!?","Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("delete from uye where tc=@tc", baglanti);
                komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme İşlemi gerçekleşti!!!!");
                daset.Tables["uye"].Clear();
                uyeListele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = " ";
                    }
                }


            }

        }
        private void uyeListele()
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from uye", baglanti);
            adtr.Fill(daset,"uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
        }

        private void UyeLidtelemeFrm_Load(object sender, EventArgs e)
        {
            uyeListele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("update uye set adsoyad=@adsoyad,yas=@yas,cinsiyet=@cinsiyet,telefon=@telefon,adres=@adres,email=@email,okukitapsayi=@okukitapsayisiwhere tc=@t", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAd.Text);
            komut.Parameters.AddWithValue("@yas", txtYas.Text);
            komut.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
            komut.Parameters.AddWithValue("@telefon", txtTel.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtMail.Text);
            komut.Parameters.AddWithValue("@okukitapsayisi", int.Parse(txtOkuKitapSayisi.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi gerçekleşti!!!!");
            daset.Tables["uye"].Clear();
            uyeListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
