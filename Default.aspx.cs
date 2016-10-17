using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmotionVisualizer
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void displayGraphsActionMethod(object sender, EventArgs e)
        {
            string studentId = dropdown.SelectedItem.Value;
            DateTime date = calendar.SelectedDate;
            Response.Redirect("Graphs.aspx?stuid=" + studentId + "&date=" + date);
        }
    }
}