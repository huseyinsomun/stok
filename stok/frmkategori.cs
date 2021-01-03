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
    public partial class frmkategori : Form
    {
        public frmkategori()
        {
            InitializeComponent();
        }
        

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-JP0V8LE\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        private void kategorikontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtkategori.Text==read["kategori"].ToString() || txtkategori.Text=="" )
                {
                    durum = false;

                }
            }
            baglanti.Close();

        }
        private void btnekle_Click(object sender, EventArgs e)
        {
            kategorikontrol();
            if (txtkategori.Text == "")
            {
                MessageBox.Show("Boş Alanları Doldurunuz");
            }
            else
            {
                if (durum == true)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values('" + txtkategori.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Kategori Eklendi");
                }
                else
                {
                    MessageBox.Show("Böyle bir kategori var", "Uyarı");
                }
                txtkategori.Text = "";
                this.Close();
            }
        }

        private void txtkategori_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmkategori_Load(object sender, EventArgs e)
        {

        }
    }
}
