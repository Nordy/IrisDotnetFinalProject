using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JonathanNordmanProject
{
    public static class Components
    {

        public static string Title()
        {
            return @"
                <div id=""title"" class=""centered"">
                    <img id=""logo"" src=""images/logo.png"" />
                    <h1 id=""dot"">•</h1>
                    <h1>Your Better <br /> School <i>Experience.</i></h1>
                </div>
            ";
        }
        public static string Navbar(bool isLoggedIn, string currentPage)
        {
            string element = $@"
                <div id=""nav"">
                    <ul>
                        <img src=""images/logo.png"" />
                ";
            if (isLoggedIn)
            {
                element += $@"
                    <li><a href=""Dashboard"" name=""dashboard"" class=""navButton {(currentPage == "Dashboard" ? "navSelected" : "")}"">
                        Dashboard
                    </a></li>
                    <li><a href=""Grades"" name=""grades"" class=""navButton {(currentPage == "Grades" ? "navSelected" : "")}"">
                        Grades
                    </a></li>
                    <li><a href=""Upcoming"" name=""upcoming"" class=""navButton {(currentPage == "Upcoming" ? "navSelected" : "")}"">
                        Upcoming
                    </a></li>
                    <li><a href=""Schedule"" name=""schedule"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        Schedule
                    </a></li>
                    <div class=""navRight"">
                        <li><a href=""Profile"" name=""profile"" class=""navExtraButton"" data-text=""Profile"">
                            Profile
                        </a></li>
                    </div>
                    ";
            } else
            {
                element += $@"
                    <li><a href=""Home"" name=""home"" class=""navButton {(currentPage == "Home" ? "navSelected" : "")}"">
                        Home
                    </a></li>
                    <li><a href=""Schedule"" name=""schedule"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        Schedule
                    </a></li>
                    <div class=""navRight"">
                        <li><a href=""Login"" name=""login"" class=""navExtraButton"">
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