using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace TigerConnectDotNetConsoleApp
{
    class Program
    {
        private static string recentSentMsgId = null;

        static void Main(string[] args)
        {
            // Initialize the SDK with an API key and secret belonging to a specific user
            TT.Win.SDK.Global.Init(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

            // Subscribe to realtime events 
            TT.Win.SDK.Api.Events.MessageReceivedEvent += Events_MessageReceivedEvent;
            TT.Win.SDK.Api.Events.MessageStatusChangedEvent += Events_MessageStatusChangedEvent;
            TT.Win.SDK.Api.Events.StartListening();

            Console.WriteLine("Listening for incoming messages & status ...");
            Console.WriteLine("You can post message anytime by entering recipient, message and press <ENTER> (Format: recipientId;message)");
            Console.WriteLine("To exit anytime simply press <ENTER>");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["uploadFolder"]))
            {
                var fsWatcher = new FileSystemWatcher(ConfigurationManager.AppSettings["uploadFolder"]);
                fsWatcher.Created += FsWatcher_Created;
                fsWatcher.EnableRaisingEvents = true;
                Console.WriteLine("File watcher started at " + ConfigurationManager.AppSettings["uploadFolder"]);
            }

            StartSendMessenger();
        }

        private async static void FsWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["uploadFileRecipients"]))
            {
                var recipients = ConfigurationManager.AppSettings["uploadFileRecipients"].Split(';');
                var fileToAttach = new TT.Win.SDK.Model.AttachedFile();
                fileToAttach.FileContents = File.ReadAllBytes(e.FullPath);
                fileToAttach.FullFileName = e.Name;
                var filesToAttach = new List<TT.Win.SDK.Model.AttachedFile>();
                filesToAttach.Add(fileToAttach);
                foreach (var curRecipient in recipients)
                {
                    Console.WriteLine(string.Format("Sending file {0} to {1} ...", e.Name, curRecipient));
                    var messageSent = await TT.Win.SDK.Api.Message.SendMessageAsync("sending file from FileWatcher bot", curRecipient, filesToAttach);
                    Console.WriteLine(string.Format("File - {0} successfully sent to {1} (Message Id: {2})", e.Name, curRecipient, messageSent.message_id));
                }
            }
        }

        private static void StartSendMessenger()
        {

            while (true)
            {
                var input = Console.ReadLine();

                var arrInput = input.Split(';');

                if (arrInput.Length < 2)
                {
                    return;
                }

                try
                {
                    var messageSent = TT.Win.SDK.Api.Message.SendMessage(arrInput[1], arrInput[0]);

                    if (messageSent.message_id != null)
                    {
                        recentSentMsgId = messageSent.message_id;
                        Console.WriteLine("Message sent successfully - " + messageSent.message_id);
                    }
                    else
                    {
                        Console.WriteLine("Send Message failed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }
        private static void Events_MessageStatusChangedEvent(object sender, TT.Win.SDK.Events.MessageEventArgs e)
        {
            if (recentSentMsgId == e.MessageData.message_id)
            {
                Console.WriteLine(string.Format("Recent sent message status : {0}", e.MessageData.status));
            }
        }

        private async static void Events_MessageReceivedEvent(object sender, TT.Win.SDK.Events.MessageEventArgs e)
        {
            if (e.MessageData.sender_user == null)
            {
                e.MessageData.sender_user = await TT.Win.SDK.Api.User.GetUserAsync(e.MessageData.sender);
            }
            Console.WriteLine(string.Format("Message received from {0} : {1}", e.MessageData.sender_user.display_name, e.MessageData.body));

            if (e.MessageData.AttachmentsToDownload != null)
            {
                DownloadFile(e.MessageData);
            }
        }

        private static async void DownloadFile(TT.Win.SDK.Model.MessageEventData message)
        {
            var folderPath = Path.GetFullPath(ConfigurationManager.AppSettings["DownloadedFilePath"]);

            foreach (var attachment in message.AttachmentsToDownload)
            {
                var fileName = string.Format("{0}_{1}{2}", message.message_id, attachment.AttachmentSequence, attachment.FileExtension);
                Console.WriteLine("Downlading attached file " + fileName + " ...");
                var fileContents = await attachment.GetFileContents();
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, fileContents);
                Console.WriteLine("Downloaded the file at " + folderPath);
            }
        }
    }
}

