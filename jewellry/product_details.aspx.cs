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
    public partial class product_details : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string str = "select * from productt where product_id=" + Session["proid"]+"";
                SqlDataReader dr = obj.fn_reader(str);
                while(dr.Read())
                {
                    Label1.Text = dr["product_name"].ToString();
                    Label2.Text = dr["product_price"].ToString();
                    Session["proprice"] = dr["product_price"];
                    Label3.Text = dr["product_descri"].ToString();
                    Label6.Text = dr["pro_status"].ToString();
                    Image1.ImageUrl = dr["product_image"].ToString();

                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userhome.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(cart_id) from cartt";
            string cartid = obj.fn_scalar(sel);
            int crtid = 0;
            if(cartid=="")
            {
                crtid = 1;
            }
            else
            {
                int newcrtid = Convert.ToInt32(cartid);
                crtid = newcrtid + 1;
            }
            int p = Convert.ToInt32(Session["proprice"]);
            int q = Convert.ToInt32(TextBox1.Text);
            string s = "insert into cartt values(" +crtid+","+ Session["proid"] + "," + Session["userid"] + "," + TextBox1.Text + "," + p * q + ")";
            int j = obj.fn_nonquery(s);
            if(j==1)
            {
               Label5.Visible = true;
                Label5.Text = "inserted";

           }
        }
    }
}