﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=hilal\\SQLEXPRESS;Initial Catalog=hastaneproje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
