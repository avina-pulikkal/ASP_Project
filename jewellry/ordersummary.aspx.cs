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
    public partial class ordersummary : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "select Productt.product_name,productt.product_image,orderr.quantity,orderr.total_amt from productt join orderr on productt.product_id=orderr.product_id where reg_id=" + Session["userid"] + " and status='pending'";
            DataSet ds = obj.fn_dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();

            string s = "select max(bill_id) from bill where reg_id='" + Session["userid"] + "'";
            string bill = obj.fn_scalar(s);
            int b = Convert.ToInt32(bill);

            string str2 = "select total_amt from bill where reg_id='" + Session["userid"] + "' and bill_id=" + b + " and status='pending'";
            string amount = obj.fn_scalar(str2);
            int am = Convert.ToInt32(amount);
            Label1.Text = am.ToString();
            //string str1 = "select sum(total_amt) from orderr where reg_id='" + Session["userid"] + "'";
            //string amount = obj.fn_scalar(str1);
            //int am = Convert.ToInt32(amount);
            //Label1.Text = am.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("bill.aspx");
        }
    }
}