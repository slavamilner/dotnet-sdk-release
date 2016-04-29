# TigerConnect .NET SDK

This repository contains binary distributions of .NET SDK released by [TigerConnect](http://tigertext.com/tigerconnect).

## Overview

The TigerConnect .NET SDK provides a simple way to enhance your .NET applications with intelligent, secure messaging from TigerConnect. The SDK is a .NET wrapper of the TigerConnect REST APIs to make things easier to incorporate into your Windows applications.  

This github repository contains a .NET solution file with 3 example projects, including a web application, a Windows console application, and a Windows Service. 

In order to use the TigerConnect .NET SDK you must be a registered developer. All aspects of this setup are covered in detail in the [TigerConnect Documentation](https://developer.tigertext.com/).

The .NET SDK is available to use with the following platforms/frameworks:
* .NET Framework 4.0 or higher (backward compatible up to Windows XP & Windows Server 2003)
* ASP.NET 4 or higher
* Silverlight 5
* Windows 8/8.1 (Store apps)
* Windows Phone 8.1
* Windows Phone Silverlight 8
* Xamarin.Android
* Xamarin.iOS
* Xamarin.iOS (Classic)

## Installation

To get started, you can clone the repository or download the zipped version in order to access and use the projects. Once you open the solution in Visual Studio, you can build the solution in order to restore all NuGet references, and to run the example projects. Simply right-click on the project of interest and set it as your startup project to debug the code.  Please see the sections below for additional notes on each sample project type, and for specific instructions on installing the Windows service provided as an example.

### TigerConnectAspNetWebApp

This is a traditional web application project using web forms (not MVC).  The default page offers a quick overview of the site and usage details.  Each webpage gives an example of a single API endpoint.  The UI offers a form to collect parameters, and the callback will use the .NET SDK to make a request to the TigerConnect REST API.  Please note that this site does not support any type of session management.  You simply add the API key and secret in the web.config file, and that is used by each form to authenticate with the SDK. 
### TigerConnectDotNetConsoleApp

This is a simple console application that demonstrates sending a message, as well as receiving events from the API.  There is a basic file watcher implementation that processes a queue folder, and can send any files dropped in that folder as an attachment in a TigerText message.  The console application is a great way to quickly test and debug code using the TigerConnect SDK.

### TigerConnectDotNetWindowsService

You should use this project type to develop any "bot" technology using the TigerConnect .NET SDK.  This template allows you to receive events, including new messages, that are sent to the user defined by the key an secret in the app.config file.  ONce you have set your key and secret and built the project in Visual Studio, you may install it as a service using the InstallUtil.exe file, which you can find in your <SYSTEMDRIVE>\Windows\Microsoft.Net directory.  Here is an example of installing the service from the command line:
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installutil.exe C:\Users\admin\Downloads\dotnet-sdk-release-master\dotnet-sdk-release-master\TigerConnectDotNetWindowsService\bin\Debug\TigerConnectDotNetWindowsService.exe

The service will write metadata to the Application Log in the Event Viewer for each message received.  Additionally, for group messages, any metadata associated with the group will also be written to the log.


###NuGet
You may also add the TigerConnect .NET SDK to your existing projects by making references to the required libraries:
* Newtonsoft.Json - You may download this file as a NuGet package, or make a file reference to the appropriate version in the /packages/Newtonsoft folder in the solution root directory.
* TigerText.Windows.SDK.dll - The TigerConnect SDK will be available as a NuGet package in the near future.  In the meantime, simply make a reference to the .dll that is in the /packages folder in the solution root directory.  

## Quick Example

```C#
// Initialize the SDK with an API key and secret belonging to a specific user
TT.Win.SDK.Global.Init(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

// Subscribe to realtime events 
TT.Win.SDK.Api.Events.MessageReceivedEvent += Events_MessageReceivedEvent;
TT.Win.SDK.Api.Events.StartListening();

// Send message from the user
TT.Win.SDK.Model.MessageEventData sentMessage = TT.Win.SDK.Api.Message.SendMessage("Test message", "testUser@mydomain.com");

// Process messages sent to the user (delivered as Server Sent Events)
private async static void Events_MessageReceivedEvent(object sender, TT.Win.SDK.Events.MessageEventArgs e)
{
	if (e.MessageData.sender_user == null)
	{
		e.MessageData.sender_user = await TT.Win.SDK.Api.User.GetUserAsync(e.MessageData.sender);
	}
	string result = string.Format("Message received from {0} : {1}", e.MessageData.sender_user.display_name, e.MessageData.body));
}

```

## Contact

If you have any questions please reach out to the TigerConnect team any time by emailing [developersupport@tigertext.com](mailto:developersupport@tigertext.com).

## License

TigerConnect is licensed under our [Developer Terms of Use](https://developer.tigertext.com/developer-terms-of-use/).
