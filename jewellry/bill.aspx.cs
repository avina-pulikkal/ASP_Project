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
    public partial class bill : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string str = "select user_reg.user_name,user_reg.user_address,bill.total_amt,bill.bill_date from bill join user_reg on user_reg.user_id=bill.reg_id where reg_id='" + Session["userid"] + "'";
                SqlDataReader dr = obj.fn_reader(str);
                while(dr.Read())
                {
                    Label6.Text = dr["user_name"].ToString();
                    Label7.Text = dr["user_address"].ToString();
                    Label10.Text = dr["total_amt"].ToString();
                    Label8.Text = dr["bill_date"].ToString();
                }
                string str1 = "select count(quantity) from orderr where reg_id='" + Session["userid"] + "'";
                string quantity = obj.fn_scalar(str1);
                Label9.Text = quantity;
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("payment.aspx");
        }
    }
}