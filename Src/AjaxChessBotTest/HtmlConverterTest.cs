using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AjaxDynamicHtmlReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjaxChessBotTest
{
    public class HtmlConverterTest
    {
        
        [Fact]
        public void SplitHtmlByBracketsTest0()
        {
            List<string> splittedHtml = AjaxHtmlReader.SplitHtmlByBrackets(
               "<h1>My Bad header</h1>",



              includeContentInsideTag:true);
            List<string> expected = new List<string> { "h1", "My Bad header", "/h1" };
         
            CollectionAssert.AreEqual(expected, splittedHtml);


        }
        [Fact]
        public void SplitHtmlByBracketsTest1()
        {
            List<string> splittedHtml = AjaxHtmlReader.SplitHtmlByBrackets(
               "<h1>My Bad header</h1><li>content0</li>",



              includeContentInsideTag: false);
            List<string> expected = new List<string> { "My Bad header","content0"};

            CollectionAssert.AreEqual(expected, splittedHtml);


        }
        [Fact]
        public void SplitHtmlByBracketsTest2()
        {
            List<string> splittedHtml = AjaxHtmlReader.SplitHtmlByBrackets(
               "syntaxError<h1>My Bad header</h1><li>content0</li>syntaxError",



              includeContentInsideTag: false);
            List<string> expected = new List<string> {"syntaxError","My Bad header", "content0","syntaxError" };

            CollectionAssert.AreEqual(expected, splittedHtml);


        }
    }
}
