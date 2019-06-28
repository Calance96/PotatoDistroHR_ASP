<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Potato_Distro_HRM__Web_.LoginForm" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="./resources/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="./resources/css/jquery-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="./resources/css/jquery-ui.structure.min.css" />
    <link rel="stylesheet" type="text/css" href="./resources/css/jquery-ui.theme.min.css" />
    <link rel="stylesheet" type="text/css" href="./resources/css/all.min.css" />
    <script type="text/javascript" src="./resources/js/jquery-3.4.0.min.js" ></script>
    <script type="text/javascript" src="./resources/js/bootstrap.min.js" ></script>
    <script type="text/javascript" src="./resources/js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="./resources/js/all.min.js"></script>
    <style>
        .cell{
            padding:10px;
        }
        .register-photo {
            /*background-color:#4D6D9A;*/
          background:#f1f7fc;
          padding:80px 0;
          height:100vh;
        }

        .register-photo .image-holder {
          display:table-cell;
          width:60%;
          vertical-align:middle;
          background-color:#4D6D9A;
          /*background:url(images/PotatoLogo.png);
          background-size:cover;*/
        }

        .register-photo .form-container {
          display:table;
          max-width:900px;
          width:90%;
          margin:0 auto;
          box-shadow:1px 1px 5px rgba(0,0,0,0.1);
        }

        .register-photo .form {
          display:table-cell;
          width:400px;
          background-color:white;
          padding:40px 60px;
          color:#505e6c;
        }

        @media (max-width:991px) {
          .register-photo .form {
            padding:40px;
          }
        }

        .register-photo .form h2 {
          font-size:24px;
          line-height:1.5;
          margin-bottom:30px;
        }

        .register-photo .form .form-control {
          background:#f7f9fc;
          border:none;
          border-bottom:1px solid #dfe7f1;
          border-radius:0;
          box-shadow:none;
          outline:none;
          color:inherit;
          text-indent:6px;
          height:40px;
        }

        .register-photo .form .form-check {
          font-size:13px;
          line-height:20px;
        }

        .register-photo .form .btn-primary {
          background:#f4476b;
          border:none;
          border-radius:4px;
          padding:11px;
          box-shadow:none;
          margin-top:35px;
          text-shadow:none;
          outline:none !important;
        }

        .register-photo .form .btn-primary:hover, .register-photo form .btn-primary:active {
          background:#eb3b60;
        }

        .register-photo .form .btn-primary:active {
          transform:translateY(1px);
        }

        .register-photo .form .already {
          display:block;
          text-align:center;
          font-size:12px;
          color:#6f7a85;
          opacity:0.9;
          text-decoration:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-photo">
            <div class="form-container">
                <div class="image-holder">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/resources/images/PotatoLogo.png" Width="100%" />
                </div>
                <div class="form">
                    <h2 class="text-center"><strong>Login</strong></h2>
                    <div class="form-group">
                        <asp:TextBox ID="IdInput" runat="server" CssClass="form-control" placeholder="Employee ID"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" CssClass="text-warning" ControlToValidate="IdInput"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="PwdInput" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" CssClass="text-warning" ControlToValidate="PwdInput"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="ModeInput" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LoginLbl" runat="server" CssClass="form-check-label text-danger"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" CssClass="btn btn-primary btn-block"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>