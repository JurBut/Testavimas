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
            public void NavigateFromMainPage(string parent, string child)
            {
                By ParentCategory = By.XPath("//a[contains(text(),'" + parent + "')]");
                Actions action = new Actions(driver);
                IWebElement ParentCatObj = driver.FindElement(ParentCategory);
                action.MoveToElement(ParentCatObj).Perform();
                By innerCat = By.XPath("//span[contains(text(),'" + child + "')]//parent::a");
                driver.FindElement(innerCat).Click();
            }
        }
    
}
