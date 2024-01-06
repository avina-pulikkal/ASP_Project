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
    public partial class viewproducts : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string str = "select * from productt where cat_id=" + Session["catid"] + "";
                DataSet ds = obj.fn_dataset(str);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
           
        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["proid"] = id;
            Response.Redirect("product_details.aspx");
        }
    }
}