using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using AjaxChessBot;
using System.Collections.Generic;

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
                bool startGettingMoveInfo = false;
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://www.chess.com/play/computer");
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            startGettingMoveInfo = true;
                            break;
                          
                        }
                    }
                }
                
                while (startGettingMoveInfo)
                {
                    try
                    {
                        //click download button
                        driver.FindElement(By.CssSelector(".icon-font-chess.download")).Click();
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        string htmlContent = (string)js.ExecuteScript("return document.documentElement.outerHTML;");
                        Console.WriteLine(htmlContent);
                        driver.FindElement(By.CssSelector(".icon-font-chess.x.ui_outside-close-icon")).Click();
                        Console.WriteLine("click");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                   
                }

                Console.ReadLine();
            }
        }
    }
}