using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterSchool
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
                    <li><a href=""Dashboard.aspx"" class=""navButton {(currentPage == "Dashboard" ? "navSelected" : "")}"">
                        <div class=""fa fa-home""></div>
                        Dashboard
                    </a></li>
                    <li><a href=""Schedule.aspx"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        <div class=""fa fa-calendar""></div>
                        Schedule
                    </a></li>
                    <li><a href=""Upcoming.aspx"" class=""navButton {(currentPage == "Upcoming" ? "navSelected" : "")}"">
                        <div class=""fa fa-bookmark""></div>
                        Upcoming
                    </a></li>
                    <li><a href=""Grades.aspx"" class=""navButton {(currentPage == "Grades" ? "navSelected" : "")}"">
                        <div class=""fa fa-book""></div>
                        Grades
                    </a></li>
                    {(isAdmin ? $@"
                        <li><a href=""Admin.aspx"" class=""navButton {(currentPage == "Admin" ? "navSelected" : "")}"">
                            <div class=""fa fa-lock""></div>
                            Admin Panel
                        </a></li>"
                    : "")}
                    <div class=""navRight"">
                        <li>
                            <div class=""navExtraButton navExtraButtonLoggedIn"">
                                <div class=""fa fa-user""></div>
                                {fname}
                                <div class=""dropdownContainer"">
                                    <div class=""dropdown"">
                                        <a href=""Update.aspx"">
                                            <div class=""fa fa-pencil""></div>
                                            Update Profile
                                        </a>
                                        <a href=""Logout.aspx"">
                                            <div class=""fa fa-sign-out""></div>
                                            Log Out
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </div>
                    ";
            }
            else
            {
                element += $@"
                    <li><a href=""Home.aspx"" class=""navButton {(currentPage == "Home" ? "navSelected" : "")}"">
                        <div class=""fa fa-home""></div>
                        Home
                    </a></li>
                    <li><a href=""Schedule.aspx"" class=""navButton {(currentPage == "Schedule" ? "navSelected" : "")}"">
                        <div class=""fa fa-calendar""></div>
                        Schedule
                    </a></li>
                    <div class=""navRight"">
                        <li><a href=""Login.aspx"" class=""navExtraButton"">
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