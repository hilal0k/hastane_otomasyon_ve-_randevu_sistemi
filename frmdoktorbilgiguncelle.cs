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

namespace Proje_Hastane
{
    public partial class frmdoktorbilgiguncelle : Form
    {
        public frmdoktorbilgiguncelle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string dttc;
        private void frmdoktorbilgiguncelle_Load(object sender, EventArgs e)
        {
            //bilgileri taşıma
            msktc1.Text = dttc;
            SqlCommand cmd = new SqlCommand("select * from tbl_doktor where doktortc=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", msktc1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                cmbbrans.Text = dr[3].ToString();
                txtsifre1.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();

            //branşları comboboxa taşıma
            SqlCommand komut2 = new SqlCommand("select bransad from tbl_brans", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut10 = new SqlCommand("update tbl_doktor set doktorad=@d1,doktorsoyad=@d2,doktorbrans=@d3,doktorsifre=@d5 where doktortc=@d4  ", bgl.baglanti());
            komut10.Parameters.AddWithValue("@d1", txtad.Text);
            komut10.Parameters.AddWithValue("@d2", txtsoyad.Text);
            komut10.Parameters.AddWithValue("@d3", cmbbrans.Text);
            komut10.Parameters.AddWithValue("@d4", msktc1.Text);
            komut10.Parameters.AddWithValue("@d5", txtsifre1.Text);
            komut10.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
