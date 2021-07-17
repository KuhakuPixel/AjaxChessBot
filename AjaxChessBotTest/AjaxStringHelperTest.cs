using System;
using Xunit;
using AjaxChessBotHelperLib;
namespace AjaxChessBotTest
{
    public class AjaxStringHelperTest
    {
        [Theory]
        [InlineData("nononono <My Room Is Full of Bs> bla bla bla", '<', '>', false, "My Room Is Full of Bs")]
        [InlineData("nononono \"My Room Is Full of Bs\" bla bla bla", '"', '"', false, "My Room Is Full of Bs")]
        [InlineData("Owh no My Queen , hashTag EricROsen,", ',', ',', false, " hashTag EricROsen")]
        [InlineData("nononono <My Room Is Full of Bs> bla bla bla", '<', '>', true, "<My Room Is Full of Bs>")]
        public void GetStringBetweenTwoCharTest(string str, char charStart, char charEnd, bool isInclusive, string expected)
        {
            string contentBetweenChar = AjaxStringHelper.GetStringBetweenTwoChar(str, charStart, charEnd, isInclusive);
            Assert.Equal(expected, contentBetweenChar);


        }
        [Theory]
        [InlineData("uci:e2e4", 0, "uci:", true)]
        [InlineData("blabla uci:e2e4", 7, "uci:", true)]
        [InlineData("blabla uci:e2e4", 0, "uci:", false)]
        [InlineData("Test uci:e2e4", 2, "uci:", false)]
        public void IsSubStringInTheFirstSubStringOfStringTest(string str, int startIndex,
            string subString, bool expected)
        {
            bool actual = AjaxStringHelper.IsSubStringInTheFirstSubStringOfString(str, startIndex, subString);
            Assert.Equal(expected, actual);
        }



    }

}

