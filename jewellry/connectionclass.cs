using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace jewellry
{
    public class connectionclass
    {
        SqlCommand cmd;
        SqlConnection con;
        public connectionclass()
        {
            con = new SqlConnection(@"server=DESKTOP-GS7CKL0\SQLEXPRESS;database=project;Integrated security=true");
        }
        public int fn_nonquery(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public string fn_scalar(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            string s = cmd.ExecuteScalar().ToString();
            con.Close();
            return s;
        }
        public SqlDataReader fn_reader(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }
        public DataSet fn_dataset(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        public DataTable fn_Datatable(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            SqlDataAdapter db = new SqlDataAdapter(sqlquery, con);
            DataTable dt = new DataTable();
            db.Fill(dt);
            return dt;
        }
    }
}