<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="BetterSchool.Admin" %>

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
        <form class="centered" runat="server" style="display:flex;gap: 2px;">
            <asp:Button runat="server" text="Update Schedules" OnClick="UpdateSchedules" />
            <asp:Button runat="server" text="Clean Schedules" OnClick="CleanSchedules" />
            <asp:Button runat="server" text="Update Changes" OnClick="UpdateChanges" />
            <asp:Button runat="server" text="Clean Grades" OnClick="CleanGrades" />
            <asp:Button runat="server" text="Update Grades" OnClick="UpdateGrades" />
            <asp:Button runat="server" text="Clean Upcoming" OnClick="CleanUpcoming" />
            <asp:Button runat="server" text="Update Upcoming" OnClick="UpdateUpcoming" />
        </form>
        <div style="display:flex">
            <div id="searchContainer">
                <form class="search" method="post" action="Admin.aspx">
                    <input type="text" placeholder="Type here..." name="fname" />
                    <input type="submit" name="submit" value="Search fname" />
                </form>
                <form class="search" method="post" action="Admin.aspx">
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
                    <input type="submit" name="submit" value="Search class" />
                </form>
                <form class="search" method="post" action="Admin.aspx">
                    <input type="submit" name="submit" value="Clear Search" />
                </form>
            </div>
        </div>

        <table class="centered">
            <tr>
                <th>Username</th>
                <th>Password</th>
                <th>MashovId</th>
                <th>MashovPassword</th>
                <th>Fname</th>
                <th>Lname</th>
                <th>Class</th>
                <th>IsAdmin</th>
                <th colspan="2" style="border:none;">&nbsp;</th>
            </tr>
            <%=users %>
        </table>
    </div>
</body>
</html>
