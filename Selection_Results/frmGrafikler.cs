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

namespace Selection_Results
{
    public partial class frmGrafikler : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=OSMAN\SQLEXPRESS;Initial Catalog=DBSECIM;Integrated Security=True");

        public frmGrafikler()
        {
            InitializeComponent();
        }

        private void frmGrafikler_Load(object sender, EventArgs e)
        {
            // İlçeleri ComboBox'a çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT ILCEAD FROM TBL_ILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
            baglanti.Close();

            // Grafiğe Seçim Sonuçlarını Getirme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) FROM TBL_ILCE;", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A Partisi", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B Partisi", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C Partisi", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D Partisi", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E Partisi", dr2[4]);
            }
            dr2.Close();
            baglanti.Close();
        }

        private void frmGrafikler_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form kapatıldığında bağlantıyı kapat
            if (baglanti.State != ConnectionState.Closed)
                baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_ILCE WHERE ILCEAD=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                progressBar1.Value = int.Parse(dr["APARTI"].ToString());
                progressBar2.Value = int.Parse(dr["BPARTI"].ToString());
                progressBar3.Value = int.Parse(dr["CPARTI"].ToString());
                progressBar4.Value = int.Parse(dr["DPARTI"].ToString());
                progressBar5.Value = int.Parse(dr["EPARTI"].ToString());

                lbla.Text = dr[2].ToString();
                lblb.Text = dr[3].ToString();
                lblc.Text = dr[4].ToString();
                lbld.Text = dr[5].ToString();
                lble.Text = dr[6].ToString();
            }
            dr.Close();
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmoygiris fr = new frmoygiris();
            fr.Show();
            this.Hide();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.CadetBlue;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }
    }
}
