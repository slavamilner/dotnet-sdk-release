using System;
using System.Linq;
using System.Configuration;
using TT.Win.SDK;

namespace TigerConnectAspNetWebApp
{
    public partial class CreateGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // Initialize the SDK using the API key and secret in the web.config file
            var appSettings = ConfigurationManager.AppSettings;
            Global.Init(appSettings["apiKey"], appSettings["apiSecret"]);

            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    ShowResult(false, "Please enter a name for your group.");
                }
                else
                {
                    TT.Win.SDK.Model.GroupEventData group = new TT.Win.SDK.Model.GroupEventData()
                    {
                        name = txtName.Text,
                        description = txtDescription.Text,
                        replay_history = chkReplay.Checked,
                        members = string.IsNullOrEmpty(txtMembers.Text) ? null : txtMembers.Text.Split(',').ToList<string>()
                    };
                    string token = TT.Win.SDK.Api.Group.CreateGroup(group);

                    if (!string.IsNullOrEmpty(token))
                    {
                        ShowResult(true, "Group created successfully.  Group token is " + token + ".  <a href=\"GroupDetail.aspx?token=" + token + "\">Click here</a> to view group detail.");
                    }
                    else
                    {
                        ShowResult(false, "Send Message failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowResult(false, ex.Message);
            }
        }

        protected void ShowResult(bool success, string message)
        {
            string cssClass = success ? "alert alert-success" : "alert alert-danger";
            sResults.Text = string.Format("<div class=\"{0}\">{1}</div>", cssClass, message);
        }
    }
}