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
    public partial class AnaSayfaFrm : Form
    {
        public AnaSayfaFrm()
        {
            InitializeComponent();
        }
     

       

        private void btnUyeEkle_Click_1(object sender, EventArgs e)
        {
            UyeEkleFrm uyeekle = new UyeEkleFrm();
            uyeekle.ShowDialog();
        }

        private void btnUyeListele_Click_1(object sender, EventArgs e)
        {
            UyeLidtelemeFrm uyeliste = new UyeLidtelemeFrm();
            uyeliste.ShowDialog();
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            KitapEkleFrm kitapekle = new KitapEkleFrm();
            kitapekle.ShowDialog();
        }

        private void btnKitapListele_Click(object sender, EventArgs e)
        {
            KitapListelemeFrm kitaplistele = new KitapListelemeFrm();
            kitaplistele.ShowDialog();
        }

        private void btnEmanetVer_Click(object sender, EventArgs e)
        {
            EmanetKitapFrm emanetverme = new EmanetKitapFrm();
            emanetverme.ShowDialog();
        }

        private void btnEmanetListele_Click(object sender, EventArgs e)
        {
            EmanetKitapListele listele = new EmanetKitapListele();
            listele.ShowDialog();
        }

        private void btnEmanetIade_Click(object sender, EventArgs e)
        {
            KitapİadeFrm iade = new KitapİadeFrm();
            iade.ShowDialog();
        }

        private void btnSiralama_Click(object sender, EventArgs e)
        {
            SiralamaFrm sirala = new SiralamaFrm();
            sirala.ShowDialog();
        }

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            GrafikFrm gr = new GrafikFrm();
            gr.ShowDialog();
        }
    }
}
