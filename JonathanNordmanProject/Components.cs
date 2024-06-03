using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JonathanNordmanProject
{
    public static class Components
    {
        public static string Navbar(bool isLoggedIn, string currentPage)
        {
            string element = $@"
                <div id=""nav"">
                    <img src=""images/logo.png"" />
                    <ul>
                ";
            if (isLoggedIn)
            {
                element += $@"
                    <li><a href=""Dashboard"" name=""dashboard"" class=""navButton {(currentPage == "Dashboard" ? "navSelected" : "")}"">
                        <img name=""dashboard"" class=""navImg {(currentPage == "Dashboard" ? "navSelected" : "")}"" src=""images/home{(currentPage == "Dashboard" ? "-selected" : "")}.png"" />
                        Dashboard
                    </a></li>
                    <li><a href=""Grades"" name=""grades"" class=""navButton {(currentPage == "Grades" ? "navSelected" : "")}"">
                        <img name=""grades"" class=""navImg {(currentPage == "Grades" ? "navSelected" : "")}"" src=""images/grades{(currentPage == "Grades" ? "-selected" : "")}.png"" />
                        Grades
                    </a></li>
                    <li><a href=""Upcoming"" name=""upcoming"" class=""navButton {(currentPage == "Upcoming" ? "navSelected" : "")}"">
                        <img name=""upcoming"" class=""navImg {(currentPage == "Upcoming" ? "navSelected" : "")}"" src=""images/upcoming{(currentPage == "Upcoming" ? "-selected" : "")}.png"" />
                        Upcoming
                    </a></li>
                    <li><a href=""Schedule"" name=""schedule"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        <img name=""schedule"" class=""navImg {(currentPage == "Schedule" ? "navSelected" : "")}"" src=""images/schedule{(currentPage == "Schedule" ? "-selected" : "")}.png"" />
                        Schedule
                    </a></li>
                    <div class=""navRight"">
                        <li><a href=""Profile"" name=""profile"" class=""navExtraButton {(currentPage == "Profile" ? "navSelected" : "")}"">
                            Profile
                        </a></li>
                    </div>
                    ";
            } else
            {
                element += $@"
                    <li><a href=""Home"" name=""home"" class=""navButton {(currentPage == "Home" ? "navSelected" : "")}"">
                        <img name=""home"" class=""navImg {(currentPage == "Home" ? "navSelected" : "")}"" src=""images/home{(currentPage == "Home" ? "-selected" : "")}.png"" />
                        Home
                    </a></li>
                    <li><a href=""Schedule"" name=""schedule"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        <img name=""schedule"" class=""navImg {(currentPage == "Schedule" ? "navSelected" : "")}"" src=""images/schedule{(currentPage == "Schedule" ? "-selected" : "")}.png"" />
                        Schedule
                    </a></li>
                    <div class=""navRight"">
                        <li><a href=""Login"" name=""login"" class=""navExtraButton {(currentPage == "Login" ? "navSelected" : "")}"">
                            Login
                        </a></li>
                    </div>
                    ";
            }
            element += "</ul></div>";
            return element;
        }
    }
}