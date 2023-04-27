using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Testavimas.POM
{
    internal class ItemDetails
    {

        IWebDriver driver;
        private string itemNameXpath = "//div[@class='title']"; //naudoju kaip xPath, nes naudoju for loope, kuriame prie string + i
        By priceInCart = By.XPath("//div[@class='cartUnitPrice']");
        By itemCard = By.XPath("//div[@class ='productCardContent']");
        By discountPrice = By.XPath(".//div[@class='productPrice flex-end']//*[last()][text()]");
        By noneDiscountPrice = By.XPath(".//div[@class ='productPrice text-end']");


        public ItemDetails(IWebDriver driver)
        {
            this.driver = driver;                       
        }   

        public double PriceFromItemList(int whichItemToCheck)
        {
            By priceOfItem = By.XPath("(//div[contains(@class,'productPrice')])" + "[" + whichItemToCheck + "]");
            string itemPrice = driver.FindElement(priceOfItem).Text;
            double priceDouble = double.Parse(itemPrice.Replace(" €", "").Replace(",", "."));           
            return priceDouble;
        }

        public double PriceFromBasket()
        {
            GeneralMethods general = new GeneralMethods(driver);
            IWebElement cart = general.WaitUntilElementExists("//div[@class='cartUnitPrice']", driver);
            string itemPriceCart = cart.FindElement(priceInCart).Text;
            double priceDouble = double.Parse(itemPriceCart.Replace(" €", "").Replace(",", "."));
            return priceDouble;
        }

        public void CheckIfNameInDescription(string name)
        {
            int howManyItems = driver.FindElements(By.XPath(itemNameXpath)).Count;

            for (int i = 1; i <= howManyItems; i++)
            {
                By descriptionField = By.XPath(itemNameXpath + "[" + i + "]");
                string description = driver.FindElement(descriptionField).Text.ToLower();
                if (!description.Contains(name.ToLower()))
                {
                    Assert.Fail("Search is not working");
                }
            }
        }

        public void CheckPriceSortingFromLowest()
        {           
            List<double> prices = new List<double>();
            
            foreach (IWebElement card in driver.FindElements(itemCard))
            {               
                if (card.FindElements(discountPrice).Count > 0)
                {                   
                    // Discount egzistuoja
                    IWebElement discountPriceElement = card.FindElement(discountPrice);
                    string onePrice = discountPriceElement.Text;
                    double priceDouble = double.Parse(onePrice.Replace(" €", "").Replace(",", "."));
                    prices.Add(priceDouble);
                }
                else
                {
                    // Discount neegzistuoja
                    IWebElement PriceElement = card.FindElement(noneDiscountPrice);
                    string priceNoDiscountElement = PriceElement.Text;
                    double priceDouble = double.Parse(priceNoDiscountElement.Replace(" €", "").Replace(",", "."));
                    prices.Add(priceDouble);
                }
            }
            Console.WriteLine("Price List: " + string.Join(", ", prices));

            for (int i = 0; i > prices.Count - 1; i++)
            {
                Console.WriteLine(prices[i]);
                if (prices[i] < prices[i + 1])
                {
                    Assert.Fail("Price sorting to descending is not working");
                }
            }
        }
    }
}

