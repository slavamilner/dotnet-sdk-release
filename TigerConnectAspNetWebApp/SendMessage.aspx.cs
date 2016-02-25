using System.Collections.Generic;
using System;
using System.Configuration;
using TT.Win.SDK;
using TT.Win.SDK.Api;
using TT.Win.SDK.Model;

namespace TigerConnectAspNetWebApp
{
    public partial class SendMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Initialize the SDK using the API key and secret in the web.config file
            var appSettings = ConfigurationManager.AppSettings;
            Global.Init(appSettings["apiKey"], appSettings["apiSecret"]);

            List<AttachedFile> filesToAttach = null;

            if (FileUpload1.HasFile)
            {
                AttachedFile fileToAttach = new AttachedFile();
                fileToAttach.FileContents = FileUpload1.FileBytes;
                fileToAttach.FullFileName = FileUpload1.FileName;
                filesToAttach = new List<AttachedFile>();
                filesToAttach.Add(fileToAttach);
            }

            try
            {
                TT.Win.SDK.Model.MessageEventData messageSent = null;
                if (string.IsNullOrEmpty(txtOrg.Text))
                {
                    messageSent = Message.SendMessage(this.txtMessage.Text, this.txtRecipient.Text, filesToAttach);
                }
                else
                {
                    messageSent = Message.SendMessage(this.txtMessage.Text, this.txtRecipient.Text, txtOrg.Text, filesToAttach);
                }

                if (messageSent.message_id != null)
                {
                    ShowResult(true, "Message sent successfully.  MessageId is " + messageSent.message_id + ".  <a href=\"MessageDetail.aspx?message_id=" + messageSent.message_id + "\">Click here</a> to view message detail and status.");
                }
                else
                {
                    ShowResult(false, "Send Message failed.");
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