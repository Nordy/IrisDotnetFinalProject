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
        public static string Navbar(bool isLoggedIn, string currentPage, bool isAdmin, string fname = null)
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
                        <div class=""fa fa-home""></div>
                        Dashboard
                    </a></li>
                    <li><a href=""Schedule"" name=""schedule"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        <div class=""fa fa-calendar""></div>
                        Schedule
                    </a></li>
                    <li><a href=""Upcoming"" name=""upcoming"" class=""navButton {(currentPage == "Upcoming" ? "navSelected" : "")}"">
                        <div class=""fa fa-bookmark""></div>
                        Upcoming
                    </a></li>
                    <li><a href=""Grades"" name=""grades"" class=""navButton {(currentPage == "Grades" ? "navSelected" : "")}"">
                        <div class=""fa fa-book""></div>
                        Grades
                    </a></li>
                    {(isAdmin ? $@"
                        <li><a href=""Admin"" name=""admin"" class=""navButton {(currentPage == "Admin" ? "navSelected" : "")}"">
                            <div class=""fa fa-lock""></div>
                            Admin Panel
                        </a></li>" 
                    : "")}
                    <div class=""navRight"">
                        <li><a href=""Profile"" name=""profile"" class=""navExtraButton"" data-text=""Profile"">
                            <div class=""fa fa-user""></div>
                            {fname}
                        </a></li>
                    </div>
                    ";
            } else
            {
                element += $@"
                    <li><a href=""Home"" name=""home"" class=""navButton {(currentPage == "Home" ? "navSelected" : "")}"">
                        <div class=""fa fa-home""></div>
                        Home
                    </a></li>
                    <li><a href=""Schedule"" name=""schedule"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        <div class=""fa fa-calendar""></div>
                        Schedule
                    </a></li>
                    <div class=""navRight"">
                        <li><a href=""Login"" name=""login"" class=""navExtraButton"">
                            <div class=""fa fa-user""></div>
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