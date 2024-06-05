<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="BetterSchool.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/SignUp.css" />
    <link rel="icon" type="image/png" href="images/logo.png" />
    <script src="js/Global.js"></script>
    <script src="js/SignUp.js"></script>
</head>
<body>
    <%=navbar %>
    <div id="container">
        <%=title %>
        <form method="post" class="centered" action="SignUp.aspx">
            <div id="form">
                <h1>Sign Up</h1>
                <div class="parallel">
                    <div class="perpendicular">
                        <label>First Name:</label>
                        <input <%=(status ? "" : "style=\"border: solid 2px red\"")%> type="text" name="fname" placeholder="Type Here..." />
                    </div>
                    <div class="perpendicular">
                        <label>Last Name:</label>
                        <input <%=(status ? "" : "style=\"border: solid 2px red\"")%> type="text" name="lname" placeholder="Type Here..." />
                    </div>
                </div>
                <br />
                <div class="parallel">
                    <div class="perpendicular">
                        <label>Username:</label>
                        <input <%=(status ? "" : "style=\"border: solid 2px red\"")%> type="text" name="username" placeholder="Type Here..." />
                    </div>
                    <div class="perpendicular">
                        <label>Password:</label>
                        <input <%=(status ? "" : "style=\"border: solid 2px red\"")%> type="password" name="password" placeholder="Type Here..." />  
                    </div>
                </div>
                <br /> 
                <div class="perpendicular centered">
                    <label>Class:</label>
                    <div id="myMultiselect" class="multiselect">
                        <div id="mySelectLabel" class="selectBox" onclick="toggleCheckboxArea()">
                            <select <%=(status ? "" : "style=\"border: solid 2px red\"")%> class="form-select">
                              <option>Placeholder</option>
                            </select>
                            <div class="overSelect"></div>
                        </div>
                      <div id="mySelectOptions">
                           <%=classes %>
                      </div>
                    </div>
                </div>
                <h3><%=error %></h3>
                <br />
                <div class="signupContainer">
                    <input class="signupButton" type="submit" name="submit" value="Sign up"/>
                </div>
                <br />
                <a href="Login.aspx">Already have an account? <b>LOG IN HERE</b></a>
            </div>
        </form>
    </div>
</body>
</html>
