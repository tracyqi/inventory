using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CrawlILSmart
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            //var host = new JobHost();
            //// The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();

            //CrawlILSmart();

            crawlPartsbase();
        }

        private static void crawlPartsbase()
        {
            IWebDriver driver = new InternetExplorerDriver(@"F:\IEDriverServer_x64_2.46.0");
            driver.Navigate().GoToUrl("http://www.partsbase.com/landing/login.asp");

            var usernameField = driver.FindElement(By.Name("username"));
            usernameField.SendKeys("tallaadmin");

            var passwdField = driver.FindElement(By.Name("password"));
            passwdField.SendKeys("Ta11a2015!");

            var submitBtn = driver.FindElement(By.XPath("//a[@class='btn btn-success' and @value='Log In']"));
            submitBtn.Click();


        }
        //tallaadmin
//Ta11a2015!

        private static void CrawlILSmart()
        {
            IWebDriver driver = new InternetExplorerDriver(@"F:\IEDriverServer_x64_2.46.0");
            driver.Navigate().GoToUrl("http://static.ilsmart.com/pages/ILSLogin2014.htm?TabId=56&amp;language=en-US");

            //<iframe src="http://static.ilsmart.com/pages/ILSLogin2014.htm?TabId=56&amp;language=en-US" frameborder="0" style="width: 620px; height: 100px;" scrolling="no"></iframe>
            // <input tabindex="3" class="btn btn-primary btn-sm enter" type="submit" alt="Enter" value="Enter">
            var usernameField = driver.FindElement(By.Name("username"));
            usernameField.SendKeys("c08hu01");

            var passwdField = driver.FindElement(By.Name("password"));
            passwdField.SendKeys("Ta11a2015%");

            var submitBtn = driver.FindElement(By.XPath("//input[@type='submit' and @value='Enter']"));
            submitBtn.Click();
        }
    }
}
