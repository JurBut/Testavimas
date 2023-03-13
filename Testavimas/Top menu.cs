using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testavimas
{
    internal class TopMenu
    {
        IWebDriver driver;
        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AddToCart (string whichItemToAdd)
        {

            By AddButton = By.XPath("(//div[@class='button-wrapper']//button[@type='submit'])"+ "[" + whichItemToAdd + "]");
            driver.FindElement(AddButton).Click();

        }

        public string CheckPrice (string whichItemToAdd)

        {

            By PriceOfItem = By.XPath("(//div[@class='product-card--price'])" + "[" + whichItemToAdd + "]");
            
            string ItemPrice = driver.FindElement(PriceOfItem).Text;

            return ItemPrice;

        }

        public string CheckPriceInBasket ()

        {

            By ClickOnCart = By.XPath("//div[@class='headerCart-wrapper']");

            driver.FindElement(ClickOnCart).Click();

            By PriceInCart = By.XPath("//div[@class='price']");

            string ItemPriceCart = driver.FindElement(PriceInCart).Text;

            return ItemPriceCart;

        }

    }

}
