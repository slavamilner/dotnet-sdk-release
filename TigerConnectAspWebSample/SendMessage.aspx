<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="SendMessage" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>TigerConnect asp.net Sample - Send Message</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin.css" rel="stylesheet">
    <link href="css/ttDemo.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div id="wrapper">

        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html">TigerConnect Asp.net Web Sample</a>
            </div>
            <ul class="nav navbar-right top-nav"></ul>
            <div class="collapse navbar-collapse navbar-ex1-collapse">
                <ul class="nav navbar-nav side-nav">
                    <li class="active">
                        <a href="SendMessage.aspx"><i class="fa fa-fw fa-dashboard"></i>Message - Send</a>
                    </li>
                    <li>
                        <a href="MessageStatus.aspx"><i class="fa fa-fw fa-dashboard"></i>Message - Status</a>
                    </li>
                </ul>
            </div>
        </nav>

        <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Literal runat="server" ID="sResults"></asp:Literal>
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Send A Message</h3>
                            </div>
                            <div class="panel-body">
                                <form id="frmSendMessage" role="form" runat="server">
                                    <div class="form-group">
                                        <label>Recipient (token, username, email, or phone#)</label>
                                        <asp:TextBox ID="txtRecipient" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Message Body</label>
                                        <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Organization Token (optional)</label>
                                        <asp:TextBox ID="txtOrg" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>File Attachment (optional)</label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                    <asp:Button ID="btnSend" runat="server" CssClass="btn btn-info" Text="Send Message" OnClick="btnSend_Click"  />
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- /#page-wrapper -->

    </div>
    <!-- /#wrapper -->

    <!-- jQuery -->
    <script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

</body>
</html>
