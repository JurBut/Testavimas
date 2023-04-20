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
        private string ItemNameXpath = "(//div[@class='product-card--title'])";
        By PriceInCartXpath;
        By ItemCardXpath;


        public ItemDetails(IWebDriver driver)
        {
            this.driver = driver;
            PriceInCartXpath = By.XPath("//div[@class='price']");
            ItemCardXpath = By.XPath("//div[@class = 'product-card']");
        }   

        public double CheckPriceInList(string whichItemToCheck)

        {
            By priceOfItem = By.XPath("(//div[@class='product-card--price'])" + "[" + whichItemToCheck + "]");
            string itemPrice = driver.FindElement(priceOfItem).Text;
            decimal decimalPrice = decimal.Parse(itemPrice.Replace(" €", "").Replace(",", "."));
            double price = (double)decimalPrice;
            return price;
        }

        public double CheckPriceInBasket()

        {           
            string itemPriceCart = driver.FindElement(PriceInCartXpath).Text;
            decimal decimalPrice = decimal.Parse(itemPriceCart.Replace(" €", "").Replace(",", "."));
            double price = (double)decimalPrice;
            return price;
        }

        public void CheckItemName(string name)

        {
            int skaicius = driver.FindElements(By.XPath(ItemNameXpath)).Count;

            for (int i = 1; i <= skaicius; i++)
            {
                By findName = By.XPath(ItemNameXpath + "[" + i + "]");
                string chosenName = driver.FindElement(findName).Text.ToLower();
                if (!chosenName.Contains(name.ToLower()))
                {
                    Assert.Fail("Search is not working");
                }
            }
        }

        public void CheckPriceSortingFromLowest()

        {           
            List<double> prices = new List<double>();
            
            foreach (IWebElement card in driver.FindElements(ItemCardXpath))
            {
                IWebElement priceElement = card.FindElement(By.XPath(".//div[@class='product-card--price']"));

                if (card.FindElements(By.XPath(".//div[contains(@class,'discount')]")).Count > 0)
                {
                    
                    // Discount egzistuoja
                    IWebElement discountPriceElement = card.FindElement(By.XPath(".//div[@class='product-card--discount']"));
                    string onePrice = discountPriceElement.Text;
                    decimal decimalPrice = decimal.Parse(onePrice.Replace(" €", "").Replace(",", "."));
                    double priceDouble = (double)decimalPrice;
                    prices.Add(priceDouble);
                }
                else
                {                   
                    //  Discount neegzistuoja
                    string onePrice = priceElement.Text;
                    decimal decimalPrice = decimal.Parse(onePrice.Replace(" €", "").Replace(",", "."));
                    double priceDouble = (double)decimalPrice;
                    prices.Add(priceDouble);
                }
            }
           
            //Console.WriteLine("Prices List: " + string.Join(", ", prices));

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

