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
    public partial class frmbrans : Form
    {
        public frmbrans()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmbrans_Load(object sender, EventArgs e)
        {
            //datagride bransları çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_brans", bgl.baglanti()) ;
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagriddeki verileri alanlara çekme
            int sec = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[sec].Cells[1].Value.ToString();
            txtbransad.Text = dataGridView1.Rows[sec].Cells[0].Value.ToString();

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            //brans ekleme
            SqlCommand komut = new SqlCommand("insert into tbl_brans (bransad) values (@p2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p2", txtbransad.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Eklendi.");
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //brans silme
            SqlCommand komut2 = new SqlCommand("delete from tbl_brans where bransid=@p3", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p3", txtid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Silindi");
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            //brans güncelleme
            SqlCommand komut3 = new SqlCommand("update tbl_brans set bransad=@p4 where bransid=@p5", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p4", txtbransad.Text);
            komut3.Parameters.AddWithValue("@p5", txtid.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Güncellendi");
        }
    }
}
