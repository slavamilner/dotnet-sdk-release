<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TigerConnectAspNetWebApp.Search" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>TigerConnect asp.net Sample - Search</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin.css" rel="stylesheet">
    <link href="css/ttDemo.css" rel="stylesheet">
    <link href='https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,300,600,700' rel='stylesheet' type='text/css'>

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
                <a class="navbar-brand" href="default.aspx">TigerConnect Asp.net</a>
            </div>
            <ul class="nav navbar-right top-nav"></ul>
            <div class="collapse navbar-collapse navbar-ex1-collapse">
                <ul class="nav navbar-nav side-nav">
                    <li>
                        <a href="SendMessage.aspx"><i class="fa fa-fw fa-dashboard"></i>Message - Send</a>
                    </li>
                    <li>
                        <a href="MessageDetail.aspx"><i class="fa fa-fw fa-dashboard"></i>Message - Detail</a>
                    </li>
                    <li>
                        <a href="CreateGroup.aspx"><i class="fa fa-fw fa-dashboard"></i>Group - Create</a>
                    </li>
                    <li>
                        <a href="GroupDetail.aspx"><i class="fa fa-fw fa-dashboard"></i>Group - Detail</a>
                    </li>
                    <li class="active">
                        <a href="Search.aspx"><i class="fa fa-fw fa-dashboard"></i>Search</a>
                    </li>
                    <li>
                        <a href="Metadata.aspx"><i class="fa fa-fw fa-dashboard"></i>Metadata</a>
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
                                <h3 class="panel-title">Enter search parameters</h3>
                            </div>
                            <div class="panel-body">
                                <h4 class="panel-subheading">Query Configuration (all optional)</h4>
                            </div>
                            <div class="panel-body">
                                <form id="frmSearch" role="form" runat="server">
                                    <div class="form-group">
                                        <label>Entity type(s) to search</label>
                                        <asp:CheckBoxList ID="cblSearchTypes" runat="server">
                                            <asp:ListItem Text="Accounts" Value="account"></asp:ListItem>
                                            <asp:ListItem Text="Groups" Value="group"></asp:ListItem>
                                            <asp:ListItem Text="Distribution Lists" Value="distribution_list"></asp:ListItem>
                                            <asp:ListItem Text="Public Groups" Value="public_group"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="form-group">
                                        <label>Continuatation Token (optional for paging)</label>
                                        <asp:TextBox ID="txtContinuation" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Organizations to search (optional, comma-separated list of org tokens</label>
                                        <asp:TextBox ID="txtDirectory" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Display Name</label>
                                        <asp:TextBox ID="txtDisplayName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Execute Search" OnClick="btnSearch_Click"  />
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



