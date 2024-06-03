<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JonathanNordmanProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link rel="stylesheet" type="text/css" href="css/Global.css" />
    <link rel="stylesheet" type="text/css" href="css/SignUp.css" />
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
                <label>First Name:</label>
                <input type="text" name="fname" placeholder="Type Here..." />
                <br />
                <label>Last Name:</label>
                <input type="text" name="lname" placeholder="Type Here..." />
                <br />
                <label>Gender:</label>
                <div id="gender">
                    <input type="radio" name="gender" value="male" />
                    <label for="genderMale">Male</label>
                    <br />
                    <input type="radio" name="gender" value="female" />
                    <label for="genderFemale">Female</label>
                </div>
                <div id="date">
                    <label>Birth date:</label>
                    <input type="date" name="date" />
                </div>

                <br />
                <label>Username:</label>
                <br />
                <input type="text" name="username" placeholder="Type Here..." />
                <br />
                <label>Password:</label>
                <br />
                <input type="password" name="password" placeholder="Type Here..." />    
                <br /> 
                <div class="signupContainer">
                    <input class="signupButton" type="button" name="submit" value="Sign up" />
                </div>
                <br />
                <a href="Login">Already have an account? <b>LOG IN HERE</b></a>
            </div>
        </form>
    </div>
</body>
</html>
