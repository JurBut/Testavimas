using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Testavimas.POM;

namespace Testavimas
{
    internal class TopMenu
     
    {
        IWebDriver driver;
        By searchBarXpath;
        By inputXpath;
        By clickSearchXpath;
        // Galima ta pati pakartoti ir cia?
        // Kodel dalis yra pagal By
        // o kita dalis tiesiog string?
        // plius By pavadinimai bet cia vel skonio gal reikalas
        private string cartXpath = "//div[@class='headerCart-amount ']";

        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
            searchBarXpath = By.XPath("//span[@class='sn-suggest']");
            inputXpath = By.XPath("//input[@type='search'][2]");
            clickSearchXpath = By.XPath("//button[@class='headerSearchButton']");
        }

        public void ClickOnCart()
        {
            // Cia man taip skaitosi labai keistai
            // GeneralMethods pasidaro navigation
            // Tu kaip ir turi klase navigation..
            // Nelabai patinka pavadinimai kintamuju, 
            // arba gal tada ne vietoje yra closePopUp metodas?
            GeneralMethods navigation = new GeneralMethods(driver);
            navigation.ClosePopUp("//img[@class='close-icon-popup']");         
            IWebElement cart = navigation.WaitUntilMethod(cartXpath, driver);
            cart.Click();
        }

        // Cia vel toks missleading pavadinimas, 
        // Ne tik click padarai, bet ir suvedi 
        // elementa ir dar seach'a paleidi,
        // gal geresnis pavadinimas butu
        // SearchForItem?
        public void ClickOnSearchBar(string itemName)
        {           
            driver.FindElement(searchBarXpath).Click();          
            driver.FindElement(inputXpath).SendKeys(itemName);            
            driver.FindElement(clickSearchXpath).Click();
        }
    }
}
