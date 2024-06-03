﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JonathanNordmanProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Login.css" />
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
                <label>Username:</label>
                <br />
                <input type="text" name="username" placeholder="Type Here..." />
                <br />
                <br />
                <label>Password:</label>
                <br />
                <input type="password" name="password" placeholder="Type Here..." />    
                <br /> 
                <div class="loginContainer">
                    <input class="loginButton" type="button" name="submit" value="Log In" />
                </div>
                <br />
                <a href="SignUp">Don't have an account? <b>SIGN UP HERE</b></a>
            </div>
        </form>
    </div>
</body>
</html>
