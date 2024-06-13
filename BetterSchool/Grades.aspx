<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="BetterSchool.Grades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetterSchool - Grades</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/Grades.css" />
    <link rel="icon" type="image/png" href="images/logo.png" />
    <script src="js/Global.js"></script>
    <script src="js/Grades.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%=title %>
        <% for (int i = 0; i < grades.Count; i++)
            { %>
                <div class="gradeContainer parallel centered">
                    <h2 class="finalGrade"><%= grades[i]["grade"]%></h2>
                    <div class="perpendicular">
                        <h2><%= grades[i]["subject"]%></h2>
                        <p><%= grades[i]["teacher"]%></p>
                        <p><%= grades[i]["date"]%></p>
                    </div>
                </div>
        <%  } %>
    </div>

</body>
</html>
