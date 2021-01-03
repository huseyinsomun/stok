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
    public partial class frmmarka : Form
    {
        public frmmarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-JP0V8LE\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        private void markakontrol()
        {   
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text == read["kategori"].ToString() && textBox1.Text == read["marka"].ToString() || comboBox1.Text == "" || textBox1.Text == "")
                {
                    durum = false;

                }
            }
            baglanti.Close();
        }
            private void btnekle_Click(object sender, EventArgs e)
        {
            markakontrol();
            if (textBox1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Boş Alanları Doldunuzu", "Uyarı");
            }
            else
            {
                if (durum == true)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Marka Eklendi");
                }
                else
                {
                    MessageBox.Show("Böyle bir kategori ve marka var", "Uyarı");
                }

                textBox1.Text = "";
                comboBox1.Text = "";
                this.Close();

            }
           


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void kategorigetir()
        {
        baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
        SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());

            }
    baglanti.Close();
        }
        private void frmmarka_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
                MessageBox.Show("Boş Alanları Doldurunuz");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
                MessageBox.Show("Boş Alanları Doldurunuz");
            }
        }
    }
}
