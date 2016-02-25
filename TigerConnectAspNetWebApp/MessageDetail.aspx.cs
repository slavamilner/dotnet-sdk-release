using System;
using System.Configuration;
using TT.Win.SDK;
using TT.Win.SDK.Model;
using Newtonsoft.Json;

namespace TigerConnectAspNetWebApp
{
    public partial class MessageDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.AppSettings;
            Global.Init(appSettings["apiKey"], appSettings["apiSecret"]);

            if (string.IsNullOrEmpty(Request.QueryString["message_id"]))
            {
                ShowResult(false, "Please pass a valid message_id value in the query string");
            }
            else
            {
                try
                {
                    MessageEventData message = TT.Win.SDK.Api.Message.GetMessage(Request.QueryString["message_id"]);
                    sMessageId.Text = message.message_id;
                    sSenderToken.Text = message.sender_token;
                    sRecipientToken.Text = message.recipient_token;
                    sMessageStatus.Text = message.status.Replace("New", "Sent");
                    txtMessageJson.Text = JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore});
                }
                catch (Exception ex)
                {
                    ShowResult(false, ex.Message);
                }
            }
        }

        protected void ShowResult(bool success, string message)
        {
            string cssClass = success ? "alert alert-success" : "alert alert-danger";
            sResults.Text = string.Format("<div class=\"{0}\">{1}</div>", cssClass, message);
        }
    }
}