using System;
using Xunit;
using AjaxChessBotHelperLib;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Xunit.Assert.Equal(expected, contentBetweenChar);


        }

        [Theory]
        [InlineData("O-O-O","-",2)]
        [InlineData("O-O-O", "O", 3)]
        [InlineData("O      -   O   -   O", "O", 3)]
        [InlineData("NicholasPixel", "i", 2)]
        public void CountOccuranceTest(string str,string strToCount, int expected)
        {
            int actual=str.CountOccurance(strToCount);
            Xunit.Assert.Equal(expected, actual);


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
            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void SplitStringToChunkTest0()
        {
            List<string> expected = new List<string>() { "e2", "e4" };
            List<string> actual=AjaxStringHelper.SplitStringToChunk("e2e4",2,true);

            CollectionAssert.AreEqual(expected, actual);
          
        }
        [Fact]
        public void SplitStringToChunkWithRemainderTest0()
        {
            
            List<string> expected = new List<string>() { "h7", "h8","q"};
            List<string> actual = AjaxStringHelper.SplitStringToChunk("h7h8q", 2, true);

            CollectionAssert.AreEqual(expected, actual);

        }
        [Fact]
        public void RemoveCharsTest0()
        {
            string str = "PaimonIsTheBest";
            string actual=str.RemoveChars(new char[] {'a','I','z'});
            string expected = "PimonsTheBest";
            Xunit.Assert.Equal(expected,actual);
        }
        [Fact]
        public void RemoveCharsTest1()
        {
            string str = "ILOVEMinecraft";
            string actual = str.RemoveChars(new char[] {'e','1','a'});
            string expected = "ILOVEMincrft";
            Xunit.Assert.Equal(expected, actual);
        }
    }

}

