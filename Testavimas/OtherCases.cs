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
            By PopUpClose = By.XPath("//button[contains(@class,'InnerPopupCloseButton')]");
            driver.FindElement(PopUpClose).Click();

        }
        [TearDown]
        public static void TearDown()
        {
            //driver.Quit();
        }


        [Test]
        public static void BasketTest()

        {
            //Kuria kategorija atidaryti
            Navigation navigation = new Navigation(driver);
            navigation.WhichCategoryToChoose("Kosmetika", "Veidui");
            Thread.Sleep(300);

            //Uzdarau apatini popup
            Navigation ClosePopUp = new Navigation(driver);
            ClosePopUp.ClosePopUp();

            //Kelinta preke pridedu i krepseli ir pasiimu kaina
            string whichItem = "4";
            Navigation add = new Navigation(driver);
            add.AddToCart(whichItem);
            Navigation CheckItem = new Navigation(driver);
            string price = CheckItem.CheckPrice(whichItem);

            //Skrolina i virsu
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(1000, 0)");
            Thread.Sleep(1000);

            //Paspaudimas ant krepselio
            TopMenu ClickBasket = new TopMenu(driver);
            ClickBasket.ClickOnCart();

            //Pasiimu kaina krepselyje esancios prekes ir palyginu ar atitinka
            TopMenu CheckItemBasket = new TopMenu(driver);

            if (price != CheckItemBasket.CheckPriceInBasket())
            {
                Assert.Fail("Item price is different in cart");
            }


        }


    }
}
