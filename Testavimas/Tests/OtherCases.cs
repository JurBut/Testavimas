using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Testavimas.POM;

namespace Testavimas
{
    public class OtherCases

    {
        IWebDriver driver;

        [SetUp]
        public void SETUP()
        {           
            driver = new ChromeDriver();
            GeneralMethods general = new GeneralMethods(driver);
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.eurovaistine.lt/";                      
            IWebElement cookies = general.WaitUntilElementExists("//button[@id='onetrust-accept-btn-handler']", driver);
            cookies.Click();  
            general.ClickElementIfExists("(//button[contains(@class,'PopupCloseButton')])[2]"); //closes popup      
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();

        }

        [Test]
        public void IfPricesEvenTest()
        {           
            Navigation navigation = new Navigation(driver);
            TopMenu topmenu = new TopMenu(driver);
            ItemDetails itemDetails = new ItemDetails(driver);

            //Kuria kategorija atidaryti
            navigation.ClickOnCategory("Kosmetika", "Veidui");  

            //Kelinta preke pridedu i krepseli ir pasiimu jos kaina sarase
            int whichItem = 2;          
            navigation.AddItemToCart(whichItem);         
            double priceFromItemList = itemDetails.PriceFromItemList(whichItem);

            //Paspaudimas ant krepselio + kainu lyginimas                             
            topmenu.ClickOnCart();

            if (priceFromItemList != itemDetails.PriceFromBasket())
            {
                Assert.Fail("Item price in list is different than in cart");
            }
        }

        [Test]
        public void SearchTest()
        {
            TopMenu topMenu = new TopMenu(driver);
            ItemDetails itemDetails = new ItemDetails(driver);

            string searchKeyword = "Pertusinas";
            topMenu.SearchForItem(searchKeyword);

            //Tikrina ar suranda prekę          
            itemDetails.CheckIfNameInDescription(searchKeyword);
        }

        [Test]
        public void PriceListDescendingTest()
        {
            ItemDetails itemDetails = new ItemDetails(driver);
            Navigation navigation = new Navigation(driver);
            GeneralMethods general = new GeneralMethods(driver);

            //Kuria kategorija atidaryti:
            navigation.ClickOnCategory("Namai ir elektronika", "Elektronikos prietaisai");

            //Paspaudzia rikiuoti nuo maziausios kainos + tikrina:
            navigation.ClickSortByLowestPrice();           
            general.ClickElementIfExists("//button[contains(@class,'InnerPopupCloseButton')]");
            itemDetails.CheckPriceSortingFromLowest();
        }
    }
}
