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
    public partial class EmanetKitapListele : Form
    {
        public EmanetKitapListele()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        string komutSatiri;
        DataSet daset = new DataSet();

        private void EmanetKitapListele_Load(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["EmanetKitaplar"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                EmanetListele();
            }
            else if (comboBox1.SelectedIndex==1)
            {
                baglanti = vtIslemleri.baglan();
                baglanti.Open();
                MySqlDataAdapter adtr = new MySqlDataAdapter("select * from EmanetKitaplar where '" + DateTime.Now.ToShortDateString() + "'>iadetarihi", baglanti);
                adtr.Fill(daset, "EmanetKitaplar");
                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                baglanti = vtIslemleri.baglan();
                baglanti.Open();
                MySqlDataAdapter adtr = new MySqlDataAdapter("select * from EmanetKitaplar where '" + DateTime.Now.ToShortDateString() + "'<= iadetarihi", baglanti);
                adtr.Fill(daset, "EmanetKitaplar");
                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }
        }
    }
}
