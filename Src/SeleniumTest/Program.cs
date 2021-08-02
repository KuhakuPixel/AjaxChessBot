﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
namespace SeleniumTest
{
    class Program
    {
        static void TestKeyEvent()
        {
           
        }
        static void Main()
        {
            TestKeyEvent();
            
            using (IWebDriver driver = new ChromeDriver(@"C:\Users\Nicho\Downloads\ChromDriver92"))
            {
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://www.chess.com/play/computer");
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            By by = By.CssSelector(".icon-font-chess.download");
                            driver.FindElement(by).Click();
                            Console.WriteLine("click");
                        }
                    }
                }


                Console.ReadLine();
            }
        }
    }
}