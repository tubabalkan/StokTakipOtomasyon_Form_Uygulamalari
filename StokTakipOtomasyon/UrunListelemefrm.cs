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
    public partial class UrunListelemefrm : Form
    {
        public UrunListelemefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
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
        private void UrunListelemefrm_Load(object sender, EventArgs e)
        {
            ÜrünListele();
            kategoriGetir();
        }

        private void ÜrünListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun", baglanti);
            adtr.Fill(daset, "urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BarkodNotxt.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            Kategoritxt.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            Markatxt.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            UrunAditxt.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            Miktaritxt.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            AlisFiyatitxt.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            SatisFiyatitxt.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set urunadi=@urunadi, miktari=@miktari, alisfiyati=@alisfiyati, satisfiyati=@satisfiyati where barkodno = @barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text);
            komut.Parameters.AddWithValue("@urunadi", UrunAditxt.Text);
            komut.Parameters.AddWithValue("@miktari",int.Parse(Miktaritxt.Text) );
            komut.Parameters.AddWithValue("@alisfiyati",double.Parse(AlisFiyatitxt.Text) );
            komut.Parameters.AddWithValue("@satisfiyati",double.Parse(SatisFiyatitxt.Text) );
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urun"].Clear();
            ÜrünListele();
            MessageBox.Show("Güncelleme Yapıldı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarkaGuncelle_Click(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text!="")
            {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set kategori=@kategori, marka=@marka  where barkodno = @barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text);
            komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Yapıldı");
                daset.Tables["urun"].Clear();
                ÜrünListele();

            }
            else
            {
                MessageBox.Show("Barkodno seçili değil", "uyarı");
            }
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }

        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri where kategori = '" + comboKategori.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urun"].Clear();
            ÜrünListele();
            MessageBox.Show("Kayıt Başarılı Bir Şekilde Silinmiştir.");
        }

        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where barkodno like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
