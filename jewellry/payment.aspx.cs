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
    public partial class payment : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string s = "select max(bill_id) from bill where reg_id='" + Session["userid"] + "'";
                string bill = obj.fn_scalar(s);
                int b = Convert.ToInt32(bill);

                string str = "select total_amt from bill where reg_id=" + Session["userid"] + " and bill_id="+b+"";
                string amount = obj.fn_scalar(str);
                Session["amt"] = amount;
                Label3.Text = amount;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            payservice.ServiceClient obj1 = new payservice.ServiceClient();
            string s = obj1.balancecheck(Convert.ToInt32(TextBox1.Text));
            int m = Convert.ToInt32(s);
            if(m>Convert.ToInt32(Session["amt"]))
            {
                string newbal = (m - Convert.ToInt32(Session["amt"])).ToString();
                Billpayservice.ServiceClient obj2 = new Billpayservice.ServiceClient();
                int b = obj2.balanceupdate(TextBox1.Text, newbal);
                if(b!=0)
                {
                    string s1 = "update orderr set status='paid' where reg_id=" + Session["userid"] + "";
                    int c = obj.fn_nonquery(s1);
                    string s2 = "update bill set status='paid' where reg_id=" + Session["userid"] + "";
                    int d = obj.fn_nonquery(s2);
                }
                Label4.Text = "successfully paid";
                string str = "select max(cart_id) from orderr";
                string maxcartid = obj.fn_scalar(str);
                int mcatid = Convert.ToInt32(maxcartid);
                int product_id = 0, reg_id = 0, quantity = 0, new_stk = 0;
                string status = "";
                for(int i=1;i<mcatid;i++)
                {
                    string sel = "select * from orderr where order_id=" + i + "";
                    SqlDataReader dr = obj.fn_reader(sel);
                    while(dr.Read())
                    {
                        product_id = Convert.ToInt32(dr["product_id"]);
                        reg_id = Convert.ToInt32(dr["reg_id"]);
                        quantity = Convert.ToInt32(dr["quantity"]);
                        status = dr["status"].ToString();
                    }
                    string r = Session["userid"].ToString();
                    string u = reg_id.ToString();
                    if(u==r)
                    {
                        if(status=="paid")
                        {
                            string s2 = "select pro_stock from productt where product_id=" + product_id + "";
                            string st = obj.fn_scalar(s2);
                            int k = Convert.ToInt32(st);
                            if(k>quantity)
                            {
                                new_stk = k - quantity;
                            }
                            else
                            {
                                new_stk = 0;
                            }
                            string s4 = "update productt set pro_stock=" + new_stk + " where product_id=" + product_id + "";
                            int j = obj.fn_nonquery(s4);
                            //string s5 = "select pro_stock from product";
                            //string t1 = obj.fn_scalar(s5);
                            //int sta = Convert.ToInt32(t1);


                            //if(sta==0)
                            //{
                            //    string s6 = "update productt set pro_status='unavailable' where product_id=" + product_id + "";
                            //    int x = obj.fn_nonquery(s6);
                            //}
                     }
                    }
                }
                    

            }
            else
            {
                Label4.Text = "Insufficient balance";
            }
           
        }
    }
}