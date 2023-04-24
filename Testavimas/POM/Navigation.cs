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

        public Navigation(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WhichCategoryToChoose(string parent, string child)
        {
            By parentCategory = By.XPath("//a[contains(text(),'" + parent + "')]");
            Actions action = new Actions(driver);
            IWebElement parentCatObj = driver.FindElement(parentCategory);
            action.MoveToElement(parentCatObj).Perform();
            By innerCat = By.XPath("//span[contains(text(),'" + child + "')]//parent::a");
            driver.FindElement(innerCat).Click();
        }

        public void AddToCart(string whichItemToAdd)
        {
            string addToCartXpath = "(//div[@class='button-wrapper']//button[@type='submit'])" + "[" + whichItemToAdd + "]";            
            GeneralMethods general = new GeneralMethods(driver);          
            general.ScrollToElement(addToCartXpath); 
            By addButton = By.XPath(addToCartXpath);
            driver.FindElement(addButton).Click();

        }

        public void ClickSortByLowestPrice()
        {
            By clickSort = By.XPath("//div[contains(@class,'pagination-top')]//select[@id='sort-box']");
            driver.FindElement(clickSort).Click();
            By element = By.XPath("//div[contains(@class,'pagination-top')]//select[@id='sort-box']//option[contains(text(),'Pigiausios viršuje')]");
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(element));
            driver.FindElement(element).Click();

        }
    }
 }
