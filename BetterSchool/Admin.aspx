<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="BetterSchool.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetterSchool - Admin Panel</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Admin.css" />
    <link rel="icon" type="image/png" href="images/logo.png" />
    <script src="js/Global.js"></script>
    <script src="js/Admin.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%=title %>
        <table class="centered">
            <tr>
                <th>Username</th>
                <th>Password</th>
                <th>MashovId</th>
                <th>MashovPassword</th>
                <th>Fname</th>
                <th>Lname</th>
                <th>IsAdmin</th>
                <th colspan="2" style="border:none;">&nbsp;</th>
            </tr>
            <%=users %>
        </table>
    </div>
</body>
</html>
