<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="JonathanNordmanProject.Scripts.WebForm1" %>

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
    <div id="nav">
        <ul id="navList">
            <li><a href="Home" name="home" class="navButton navSelected">
                Home
            </a></li> 
            <li><a href="TimeTable" name="timetable" class="navButton">
                Time Table
            </a></li>
            <li><a href="Events" name="events" class="navButton">
                Events
            </a></li>
        </ul>
    </div>
    <!--
    <div id="nav">
        <ul id="navList">
            <li><a href="Home" name="home" class="navButton navSelected">
                <img name="home" class="navImg navSelected" src="images/home-selected-white.png" />
                Home
            </a></li>
            <li><a href="Login" class="navButton">Login</a></li>
        </ul>
    </div>
    -->
    <div id="container">
        <div id="title" class="centered">
            <img id="logo" src="images/logo-white.png" />
            <h1 id="dot">•</h1>
            <h1>Your Better <br /> School <i>Experience.</i></h1>
        </div>
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
