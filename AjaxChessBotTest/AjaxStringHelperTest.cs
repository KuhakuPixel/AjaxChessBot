using System;
using Xunit;
using AjaxChessBotHelperLib;
namespace AjaxChessBotTest
{
    public class AjaxStringHelperTest
    {
        [Theory]
        [InlineData("nononono <My Room Is Full of Bs> bla bla bla",'<', '>', false, "My Room Is Full of Bs")]
        [InlineData("nononono \"My Room Is Full of Bs\" bla bla bla",'"', '"', false, "My Room Is Full of Bs")]
        [InlineData("Owh no My Queen , hashTag EricROsen,",',', ',', false, " hashTag EricROsen")]
        [InlineData("nononono <My Room Is Full of Bs> bla bla bla", '<', '>', true, "<My Room Is Full of Bs>")]
        public void GetStringBetweenTwoCharTest(string str,char charStart,char charEnd,bool isInclusive,string expected)
        {
            string contentBetweenChar = AjaxStringHelper.GetStringBetweenTwoChar(str,charStart,charEnd,isInclusive);
            Assert.Equal(expected, contentBetweenChar);
         

        }
        
        
    }
   
}

