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
using System.Net.Mail;

namespace stok
{
    public partial class frmmusteriekle : Form
    {
        public frmmusteriekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-JP0V8LE\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        private void txtTc_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTc.Text == "" || txtAdSoyad.Text == "" || txtTel.Text == "" || txtAdres.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz.", "Uyarı");

                
            }
            else if (11!=txtTc.Text.Length)
            {
                MessageBox.Show("Kimlik numaranızı eksiksiz giriniz.");
            }
            else if (11 != txtTel.Text.Length)
            {
                MessageBox.Show("Telefon numaranızı eksiksiz giriniz.");
            }

            else 
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into musteri(tc,adsoyad,telefon,adres,email) values(@tc,@adsoyad,@telefon,@adres,@email)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.Parameters.AddWithValue("@email", txtEmail.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri Kaydı Eklendi.");
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                        this.Close();
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmmusteriekle_Load(object sender, EventArgs e)
        {

        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Kimlik Numaranız Sadece Rakam Olmalıdır.", "Uyarı");
            }  
           
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Telefon Numaranız Sadece Rakam Olmalıdır.", "Uyarı");
            }
        }

        private void txtAdSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar))
            {
                MessageBox.Show("Adınız ve Soyadınız sadece harf almalıdır. ","Uyarı");
            }

        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

            




        }

        private void txtEmail_ControlRemoved(object sender, ControlEventArgs e)
        {

        }
    }
}
