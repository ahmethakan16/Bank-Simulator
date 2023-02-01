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
    public partial class Hareketler : Form
    {
        public Hareketler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RVCRK4G\\SQLEXPRESS;Initial Catalog=FASTSimulator;Integrated Security=True");

        public void listele()
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Hareket", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt= new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource= dt;

        }
        private void Hareketler_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
