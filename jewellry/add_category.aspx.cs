using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jewellry
{
    public partial class add_category : System.Web.UI.Page
    {
        connectionclass obj = new connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string photo = "~/photo/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(photo));
            string str = "insert into category values('" + TextBox1.Text + "','" + photo + "','" + TextBox2.Text + "','" + TextBox3.Text + "')";
            int i = obj.fn_nonquery(str);
            if(i==1)
            {
                Label5.Text = "inserted";
            }
        }

    }
}