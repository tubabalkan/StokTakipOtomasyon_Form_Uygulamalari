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

namespace StokTakipOtomasyon
{
    public partial class Kategorifrm : Form
    {
        public Kategorifrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stok_Takip;Integrated Security=True");
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values ('"+txtKategori.Text+"')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            txtKategori.Text = "";
            MessageBox.Show("Kategori Eklendi");

        }

        private void Kategorifrm_Load(object sender, EventArgs e)
        {

        }
    }
}
