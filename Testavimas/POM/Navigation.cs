using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Xml.Linq;
using Testavimas.POM;

namespace Testavimas
{

    public class Navigation
    {
        IWebDriver driver;
        private string sortButtonXpath = "//div[contains(text(),'Rikiavimas')]";

        public Navigation(IWebDriver driver)
        {
            this.driver = driver;           
        }

        public void ClickOnCategory(string parent, string child)
        {
            By parentCategory = By.XPath("//a[contains(text(),'" + parent + "')]");
            Actions action = new Actions(driver);
            IWebElement parentCatObj = driver.FindElement(parentCategory);
            action.MoveToElement(parentCatObj).Perform();
            By innerCat = By.XPath("//span[contains(text(),'" + child + "')]//parent::a");
            driver.FindElement(innerCat).Click();
        }

        public void AddItemToCart(int whichItemToAdd)
        {
            string addToCartXpath = "(//div[@class='cardButtons'])" + "[" + whichItemToAdd + "]";
            string itemCardXpath = "(//a[@class='productCard'])" + "[" + whichItemToAdd + "]";
            GeneralMethods general = new GeneralMethods(driver);
            general.ClickElementIfExists("//button[contains(@class,'CloseButton')]");            
            general.ScrollToElement(itemCardXpath);
            general.MoveToElement(itemCardXpath);
            By addToBasketButton = By.XPath(addToCartXpath);
            driver.FindElement(addToBasketButton).Click();
        }

        public void ClickSortByLowestPrice()
        {
            GeneralMethods generalMethods = new GeneralMethods(driver);
            IWebElement sortButton = generalMethods.WaitUntilElementExists(sortButtonXpath, driver);
            sortButton.Click();
            By element = By.XPath("//div[contains(@id,'Pigiausios viršuje')]");
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(element));
            driver.FindElement(element).Click();
        }
    }
 }
