<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="JonathanNordmanProject.Events" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Events</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Events.css" />
    <script src="js/Global.js"></script>
    <script src="js/Events.js"></script>
</head>
<body>
    <div id="nav">
        <ul id="navList">
            <li><a href="Home" name="home" class="navButton">
                Home
            </a></li> 
            <li><a href="TimeTable" name="timetable" class="navButton">
                Time Table
            </a></li>
            <li><a href="Events" name="events" class="navButton navSelected">
                Events
            </a></li>
        </ul>
    </div>
    <div id="container">
        <div id="title" class="centered">
            <img id="logo" src="images/logo-white.png" />
            <h1 id="dot">•</h1>
            <h1>Your Better <br /> School <i>Experience.</i></h1>
        </div>
        <div class="homework-container">
            <h2>Events</h2>
            <div class="homework-item">
                <h3>Chapter 4 Exercises</h3>
                <p>Subject: Math</p>
                <p>Type: Quiz</p>
                <p>Due Date: April 10, 2024 (In 12 hours)</p>
            </div>
            <div class="homework-item">
                <h3>Essay on "Modern Literature"</h3>
                <p>Subject: English</p>
                <p>Type: Project</p>
                <p>Due Date: April 12, 2024 (In 1 days)</p>
            </div>
            <div class="homework-item">
                <h3>Research Paper on World War II</h3>
                <p>Subject: History</p>
                <p>Type: Presentation</p>
                <p>Due Date: April 15, 2024 (In 4 days)</p>
            </div>
        </div>
    </div>
</body>
</html>
