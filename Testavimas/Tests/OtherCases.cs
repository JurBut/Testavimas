using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Testavimas.POM;

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
            By Cookies = By.XPath("//button[@id='onetrust-accept-btn-handler']");
            driver.FindElement(Cookies).Click();           
        }

        [TearDown]
        public static void TearDown()
        {
            //driver.Quit();
        }


        [Test]
        public static void IfPricesEvenTest()

        {
            
            Navigation navigation = new Navigation(driver);
            TopMenu topmenu = new TopMenu(driver);
            ItemDetails details = new ItemDetails(driver);

            //Kuria kategorija atidaryti
            navigation.WhichCategoryToChoose("Kosmetika", "Veidui");  

            //Kelinta preke pridedu i krepseli ir pasiimu jos kaina sarase
            string whichItem = "2";          
            navigation.AddToCart(whichItem);         
            double price = details.CheckPriceInList(whichItem);

            //Sroll iki pat virsaus + paspaudimas ant krepselio
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(1000, 0)");                                
            topmenu.ClickOnCart();

            if (price != details.CheckPriceInBasket())
            {
                Assert.Fail("Item price is different in cart");
            }
        }

        [Test]
        public static void SearchTest()
        {
            string searchKeyword = "Pertusinas";
            TopMenu search = new TopMenu(driver);
            search.ClickOnSearchBar(searchKeyword);

            //Tikrina ar suranda prekę
            ItemDetails searchResults = new ItemDetails(driver);
            searchResults.CheckItemName(searchKeyword);
        }

        [Test]
        public static void PriceListDescendingTest()
        {

            //Kuria kategorija atidaryti
            Navigation navigation = new Navigation(driver);
            navigation.WhichCategoryToChoose("Namai ir elektronika", "Elektronikos prietaisai");

            //Paspaudzia rikiuoti nuo maziausios kainos + tikrina
            navigation.ClickSortByLowestPrice();
            ItemDetails searchResults = new ItemDetails(driver);
            searchResults.CheckPriceSortingFromLowest();

        }
    }
}
