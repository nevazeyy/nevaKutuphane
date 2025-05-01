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
    public partial class SiralamaFrm : Form
    {
        public SiralamaFrm()
        {
            InitializeComponent();
        }
        VeriTabaniIslemleri vtIslemleri = new VeriTabaniIslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        string komutSatiri;
        DataSet daset = new DataSet();
        private void SiralamaFrm_Load(object sender, EventArgs e)
        {
            baglanti = vtIslemleri.baglan();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from Uye order by okukitapsayisi desc", baglanti);
            adtr.Fill(daset, "Uye");
            dataGridView1.DataSource = daset.Tables["Uye"];
            baglanti.Close();
            txtEnCokk.Text = daset.Tables["Uye"].Rows[0]["adsoyad"].ToString();
            txtEnCokk.Text += daset.Tables["Uye"].Rows[0]["okukitapsayisi"].ToString();
            txtEnAz.Text = daset.Tables["Uye"].Rows[dataGridView1.Rows.Count-2]["adsoyad"].ToString();
            txtEnAz.Text += daset.Tables["Uye"].Rows[dataGridView1.Rows.Count - 2]["okukitapsayisi"].ToString();
        }
    }
}
