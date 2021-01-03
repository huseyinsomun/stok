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

namespace stok
{
    public partial class frmsatis : Form
    {
        public frmsatis()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-JP0V8LE\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
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

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if (txtTc.Text=="")
            {
                txtAdSoyad.Text = "";
                txtTel.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from musteri where tc like '"+txtTc.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtTel.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }

        private void btnMusListe_Click(object sender, EventArgs e)
        {
            frmmusteriliste liste = new frmmusteriliste();
            liste.ShowDialog();
        }

        private void btnMusEkle_Click(object sender, EventArgs e)
        {
            frmmusteriekle ekle = new frmmusteriekle();
            ekle.ShowDialog();
        }

        private void formsatis_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }

        private void btnUrEkle_Click(object sender, EventArgs e)
        {
            frmurunekle ekle = new frmurunekle();
            ekle.ShowDialog();
        }

        private void btnUrListe_Click(object sender, EventArgs e)
        {
            frmurunlisteleme liste = new frmurunlisteleme();
            liste.ShowDialog();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {
            frmkategori ekle = new frmkategori();
            ekle.ShowDialog();
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmmarka ekle = new frmmarka();
            ekle.ShowDialog();
        }

        private void txtBarkod_TextChanged(object sender, EventArgs e)
        {
            temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtUrunAdi.Text = read["urunadi"].ToString();
                txtSatFiyat.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtMiktar)
                        {
                            item.Text = "";

                        }
                    }

                }
            }
        }
        bool durum;
        private void barkodcontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sepet", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            barkodcontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(tc,adsoyad,telefon,barkodno,urunadi,miktar,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktar,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktar", int.Parse(txtMiktar.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatFiyat.Text));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(txtTopFiyat.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                
            }
            else
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update sepet set miktar=miktar+'"+int.Parse(txtMiktar.Text)+ "' where barkodno='" + txtBarkodNo.Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                SqlCommand komut3 = new SqlCommand("update sepet set toplamfiyati=miktar*satisfiyati where barkodno='"+txtBarkodNo.Text+"'", baglanti);
                komut3.ExecuteNonQuery();
                
               
                baglanti.Close();
            }

            
            txtMiktar.Text = "1";
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtMiktar)
                    {
                        item.Text = "";

                    }
                }

            }

        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTopFiyat.Text =(double.Parse( txtMiktar.Text )*double.Parse(txtSatFiyat.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void txtSatFiyat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTopFiyat.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatFiyat.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Ürün sepetten çıkarıldı.");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void BtnSatİptal_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Ürünler sepetten çıkarıldı.");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void btnSatEkle_Click(object sender, EventArgs e)
        {
            frmsatislisteleme liste = new frmsatislisteleme();
            liste.ShowDialog();
        }
        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from sepet", baglanti);
                lblGenelToplam.Text = komut.ExecuteScalar()+" TL";
                baglanti.Close();

            }
            catch (Exception)
            {

                ;
            }
        }

        private void BtnSatYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satis(tc,adsoyad,telefon,barkodno,urunadi,miktar,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktar,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("@miktar", int.Parse(dataGridView1.Rows[i].Cells["miktar"].Value.ToString()));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                SqlCommand komut2 = new SqlCommand("update urun set miktar=miktar-'" + int.Parse(dataGridView1.Rows[i].Cells["miktar"].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString()+"'", baglanti);
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
            MessageBox.Show("Satış işlemi tamamlandı");

        }
    }
}
