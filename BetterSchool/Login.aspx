<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BetterSchool.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Login.css" />
    <link rel="icon" type="image/png" href="images/logo.png" />
    <script src="js/Global.js"></script>
    <script src="js/Login.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%=title %>
        <form method="post" class="centered" action="Login.aspx">
            <div id="form">
                <h1>Login</h1>
                <div class="parallel centered">
                    <div class="perpendicular">
                        <label>Username:</label>
                        <input <%=(status ? "" : "style=\"border: solid 2px red\"")%> type="text" name="username" placeholder="Type Here..." />
                    </div>
                    <div class="perpendicular">
                        <label>Password:</label>
                        <input <%=(status ? "" : "style=\"border: solid 2px red\"")%> type="password" name="password" placeholder="Type Here..." />  
                    </div>
                </div>
                <h3><%=error %></h3>
                <br /> 
                <div class="loginContainer">
                    <input class="loginButton" type="submit" name="submit" value="Log In" />
                </div>
                <br />
                <a href="SignUp.aspx">Don't have an account? <b>SIGN UP HERE</b></a>
            </div>
        </form>
    </div>
</body>
</html>
