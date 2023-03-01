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
    public partial class Satisfrm : Form
    {
        public Satisfrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from sepet", baglanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        
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

        private void btnUrunListeleme_Click(object sender, EventArgs e)
        {
            UrunListelemefrm listele = new UrunListelemefrm();
            listele.ShowDialog();
        }

        private void Satisfrm_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }
    }
}
