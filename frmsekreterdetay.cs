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
    public partial class frmsekreterdetay : Form
    {
        public frmsekreterdetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        public string tcno2;
        private void btnduyuruolustur_Click(object sender, EventArgs e)
        {
            //duyuru oluşturma
            SqlCommand komut5 = new SqlCommand("insert into tbl_duyuru (duyuru) values (@r2)", bgl.baglanti());
            komut5.Parameters.AddWithValue("@r2", rchduyuru.Text);
            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void frmsekreterdetay_Load(object sender, EventArgs e)
        {
            //ad soyad çekme
            lbltc.Text = tcno2;
            SqlCommand komut = new SqlCommand("select sekreteradsoyad from tbl_sekreter where sekretertc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();


            //Branşları comboboxa çekme
            SqlCommand komut3 = new SqlCommand("select bransad from tbl_brans ", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbbrans.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();

            


            //branşları datagride çekme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select bransad as 'Branşlar' from tbl_brans", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //doktorları datagride çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select (doktorad + ' ' +doktorsoyad) as 'Doktorlar', doktorbrans as 'Doktor Branşları' from tbl_doktor", bgl.baglanti());
            da1.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            //Randevu oluşturma
            SqlCommand komut2 = new SqlCommand("insert into tbl_randevular (randevutarih,randevusaat,randevubrans,randevudoktor) values (@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p2", msktarih.Text);
            komut2.Parameters.AddWithValue("@p3", msksaat.Text);
            komut2.Parameters.AddWithValue("@p4", cmbbrans.Text);
            komut2.Parameters.AddWithValue("@p5", cmbdoktor.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branşa göre doktorları comboboxa çekme
            cmbdoktor.Items.Clear();
            SqlCommand komut4 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktorbrans=@r1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@r1", cmbbrans.Text);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                cmbdoktor.Items.Add(dr4[0] + " " + dr4[1]);
            }
            bgl.baglanti().Close();
        }

        private void btndoktorpanel_Click(object sender, EventArgs e)
        {
            frmdoktorpaneli fr = new frmdoktorpaneli();
            fr.Show();
            
        }

        private void btnrandevuliste_Click(object sender, EventArgs e)
        {
            frmrandevulistesi frm1 = new frmrandevulistesi();
            frm1.Show();
        }

        private void btnbranspanel_Click(object sender, EventArgs e)
        {
            frmbrans fr = new frmbrans();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmduyurular fr = new frmduyurular();
            fr.Show();
        }
    }
}
