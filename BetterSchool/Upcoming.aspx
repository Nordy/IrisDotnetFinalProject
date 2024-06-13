<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upcoming.aspx.cs" Inherits="BetterSchool.Upcoming" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetterSchool - Upcoming</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Upcoming.css" />
    <link rel="icon" type="image/png" href="images/logo.png" />
    <script src="js/Global.js"></script>
    <script src="js/Upcoming.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%=title %>
        <% for (int i = 0; i < upcoming.Count; i++)
            { %>
                <div class="upcomingContainer parallel centered">
                    <h2 class="date"><%= upcoming[i]["date"]%></h2>
                    <div class="perpendicular">
                        <h2><%= upcoming[i]["subject"]%></h2>
                        <p><%= upcoming[i]["task"]%></p>
                    </div>
                </div>
        <%  } %>
    </div>

</body>
</html>
