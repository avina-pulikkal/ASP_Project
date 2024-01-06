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
    public partial class product_page : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grid_bind();
            }
        }
        public void grid_bind()
        {
            string str = "select productt.*,category.cat_name from category join productt on category.cat_id=productt.cat_id";
            DataSet ds = obj.fn_dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string s = "select pro_status from productt where product_id=" + id + "";
            string c = obj.fn_scalar(s);
            if (c == "available")
            {
                string up = "update productt set pro_status='unavailable' where product_id=" + id + "";
                int i = obj.fn_nonquery(up);
                if (i == 1)
                {
                    Label1.Text = "blocked";
                }
                else if (c == "unavailable")
                {
                    string upd = "update productt set pro_status='available' where product_id=" + id + "";
                    int i1 = obj.fn_nonquery(upd);
                    if (i1 == 1)
                    {

                        Label1.Text = "unblocked";
                    }
                }
                grid_bind();

            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            grid_bind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            grid_bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox textname = (TextBox)GridView1.Rows[i].Cells[3].Controls[0];
            TextBox textprice = (TextBox)GridView1.Rows[i].Cells[4].Controls[0];
            TextBox textdesc= (TextBox)GridView1.Rows[i].Cells[5].Controls[0];
            TextBox textstock = (TextBox)GridView1.Rows[i].Cells[7].Controls[0];
            string str = "update productt set product_name='" + textname.Text + "',product_price=" + textprice.Text + ",product_description='" + textdesc.Text + ",pro_stock='" + textstock.Text + "' where product_id='" + id + "'";
            int j = obj.fn_nonquery(str);
            GridView1.EditIndex = -1;
            grid_bind();
        }
    }
}