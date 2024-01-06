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
    public partial class cart_view : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gridbind();
                    }
        }
        public void gridbind()
        {
            string str = "select productt.product_name,productt.product_image,cartt.* from cartt join productt on productt.product_id=cartt.product_id where reg_id=" + Session["userid"] +"";
            DataSet ds = obj.fn_dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string del = "delete from cartt where cart_id=" + id + "";
            int j = obj.fn_nonquery(del);
            gridbind();
        }

        

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            gridbind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gridbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            //string s = "select product_price from productt";
            //string m = obj.fn_scalar(s);
            //int pri = Convert.ToInt32(m);
            int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox textqua = (TextBox)GridView1.Rows[i].Cells[2].Controls[0];
            string str = "update cartt set quantity=" + textqua.Text + " where cart_id=" + id + "";
            int j = obj.fn_nonquery(str);
            int q = 0, p = 0;
            string se = "select cartt.quantity,productt.product_price from cartt join productt on productt.product_id=cartt.product_id";
            SqlDataReader dr = obj.fn_reader(se);
            while (dr.Read())
            {
                q = Convert.ToInt32(dr["quantity"]);
                p = Convert.ToInt32(dr["product_price"]);

            }
            //string q = obj.fn_scalar(se);
            //int qu = Convert.ToInt32(q);
            string str1 = "update cartt set total_amt=" + q*p + " where cart_id=" + id + "";
            int n = obj.fn_nonquery(str1);
            GridView1.EditIndex = -1;
            gridbind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select max(cart_id) from cartt";
            string cartid = obj.fn_scalar(str);
            int maxcartid = Convert.ToInt32(cartid);
            int cart_id = 0, product_id=0, reg_id=0, quantity = 0, total_amt = 0;
            for(int i=1;i<=maxcartid;i++)
            {
                string sel = "select * from cartt where cart_id=" + i + "";
                SqlDataReader dr = obj.fn_reader(sel);
                while(dr.Read())
                {
                    cart_id = Convert.ToInt32(dr["cart_id"].ToString());
                    product_id = Convert.ToInt32(dr["product_id"].ToString());
                    reg_id = Convert.ToInt32(dr["reg_id"].ToString());
                    quantity = Convert.ToInt32(dr["quantity"].ToString());
                    total_amt= Convert.ToInt32(dr["total_amt"].ToString());
                }
                string r = Session["userid"].ToString();
                string u = reg_id.ToString();
                if(u==r)
                {
                    string s = "select max(order_id) from orderr";
                    string ordid = obj.fn_scalar(s);
                    int order_id = 0;
                    if(ordid=="")
                    {
                        order_id = 1;

                    }
                    else
                    {
                        int neworder_id = Convert.ToInt32(ordid);
                            order_id = neworder_id + 1;

                    }
                    string ordins="insert into orderr values(" + order_id + "," + cart_id + "," + product_id + "," + reg_id + "," + quantity + "," + total_amt + ",'pending')";
                    int j = obj.fn_nonquery(ordins);
                    if(j!=0)
                    {
                        string del = "delete from cartt where cart_id=" + cart_id + "";
                        int d = obj.fn_nonquery(del);
                    }

                }
            }
            string str1 = "select sum(total_amt) from orderr where reg_id=" + Session["userid"] + "";
            string G_total = obj.fn_scalar(str1);
            Session["Gtotal"] = G_total;
            string s3 = "select max(bill_id) from bill";
            string bid = obj.fn_scalar(s3);
            int bill_id = 0;
            if(bid=="")
            {
                bill_id = 1;
            }
            else
            {
                int newbillid = Convert.ToInt32(bid);
                bill_id = newbillid + 1;
            }
            var billdate = DateTime.Now.ToShortDateString();
            string newdate = Convert.ToDateTime(billdate).ToString("yyyy-MM-dd");
            string insbill = "insert into bill values(" + bill_id + ",'" + newdate + "'," + Session["userid"] + "," + Session["Gtotal"] + ",'pending')";
            int b = obj.fn_nonquery(insbill);
            Response.Redirect("ordersummary.aspx");
        }
    }
}