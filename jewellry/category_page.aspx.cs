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
    public partial class category_page : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                grid_bind();
            }
        }
        public void grid_bind()
        {
            string str = "select * from category";
            DataSet ds = obj.fn_dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string s = "select cat_status from category where cat_id=" + id + "";
            string c = obj.fn_scalar(s);
            if(c=="available")
            {
                string up = "update category set cat_status='unavailable' where cat_id=" + id + "";
                int i = obj.fn_nonquery(up);
                if(i==1)
                {
                    Label1.Text = "blocked";
                }
                else if(c== "unavailable")
                {
                    string upd = "update category set cat_status='available' where cat_id=" + id + "";
                    int i1 = obj.fn_nonquery(upd);
                    if(i1==1)
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
        protected void Gridview1_RowCancelingEdit(object sender,GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            grid_bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int i = e.RowIndex;
            //int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
            //TextBox textname = (TextBox)GridView1.Rows[i].Cells[2].Controls[0];
            //TextBox textdesc=(TextBox)GridView1.Rows[i].Cells[3].Controls[0];
            ////FileUpload fu=(FileUpload)GridView1.Rows[i].Cells[4].Controls[0];
            //string str = "update category set cat_name='" + textname.Text + "',cat_description='" + textdesc.Text + "' where cat_id=" + id + "";
            //int j = obj.fn_nonquery(str);
            //GridView1.EditIndex = -1;
            //grid_bind();


        }
    }
}