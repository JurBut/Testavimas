using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Testavimas.POM;

namespace Testavimas
{
    internal class TopMenu
     
    {
        IWebDriver driver;
        By searchBar = By.XPath("//span[@class='sn-suggest']");
        By inputSpace = By.XPath("//input[@type='search'][2]");
        By searchButton = By.XPath("//button[@class='headerSearchButton']");
        private string cartButtonXpath = "//div[@class='headerCart-wrapper']";

        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickOnCart()
        {
            GeneralMethods general = new GeneralMethods(driver);
            general.ClickElementIfExists("//button[contains(@class,'PopupCloseButton')]");
            //si apacioje idejau, nes nenorejau naudoti thread sleep. Jei neidedu, tada paspaudzia ant krepselio per greitai ir nerodo prekes krepselyje.
            IWebElement cart1 = general.WaitUntilElementExists("//div[@class='headerCart-amount']", driver);
            IWebElement cart = general.WaitUntilElementExists(cartButtonXpath, driver);
            cart.Click();
        }

        public void SearchForItem(string itemName)
        {           
            driver.FindElement(searchBar).Click();          
            driver.FindElement(inputSpace).SendKeys(itemName);            
            driver.FindElement(searchButton).Click();        
        }
    }
}
