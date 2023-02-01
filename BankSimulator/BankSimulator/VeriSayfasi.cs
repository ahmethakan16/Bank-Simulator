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

namespace BankSimulator
{
    public partial class VeriSayfasi : Form
    {
        public VeriSayfasi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RVCRK4G\\SQLEXPRESS;Initial Catalog=FASTSimulator;Integrated Security=True");

        public string hesapno;
        private void VeriSayfasi_Load(object sender, EventArgs e)
        {
            labelHesapNo.Text= hesapno;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Kisiler where HESAPNO=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", hesapno);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                labelAdSoyad.Text = dr[1] + "  " + dr[2];
                labelTCkimlik.Text = dr[3].ToString();
                labelTelefon.Text = dr[4].ToString();
                labelHesapNo.Text = dr[5].ToString();
            }
            baglanti.Close();
        }

        private void buttonGonder_Click(object sender, EventArgs e)
        {
            //Hesab'a para girişi;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Tbl_Hesap set BAKIYE=BAKIYE+@p1 where HESAPNO=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", decimal.Parse((textBoxTutar.Text)));
            komut.Parameters.AddWithValue("@p2", maskedTextBoxHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            //Hesabın Para Azalışı;
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Update Tbl_Hesap set BAKIYE=BAKIYE-@p3 where HESAPNO=@p4", baglanti);
            komut2.Parameters.AddWithValue("@p3", decimal.Parse(textBoxTutar.Text));
            komut2.Parameters.AddWithValue("@p4", hesapno);
            komut2.ExecuteNonQuery();
            baglanti.Close();

            //Hareket Tablosu;
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("Insert Into Tbl_Hareket (GONDEREN,ALICI,TUTAR) Values (@p1,@p2,@p3)", baglanti);
            komut3.Parameters.AddWithValue("@p1", labelHesapNo.Text);
            komut3.Parameters.AddWithValue("@p2", maskedTextBoxHesapNo.Text);
            komut3.Parameters.AddWithValue("@p3", decimal.Parse(textBoxTutar.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşlem Gerçekleşti");
        }

        private void buttonHareket_Click(object sender, EventArgs e)
        {
            Hareketler frm = new Hareketler();
            frm.Show();
            
        }
    }
}
