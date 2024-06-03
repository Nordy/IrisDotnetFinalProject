<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="JonathanNordmanProject.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BetterSchool</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Home.css" />
    <script src="js/Global.js"></script>
    <script src="js/Home.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%=title %>
        <div id="details" class="centered">
            <div class="det">
                <h2>What?</h2>
                <p>BetterSchool is an app that connects to Mashov and Shahaf to make it's own database of interconnected data that will improve your productivity.</p>
            </div>
            <div class="det">
                <h2>Why?</h2>
                <p>This is a final project in a computer science class.</p>
            </div>
            <div class="det">
                <h2>Who?</h2>
                <a class="centered" target="_blank" href="https://github.com/YonaNord">
                    <img src="images/gitLogo.jpeg"/>YonaNord
                </a>
            </div>
        </div>




    </div>

</body>
</html>
