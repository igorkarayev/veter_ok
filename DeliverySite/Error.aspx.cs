using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery
{
    public partial class Error : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null)
            {
                var id = Convert.ToInt32(Request.Params["id"]);
                if (id == 1)
                {
                    lblErrorText.Text = "У вас не достаточно прав!";
                }
            }
        }
    }
}