using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjaxChessBotHelperLib;
namespace AjaxChessBotTest
{
    public class StringBuilderExtensionTest
    {
        [Fact]
        public void ToListTest()
        {
            List<string> expected = new List<string>(){"Line 0","Line 1","Line 2" };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Line 0");
            sb.AppendLine("Line 1");
            sb.AppendLine("Line 2");
            List<string> actual=sb.ToList();
            CollectionAssert.AreEqual(expected,actual);

        }
    }
}
