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
    public partial class Markafrm : Form
    {
        public Markafrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stok_Takip;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Markafrm_Load(object sender, EventArgs e)
        {
            kategoriGetir();
        }

        private void kategoriGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboKategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori, marka) values ('"+comboKategori.Text+"'   ,'" + txtMarka.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            txtMarka.Text = "";
            comboKategori.Text = "";

            MessageBox.Show("Marka Eklendi");
        }
    }
}
