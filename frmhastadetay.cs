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
    public partial class frmhastadetay : Form
    {
        public frmhastadetay()
        {
            InitializeComponent();
        }
        public string tc;

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmhastadetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;

            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select hastaad,hastasoyad from tbl_hastalar where hastatc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where hastatc="+tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            //Branş Çekme
            SqlCommand komut2 = new SqlCommand("select bransad from tbl_brans",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branş Ve Doktor Seçme
            cmbdoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktorbrans=@p2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p2", cmbbrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Aktif Randevular
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select * from tbl_randevular where randevubrans='" + cmbbrans.Text +"'"  + " and randevudoktor='"+cmbdoktor.Text+"'" +"and randevudurum=0", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void lnkbilgiduzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Bilgi Güncelleme
            frmbilgiduzenle fr = new frmbilgiduzenle();
            fr.tcno = lbltc.Text;
            fr.Show();
            
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //randevu seçme
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();

        }

        private void btnrandevual_Click(object sender, EventArgs e)
        {
            //randevu oluşturma
            SqlCommand kmt = new SqlCommand("update tbl_randevular set randevudurum=1,hastatc=@p1,hastasikayet=@p2 where randevuid=@p3",bgl.baglanti());
            kmt.Parameters.AddWithValue("@p1", lbltc.Text);
            kmt.Parameters.AddWithValue("@p2", rchsikayet.Text);
            kmt.Parameters.AddWithValue("@p3", txtid.Text);
            kmt.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu oluşturuldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
