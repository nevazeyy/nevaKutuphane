using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nesneProje
{
    public partial class KitapİadeFrm : Form
    {
        public KitapİadeFrm()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        string komutSatiri;
        DataSet daset = new DataSet();
        private void KitapİadeFrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
        }
        public void EmanetListele()
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from EmanetKitaplar", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
            baglanti.Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from EmanetKitaplar where tc like '%"+txtTcAra.Text+"%'", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");
            baglanti.Close();
            if (txtTcAra.Text==" ")
            {
                daset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }
        }

        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from EmanetKitaplar where barkodno like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");
            baglanti.Close();
            if (txtBarkodNoAra.Text == " ")
            {
                daset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from EmanetKitaplar where tc=@tc and barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.Parameters.AddWithValue("barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut.ExecuteNonQuery();
            MySqlCommand komut2 = new MySqlCommand("update Kitap set stoksayisi-stoksayisi+'" + dataGridView1.CurrentRow.Cells["kitapsayisi"].Value.ToString() +"'where barkodno-@barkodno", baglanti);
            komut2.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitapciklar İade Edildii :))");
            daset.Tables["EmanetKitaplar"].Clear();
            EmanetListele();
              
        }
    }
}
