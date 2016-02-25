# TigerConnect .NET SDK

This repository contains binary distributions of .NET SDK released by [TigerConnect](http://tigertext.com/tigerconnect).

If you have any questions, comments, or issues related to this repository then please contact the team by emailing [tigerconnect@tigertext.com](mailto:tigerconnect@tigertext.com).

## Overview

The TigerConnect .NET SDK provides a simple way to enhance your .NET applications with intelligent, secure messaging from TigerConnect. The SDK is a .NET wrapper of the TigerConnect REST APIs to make things easier to incorporate into your Windows applications.  

In order to use the TigerConnect .NET SDK you must be a registered developer. All aspects of this setup are covered in detail in the [TigerConnect Documentation](https://tigerconnect.readme.io/).

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

This github repository contains a .NET solution file with 3 example projects, including a web application, a Windows console application, and a Windows Service.  You may clone the repo or download the zipped version in order to access and use the projects.  Once you open the solution in Visual Studio, you can build the solution in order to restore all NuGet references, and to run the example projects.  Simply right-click on the project of interest and set it as your startup project to debug the code.

You may also add the TigerConnect .NET SDK to your existing projects by making references to the required libraries:
1.  Newtonsoft.Json - You may download this file as a NuGet package, or make a file reference to the appropriate version in the /packages/Newtonsoft folder in the solution root directory.
2.  TigerText.Windows.SDK.dll - The TigerConnect SDK will be available as a NuGet package in the near future.  In the meantime, simply make a reference to the .dll that is in the /packages folder in the solution root directory.  

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
