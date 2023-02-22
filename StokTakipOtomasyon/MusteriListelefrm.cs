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
    public partial class MusteriListelefrm : Form
    {
        public MusteriListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();//Kayıtlar Geçici Tutuluyor
        private void MusteriListelefrm_Load(object sender, EventArgs e)
        {
            Goster();

        }

        private void Goster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Musteriler", baglanti);
            adtr.Fill(daset, "Musteriler");
            dataGridView1.DataSource = daset.Tables["Musteriler"];
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            
        
        
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new  SqlCommand("update Musteriler set adsoyad=@adsoyad, telefon=@telefon,  adres=@adres, email=@email where tc=@tc",baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Musteriler"].Clear();
            Goster();
            MessageBox.Show("Güncelleme İşlemi Başarılı Bir Şekilde Tamamlanıdı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Musteriler where tc='"+dataGridView1.CurrentRow.Cells["tc"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Musteriler"].Clear();
            Goster();
            MessageBox.Show("Kayıt Başarılı Bir Şekilde Silinmiştir.");

        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Musteriler where tc like '%"+txtTcAra.Text+"%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        
        
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
