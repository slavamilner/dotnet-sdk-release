using System;
using System.Net;
using System.ServiceProcess;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;

namespace TigerConnectDotNetWindowsService
{
    public class MessageReceiver : ServiceBase
    {
        private EventLog eventLog1;

        public MessageReceiver()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                base.OnStart(args);

                // Retrieve key and secret
                string apiKey = ConfigurationManager.AppSettings.Get("apiKey");
                string apiSecret = ConfigurationManager.AppSettings.Get("apiSecret");
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                {
                    throw new Exception("Invalid api key and/or secret.  Please enter a valid key and secret in the config file");          
                }

                TT.Win.SDK.Global.Init(apiKey, apiSecret);
                TT.Win.SDK.Api.Events.MessageReceivedEvent += Events_MessageReceivedEvent;
                TT.Win.SDK.Api.Events.StartListening();

                eventLog1.WriteEntry("The TigerConnect Message Receiver service has started.  MessageReceiver is listening for configured key " + apiKey);
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            eventLog1.WriteEntry("The TigerConnect Message Receiver service is terminating.  The background timer has been stopped and disposed.");
        }

        private async void Events_MessageReceivedEvent(object sender, TT.Win.SDK.Events.MessageEventArgs e)
        {
            try
            {
                // Retrieve the token of the user who sent the message, and the messageId
                string senderToken = string.IsNullOrEmpty(e.MessageData.sender) ? e.MessageData.sender_token : e.MessageData.sender;
                string messageId = string.IsNullOrEmpty(e.MessageData.message_id) ? e.MessageData.client_id : e.MessageData.message_id;

                // Retrieve body of the message.  For security reasons, this will not be logged below.
                string body = e.MessageData.body;

                if (string.IsNullOrEmpty(e.MessageData.group_token))
                {
                    // This was an individual (P2P) message from the sender to the recipient
                    eventLog1.WriteEntry(string.Format("P2P message received from sender token {0}.  MessageId is {1}.", senderToken, messageId), EventLogEntryType.Information);
                }
                else
                {
                    // This is a message to a group that the user defined by the API key and secret belongs to
                    string groupToken = e.MessageData.group_token;
                    string groupMessageLog = string.Format("Group message received from sender token {0}.  MessageId is {1}.  Group token is {2}.", senderToken, messageId, groupToken);

                    // Retrieve metadata associated with the group
                    // This allows a group conversation to tie back to another system, such as a patient record in an EMR
                    try
                    {
                        Dictionary<string, string> metadata = await TT.Win.SDK.Api.Metadata.GetMetadataAsync(groupToken);
                        if (metadata != null && metadata.Count > 0)
                        {
                            groupMessageLog += "\r\nMetadata associated with this group:";
                            foreach (string key in metadata.Keys)
                            {
                                groupMessageLog += string.Format("\r\n{0}: {1}", key, metadata[key]);
                            }
                        }
                        else
                        {
                            groupMessageLog += "\r\nNo Metadata associated with this group";
                        }
                    }
                    catch (Exception ex)
                    {
                        groupMessageLog += "\r\nGroup Metadata not available:  " + ex.Message;
                    }

                    eventLog1.WriteEntry(groupMessageLog, EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        private void InitializeComponent()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // eventLog1
            // 
            this.eventLog1.Log = "Application";
            this.eventLog1.Source = "TigerConnect Message Receiver";
            // 
            // MessageReceiver
            // 
            this.ServiceName = "TigerConnect Message Receiver";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }
    }
}
