using BugzillaWebDriver.BaseClasses;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugzillaWebDriver.ComponentHelper
{
    public class NavigationHelper
    {
        public static void NavigateToUrl(string url)
        {
            // Console.WriteLine(url);
            // ObjectRepository.Driver.Navigate().GoToUrl(url);
            ObjectRepository.Driver.Navigate().GoToUrl("file:///home/vkrpk/RiderProjects/BWAPP_ATDD_NEW/Workshop%20not%C3%A9/Workshop.html");
        }

        public static void NavigateToHomePage()
        {
            // ObjectRepository.Driver.Navigate().GoToUrl(ObjectRepository.Config.GetWebsite());
            ObjectRepository.Driver.Navigate().GoToUrl("file:///home/vkrpk/RiderProjects/BWAPP_ATDD_NEW/Workshop%20not%C3%A9/Workshop.html");
        }

        public static void Logout()
        {
            if (PageHelper.GetPageTitle() != "bWAPP - Login")
            {
                LinkHelper.ClickLink(By.LinkText("Logout"));
                AlertHelper.Accept();
                Task.Delay(100).Wait();
            }
        }
    }
}
