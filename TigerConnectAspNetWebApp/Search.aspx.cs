using System;
using System.Linq;
using System.Configuration;
using TT.Win.SDK;

namespace TigerConnectAspNetWebApp
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Initialize the SDK using the API key and secret in the web.config file
            var appSettings = ConfigurationManager.AppSettings;
            Global.Init(appSettings["apiKey"], appSettings["apiSecret"]);

            try
            {
                TT.Win.SDK.Model.SearchParams searchParams = new TT.Win.SDK.Model.SearchParams()
                {
                    continuation = txtContinuation.Text,
                    directory = string.IsNullOrEmpty(txtDirectory.Text) ? null : txtDirectory.Text.Split(',').ToList<string>(),
                    display_name = txtDisplayName.Text
                };
                TT.Win.SDK.Model.SearchReplyData searchReplyData = TT.Win.SDK.Api.Search.Execute(searchParams);

                if (searchReplyData.results.Count() > 0)
                {
                    ShowResult(true, "Matching results: " + searchReplyData.results.Count().ToString());
                }
                else
                {
                    ShowResult(false, "No matching search results found.");
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