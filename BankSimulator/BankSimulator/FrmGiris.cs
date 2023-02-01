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
using System.Data.SqlClient;

namespace BankSimulator
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RVCRK4G\\SQLEXPRESS;Initial Catalog=FASTSimulator;Integrated Security=True");

        public string hesapno;


        private void linkLabelKayıtOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KayıtForm frm = new KayıtForm();
            frm.Show();

        }

        private void buttonGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Kisiler where HESAPNO=@p1 AND SIFRE=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", maskedTextBoxHesapNo.Text);
            komut.Parameters.AddWithValue("@p2", textBoxSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                VeriSayfasi frm = new VeriSayfasi();
               frm.hesapno = maskedTextBoxHesapNo.Text;
                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("HesapNo Veya Şifre Hatalı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
        }
    }
}
