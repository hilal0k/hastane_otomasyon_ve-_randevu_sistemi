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
    public partial class frmdoktorpaneli : Form
    {
        public frmdoktorpaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmdoktorpaneli_Load(object sender, EventArgs e)
        {
            //datagride doktor çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select doktorad,doktorsoyad,doktorbrans,doktortc,doktorsifre from tbl_doktor", bgl.baglanti()); ;
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //comboboxa brans çekme
            SqlCommand komut7 = new SqlCommand("select bransad from tbl_brans ", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                cmbbrans.Items.Add(dr7[0]);
            }
            bgl.baglanti().Close();


        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            //doktor ekleme
            SqlCommand komut6 = new SqlCommand("insert into tbl_doktor ( doktorad,doktorsoyad,doktorbrans,doktortc,doktorsifre) values (@k1,@k2,@k3,@k4,@k5)", bgl.baglanti());
            komut6.Parameters.AddWithValue("@k1",txtad.Text);
            komut6.Parameters.AddWithValue("@k2",txtsoyad.Text);
            komut6.Parameters.AddWithValue("@k3",cmbbrans.Text);
            komut6.Parameters.AddWithValue("@k4",msktc.Text);
            komut6.Parameters.AddWithValue("@k5",txtsifre.Text);
            komut6.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi.");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //verilerin otomatik dolmasını sağlama
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtad.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtsoyad.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            cmbbrans.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktc.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsifre.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //doktor silme
            SqlCommand komut9 = new SqlCommand("delete from tbl_doktor where doktortc=@p1", bgl.baglanti());
            komut9.Parameters.AddWithValue("@p1", msktc.Text);
            komut9.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            //doktor bilgileri güncelleme
            SqlCommand komut10 = new SqlCommand("update tbl_doktor set doktorad=@d1,doktorsoyad=@d2,doktorbrans=@d3,doktorsifre=@d5 where doktortc=@d4  ", bgl.baglanti());
            komut10.Parameters.AddWithValue("@d1", txtad.Text);
            komut10.Parameters.AddWithValue("@d2", txtsoyad.Text);
            komut10.Parameters.AddWithValue("@d3", cmbbrans.Text);
            komut10.Parameters.AddWithValue("@d4", msktc.Text);
            komut10.Parameters.AddWithValue("@d5", txtsifre.Text);
            komut10.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
