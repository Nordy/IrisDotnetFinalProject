<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="BetterSchool.Schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetterSchool - Schedule</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Schedule.css" />
    <link rel="icon" type="image/png" href="images/logo.png" />
    <script src="js/Global.js"></script>
    <script src="js/Schedule.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%if (Session["isLoggedIn"] == null)
            { %>
            <form class="centered" method="post" action="Schedule.aspx">
                <div id="myMultiselect" class="multiselect">
                    <div id="mySelectLabel" class="selectBox" onclick="toggleCheckboxArea()">
                        <select class="form-select">
                            <option>Placeholder</option>
                        </select>
                        <div class="overSelect"></div>
                    </div>
                    <div id="mySelectOptions">
                        <%=classes %>
                    </div>
                </div>
                <input type="submit" name="submit" value="Submit" />
            </form>
        <%} else
            { %>
                <h1 class="centered"><%=currentGrade %> Schedule</h1>
        <% } %>
        
        <% if (showSchedule)
           { %>
            
            <table class="centered">
                <tr>
                    <th style="border:none;">&nbsp;</th>
                    <th>Sunday</th>
                    <th>Monday</th>
                    <th>Tuesday</th>
                    <th>Wednesday</th>
                    <th>Thursday</th>
                    <th>Friday</th>
                </tr>
                <% for (int lesson = 0; lesson < 13; lesson++)
                   { %>
                        <tr>
                            <th>
                                <h2><%=lesson %></h2>
                                <p><%=times[lesson] %></p>
                            </th>
                            <% for (int day = 1; day < 7; day++)
                               { %>
                                    <td><%=schedule[day, lesson] %></td>
                            <% } %>
                        </tr>
                <% } %>

                
            </table>
        <% } %>
    </div>
</body>
</html>
