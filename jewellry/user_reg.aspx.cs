using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace jewellry
{
    public partial class user_reg : System.Web.UI.Page
    {
        connectionclass ob = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(reg_id) from pro_login";
            string regid = ob.fn_scalar(sel);
            int Reg_id = 0;
            if (regid == "")
            {
                Reg_id = 1;
            }
            else
            {
                int newregid = Convert.ToInt32(regid);
                Reg_id = newregid + 1;
            }
            string ins = "insert into user_reg values(" + Reg_id + ",'" + TextBox1.Text + "', " + TextBox2.Text + ",'" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "')";
            int i = ob.fn_nonquery(ins);
            string inslog = "insert into pro_login values(" + Reg_id + ",'" + TextBox8.Text + "','" + TextBox9.Text + "','user','active')";
            int i1 = ob.fn_nonquery(inslog);
        }
    }
}