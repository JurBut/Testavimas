using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Testavimas
{
    public class OtherCases

    {
        private static string driverPath = "C:\\Users\\M\\Desktop\\Mokslai\\Testavimas\\Testavimas\\bin\\Debug\\chromedriver.exe";
        static IWebDriver driver;
        [SetUp]
        public static void SETUP()
        {
            driver = new ChromeDriver(driverPath);
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.eurovaistine.lt/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            By Cookies = By.XPath("//button[@id='onetrust-accept-btn-handler']");
            driver.FindElement(Cookies).Click();
        }
        [TearDown]
        public static void TearDown()
        {
            //driver.Quit();
        }


        [Test]
        public static void BasketTest()
        {
            Navigation navigation = new Navigation(driver);

            navigation.NavigateFromMainPage("Kosmetika", "Veidui");
            Thread.Sleep(300);

            string whichItem = "4";

            TopMenu add = new TopMenu(driver);
            add.AddToCart(whichItem);

            TopMenu CheckItem = new TopMenu(driver);
            string price = CheckItem.CheckPrice(whichItem);


            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(1000, 0)");

            Thread.Sleep(1000);

            TopMenu CheckItemBasket = new TopMenu(driver);


            if (price != CheckItemBasket.CheckPriceInBasket())
            {
                Assert.Fail("Item price is different in cart");
            }


        }






    }
}
