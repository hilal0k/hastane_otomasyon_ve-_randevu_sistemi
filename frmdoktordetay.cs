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
    public partial class frmdoktordetay : Form
    {
        public frmdoktordetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        public string dtc;
        private void frmdoktordetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = dtc;

            SqlCommand komut = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktortc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //randevular 
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select * from tbl_randevular where randevudoktor='"+ lbladsoyad.Text+ "'",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            frmdoktorbilgiguncelle fr = new frmdoktorbilgiguncelle();
            fr.dttc = lbltc.Text;
            fr.Show();
        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            frmduyurular fr = new frmduyurular();
            fr.Show();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //randevu detay
            int secilen = dataGridView1.SelectedCells[0].RowIndex;        
            SqlCommand kmt = new SqlCommand("select hastasikayet from tbl_randevular where randevuid=@p1", bgl.baglanti());
            kmt.Parameters.AddWithValue("@p1", dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            SqlDataReader rd = kmt.ExecuteReader();
            while (rd.Read())
            {
                rchsikayet.Text = rd[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btncıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
