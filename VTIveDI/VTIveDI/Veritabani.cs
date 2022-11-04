using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace VTIveDI
{
    public class Veritabani
    {
        public Veritabani(string veriTabaniAdi)
        {
            baglanti = new SqlConnection("Data Source=.;Initial Catalog="+veriTabaniAdi+";Integrated Security=True");
        }

        SqlConnection baglanti;
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adtr = new SqlDataAdapter();
        DataTable dt = new DataTable();

        public DataTable Select(string sorgu)
        {
            dt = new DataTable();
            baglanti.Open();
            komut.CommandText = sorgu;
            komut.Connection = baglanti;
            adtr.SelectCommand = komut;
            adtr.Fill(dt);
            baglanti.Close();
            return dt;
        }

        public int UpdateDelete(string sorgu)
        {
            baglanti.Open();
            komut.CommandText = sorgu;
            komut.Connection = baglanti;
            int kayitSayisi = komut.ExecuteNonQuery();
            baglanti.Close();
            return kayitSayisi;
        }
    }
}
