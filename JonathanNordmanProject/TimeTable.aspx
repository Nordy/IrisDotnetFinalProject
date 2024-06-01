<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeTable.aspx.cs" Inherits="JonathanNordmanProject.TimeTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetterSchool</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/TimeTable.css" />
    <script src="js/Global.js"></script>
    <script src="js/TimeTable.js"></script>
</head>
<body>
    <div id="nav">
        <ul id="navList">
            <li><a href="Home" name="home" class="navButton">
                Home
            </a></li> 
            <li><a href="TimeTable" name="timetable" class="navButton navSelected">
                Time Table
            </a></li>
            <li><a href="Events" name="events" class="navButton">
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
        <div class="timetable">
            <h2>Weekly Timetable</h2>
            <table>
                <tr>
                    <th>Time</th>
                    <th>Sunday</th>
                    <th>Monday</th>
                    <th>Tuesday</th>
                    <th>Wednesday</th>
                    <th>Thursday</th>
                    <th>Friday</th>
                </tr>
                <tr>
                    <td>9:00 - 10:00</td>
                    <td>Math</td>
                    <td>English</td>
                    <td>History</td>
                    <td>Science</td>
                    <td>Art</td>
                </tr>
                <tr>
                    <td>10:00 - 11:00</td>
                    <td>Science</td>
                    <td>Math <br /><p class="changeSchedule">Canceled Lesson</p></td>
                    <td>English</td>
                    <td>History</td>
                    <td>Physical Education</td>
                </tr>
            </table>
        </div>
    </div>

</body>
</html>
