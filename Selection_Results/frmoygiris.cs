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
using static System.Net.Mime.MediaTypeNames;


namespace Selection_Results
{
    public partial class frmoygiris : Form
    {
        public frmoygiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=OSMAN\SQLEXPRESS;Initial Catalog=DBSECIM;Integrated Security=True");
        private void btngiris_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtilce.Text) || string.IsNullOrEmpty(txta.Text) || string.IsNullOrEmpty(txtb.Text) ||
                string.IsNullOrEmpty(txtc.Text) || string.IsNullOrEmpty(txtd.Text) || string.IsNullOrEmpty(txtE.Text))
            {
                MessageBox.Show("Eksik veri girişi! Tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_ILCE (ILCEAD,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
                komut.Parameters.AddWithValue("@P1", txtilce.Text);
                komut.Parameters.AddWithValue("@P2", txta.Text);
                komut.Parameters.AddWithValue("@P3", txtb.Text);
                komut.Parameters.AddWithValue("@P4", txtc.Text);
                komut.Parameters.AddWithValue("@P5", txtd.Text);
                komut.Parameters.AddWithValue("@P6", txtE.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Oy girişi gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }


        private void frmoygiris_Load(object sender, EventArgs e)
        {

        }

        private void btngrafik_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnistatistik_Click(object sender, EventArgs e)
        {
            frmGrafikler fr = new frmGrafikler();
            fr.Show();
            this.Hide();
        }
    }
}
