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
    public partial class userhome : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = "select * from category";
                DataSet ds = obj.fn_dataset(str);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
        }

        //protected void ImageButton1_Command(object sender, CommandEventArgs e)
        //{
           
        //}

        protected void ImageButton1_Command1(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["catid"] = id;
            Response.Redirect("viewproducts.aspx");
        }
    }
}