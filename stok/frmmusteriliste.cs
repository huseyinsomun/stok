using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace stok
{
    public partial class frmmusteriliste : Form
    {
        public frmmusteriliste()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-JP0V8LE\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmmusteriliste_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri", baglanti);
            adtr.Fill(daset, "musteri");
            dataGridView1.DataSource = daset.Tables["musteri"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void btnGuncel_Click(object sender, EventArgs e)
        { if (txtTc.Text == "" || txtAdSoyad.Text == "" || txtAdres.Text == "" || txtTel.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Bir Ürün Seçiniz.", "Uyarı");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update musteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.Parameters.AddWithValue("@email", txtEmail.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["musteri"].Clear();
                Kayıt_Göster();
                MessageBox.Show("Müşteri Kaydı Güncellendi.");
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musteri where tc='"+dataGridView1.CurrentRow.Cells["tc"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["musteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kayıt Silindi");

        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri where tc like '%"+txtTcAra.Text+"%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAdSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar))
            {
                MessageBox.Show("Adınız ve Soyadınız sadece harf almalıdır. ", "Uyarı");
            }
        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Telefon Numaranız Sadece Rakam Olmalıdır.", "Uyarı");
            }
        }
    }
}
