
using System;
using Xunit;
using System.Collections.Generic;
using AjaxDynamicHtmlReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjaxChessBotTest
{
    public class LichessMoveDecoderTest
    {
        [Fact]
        public void DecodeLichessMoveTest0()
        {
            List<string> expected = new List<string> { "e2e4" };
            string jsScript = "whateverWordsPutHere bla bla bla \"uci\":\"e2e4\"";

            List<string>actual=OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript);
            CollectionAssert.AreEqual(expected, actual);

        }

        [Fact]
        public void DecodeLichessMoveTest1()
        {
            List<string> expected = new List<string> { "e2e4","e7e5" };
            string jsScript = "whateverWordsPutHere bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai "+"\"uci\":\"e7e5\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript);
            CollectionAssert.AreEqual(expected, actual);

        }
        [Fact]
        public void DecodeLichessMoveTest2()
        {
            List<string> expected = new List<string> { "e2e4", "e7e5" };
            string jsScript = "whateverWordsPutHere \"uci\":null bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai " + "\"uci\":\"e7e5\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript);
            CollectionAssert.AreEqual(expected, actual);

        }
        [Theory]
        [InlineData("whatever  things i hate \"player\":\"white\"", ChessGameProperties.PieceColor.white)]
        [InlineData("whatever \"player\":\"black\" things i hate ", ChessGameProperties.PieceColor.black)]
        [InlineData("whatever Dirty Data bla bla bla< \"player\":\"black\"> things i hate ", ChessGameProperties.PieceColor.black)]
        [InlineData("<\"player\":\"white\">whatever Dirty Data bla bla bla things i hate ", ChessGameProperties.PieceColor.white)]
        public void DecodeLichessPlayerColor1(string jsScript,ChessGameProperties.PieceColor expected)
        {

            

            ChessGameProperties.PieceColor actual = OnlineChessGameStateDecoder.DecodeLichessPlayerColorFromJavaScript(jsScript);
            

            Xunit.Assert.Equal(expected, actual);

        }

    }
}
