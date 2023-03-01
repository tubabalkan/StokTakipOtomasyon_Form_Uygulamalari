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
        bool durum;
        private void kategoriKontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtKategori.Text == read["kategori"].ToString() || txtKategori.Text=="" )
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            kategoriKontrol();
            if (durum == true)
            {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values ('"+txtKategori.Text+"')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Kategori Eklendi");
            }
            else
            {
                MessageBox.Show("Böyle bir kategori var", "Uyarı");
            }
            txtKategori.Text = "";

        }

        private void Kategorifrm_Load(object sender, EventArgs e)
        {

        }
    }
}
