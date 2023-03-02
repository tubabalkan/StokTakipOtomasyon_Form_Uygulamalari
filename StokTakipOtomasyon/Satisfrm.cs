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
        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from sepet", baglanti);
                lblGenelToplam.Text = komut.ExecuteScalar() + "TL";
                baglanti.Close();


            }
            catch (Exception)
            {

                ;
            }
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if (txtTc.Text=="")
            {
                txtAdSoyad.Text = "";
                txtTelefon.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Musteriler where tc like '"+txtTc.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtTelefon.Text = read["telefon"].ToString();

            }
            baglanti.Close();
        
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Urun where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                UrunAdi.Text = read["urunadi"].ToString();
                txtSatisFiyati.Text = read["satisfiyati"].ToString();

            }
            baglanti.Close();
        }

        private void Temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                foreach (Control item in grpUrunIslem.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtMiktari)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sepet ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text==read["barkodno"].ToString())
                {
                    durum = false;

                }
            }
            baglanti.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(tc, adsoyad, telefon, barkodno,urunadi, miktari,satisfiyati, toplamfiyati, tarih ) values(@tc, @adsoyad, @telefon, @barkodno,@urunadi, @miktari,@satisfiyati, @toplamfiyati, @tarih)", baglanti);
                komut.Parameters.AddWithValue("tc", txtTc.Text);
                komut.Parameters.AddWithValue("adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("urunadi", UrunAdi.Text);
                komut.Parameters.AddWithValue("miktari", int.Parse(txtMiktari.Text));
                komut.Parameters.AddWithValue("satisfiyati", double.Parse(txtSatisFiyati.Text));
                komut.Parameters.AddWithValue("toplamfiyati", double.Parse(txtToplamFiyati.Text));
                komut.Parameters.AddWithValue("tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update sepet set miktari=miktari+'"+int.Parse(txtMiktari.Text)+ "' where barkodno='" + txtBarkodNo.Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                SqlCommand komut3 = new SqlCommand("update sepet set toplamfiyati=miktari*satisfiyati where barkodno='"+txtBarkodNo.Text+"'", baglanti);  
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
           
            txtMiktari.Text = "1";
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
            foreach (Control item in grpUrunIslem.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtMiktari)
                    {
                        item.Text = "";
                    }
                }

            }


        }

        private void txtMiktari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyati.Text = (double.Parse(txtMiktari.Text) * double.Parse(txtSatisFiyati.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void txtSatisFiyati_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyati.Text = (double.Parse(txtMiktari.Text) * double.Parse(txtSatisFiyati.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno = '"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Urun Sepetten Çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();


        }

        private void btnSatisIptal_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
           
            MessageBox.Show("Urunler Sepetten Çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void btnSatisListele_Click(object sender, EventArgs e)
        {
            satisListelefrm listesatis = new satisListelefrm();
            listesatis.ShowDialog();
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satis(tc, adsoyad, telefon, barkodno,urunadi, miktari,satisfiyati, toplamfiyati, tarih ) values(@tc, @adsoyad, @telefon, @barkodno,@urunadi, @miktari,@satisfiyati, @toplamfiyati, @tarih)", baglanti);
                komut.Parameters.AddWithValue("tc", txtTc.Text);
                komut.Parameters.AddWithValue("adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                komut.Parameters.AddWithValue("satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                SqlCommand komut2 = new SqlCommand("update urun set miktari = miktari - '" + int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()) + "' where barkodno = '" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from sepet", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();

            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();


        }
    }
}
