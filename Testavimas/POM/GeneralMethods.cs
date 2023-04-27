using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium.Interactions;

namespace Testavimas.POM
{
    internal class GeneralMethods
    {
        IWebDriver driver;

        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement WaitUntilElementExists(string xPath, IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(70));
            wait.PollingInterval = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(d => d.FindElement(By.XPath(xPath)));
        }

        public void ScrollToElement(string xpath)
        {
            IWebElement el = driver.FindElement(By.XPath(xpath));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", el);

        }

        public void ClickElementIfExists(string xPath)
        {           
            try
            {
                By element = By.XPath(xPath);
                driver.FindElement(element).Click();
            }
            catch (NoSuchElementException ex)
            {                
                Console.WriteLine(ex.ToString());
            }
        }
        public void MoveToElement(string xPath)
        {
            IWebElement element = driver.FindElement(By.XPath(xPath));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }
    }
}
