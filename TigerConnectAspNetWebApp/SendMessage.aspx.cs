using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TigerConnectAspNetWebApp
{
    public partial class SendMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TT.Win.SDK.Global.Init("key", "secret");
            TT.Win.SDK.Api.Message.SendMessage("body", "m");

        }
    }
}