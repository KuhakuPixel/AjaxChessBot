using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AjaxChessBotHelperLib;
namespace AjaxChessBotUnitTest
{
    [TestClass]
    public class AjaxStringHelperTest
    {
       
        [TestMethod]
        public void GetStringBetweenTwoCharTest0()
        {
           string contentBetweenChar= AjaxStringHelper.GetStringBetweenTwoChar("nononono <My Room Is Full of Bs> bla bla bla",
                '<', '>', false);
            Assert.AreEqual("My Room Is Full of Bs",contentBetweenChar);

        }
        [TestMethod]
        public void GetStringBetweenTwoCharTest1()
        {
            string contentBetweenChar = AjaxStringHelper.GetStringBetweenTwoChar("nononono \"My Room Is Full of Bs\" bla bla bla",
                 '"', '"', false);
            Assert.AreEqual("My Room Is Full of Bs", contentBetweenChar);

        }

        [TestMethod]
        public void GetStringBetweenTwoCharTest2()
        {
            string contentBetweenChar = AjaxStringHelper.GetStringBetweenTwoChar("Owh no My Queen , hashTag EricROsen,",
                 ',', ',', false);
            Assert.AreEqual(" hashTag EricROsen", contentBetweenChar);

        }
    }
}
