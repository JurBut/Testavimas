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


        public void ClickOnCart()

        {
            By ClickOnCart = By.XPath("//div[contains(@class,'headerCart')]");

            driver.FindElement(ClickOnCart).Click();
        }

        public string CheckPriceInBasket ()

        {
            By PriceInCart = By.XPath("//div[@class='price']");

            string ItemPriceCart = driver.FindElement(PriceInCart).Text;

            return ItemPriceCart;
        }





    }

}
