﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class frmgirisler : Form
    {
        public frmgirisler()
        {
            InitializeComponent();
        }

        private void btnhasta_Click(object sender, EventArgs e)
        {
            frmhasta fr = new frmhasta();
            fr.Show();
            this.Hide();
            
        }

        private void btndoktor_Click(object sender, EventArgs e)
        {
            frmdoktorgiris fr = new frmdoktorgiris();
            fr.Show();
            this.Hide();
        }

        private void btnsekreter_Click(object sender, EventArgs e)
        {
            frmsekretergiris fr = new frmsekretergiris();
            fr.Show();
            this.Hide();
        }
    }
}
