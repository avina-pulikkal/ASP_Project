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
    public partial class login_pro : System.Web.UI.Page
    {
        connectionclass ob = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select count(reg_id) from pro_login where username='" + TextBox1.Text + "' and password='" + TextBox2.Text + "'";
            string cid = ob.fn_scalar(str);
            int cid1 = Convert.ToInt32(cid);
            if (cid1 == 1)
            {
                string str1 = "select reg_id from pro_login where username='" + TextBox1.Text + "' and password='" + TextBox2.Text + "'";
                string regid = ob.fn_scalar(str1);
                Session["userid"] = regid;
                string str2 = "select log_type from pro_login where username='" + TextBox1.Text + "' and password='" + TextBox2.Text + "'";
                string logtype = ob.fn_scalar(str2);
                if (logtype == "admin")
                {
                    Response.Redirect("admin_home.aspx");
                }
                else if (logtype == "user")
                {
                    Response.Redirect("userhome.aspx");
                }
            }
        }
    }
}