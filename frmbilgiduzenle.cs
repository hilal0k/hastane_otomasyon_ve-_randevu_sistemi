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
    public partial class frmbilgiduzenle : Form
    {
        public frmbilgiduzenle()
        {
            InitializeComponent();
        }
        public string tcno;

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmbilgiduzenle_Load(object sender, EventArgs e)
        {
            //hasta verilerini taşıma
            msktc1.Text = tcno;
            SqlCommand komut = new SqlCommand("select * from tbl_hastalar where hastatc=@p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                msktelefon.Text = dr[4].ToString();
                cmbcinsiyet.Text = dr[6].ToString();
                txtsifre1.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            //verileri güncelleme
            SqlCommand komut2 = new SqlCommand("update tbl_hastalar set hastaad=@p2,hastasoyad=@p3,hastatelefon=@p4,hastacinsiyet=@p5,hastasifre=@p6 where hastatc=@p7", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p2", txtad.Text);
            komut2.Parameters.AddWithValue("@p3", txtsoyad.Text);
            komut2.Parameters.AddWithValue("@p4", msktelefon.Text);
            komut2.Parameters.AddWithValue("@p5", cmbcinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", txtsifre1.Text);
            komut2.Parameters.AddWithValue("@p7", msktc1.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
