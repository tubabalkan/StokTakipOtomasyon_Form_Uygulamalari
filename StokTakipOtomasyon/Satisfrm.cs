using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipOtomasyon
{
    public partial class Satisfrm : Form
    {
        public Satisfrm()
        {
            InitializeComponent();
        }

        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            MusteriEklefrm ekle = new MusteriEklefrm();
            ekle.ShowDialog();
        }

        private void btnMusteriListele_Click(object sender, EventArgs e)
        {
            MusteriListelefrm listele = new MusteriListelefrm();
            listele.ShowDialog();

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            UrunEklefrm ekle = new UrunEklefrm();
            ekle.ShowDialog();

        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            Markafrm marka = new Markafrm();
            marka.ShowDialog();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {
            Kategorifrm kategori = new Kategorifrm();
            kategori.ShowDialog();
        }
    }
}
