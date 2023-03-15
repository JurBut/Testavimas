using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            By ParentCategory = By.XPath("//a[contains(text(),'" + parent + "')]");
            Actions action = new Actions(driver);
            IWebElement ParentCatObj = driver.FindElement(ParentCategory);
            action.MoveToElement(ParentCatObj).Perform();
            By innerCat = By.XPath("//span[contains(text(),'" + child + "')]//parent::a");
            driver.FindElement(innerCat).Click();
        }

        public void ClosePopUp()
        {
            By PopUpClose = By.XPath("//button[contains(@class,'InnerPopupCloseButton')]");
            driver.FindElement(PopUpClose).Click();
        }

        public void AddToCart(string whichItemToAdd)
        {
            By AddButton = By.XPath("(//div[@class='button-wrapper']//button[@type='submit'])" + "[" + whichItemToAdd + "]");
            driver.FindElement(AddButton).Click();
        }

        public string CheckPrice(string whichItemToCheck)

        {

            By PriceOfItem = By.XPath("(//div[@class='product-card--price'])" + "[" + whichItemToCheck + "]");
            string ItemPrice = driver.FindElement(PriceOfItem).Text;
            return ItemPrice;
        }

    }

 }
