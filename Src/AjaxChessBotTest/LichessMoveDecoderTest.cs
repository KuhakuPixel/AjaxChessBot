
using System;
using Xunit;
using System.Collections.Generic;
using AjaxChessBot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjaxChessBotHelperLib;
namespace AjaxChessBotTest
{
    public class LichessMoveDecoderTest
    {
        [Fact]
        public void DecodeLichessMoveTestUCI0()
        {
            List<string> expected = new List<string> { "e2e4" };
            string jsScript = "whateverWordsPutHere bla bla bla \"uci\":\"e2e4\"";

            List<string>actual=OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript,
                OnlineChessGameStateDecoder.MoveNotation.uci);
            CollectionAssert.AreEqual(expected, actual);

        }

        [Fact]
        public void DecodeLichessMoveTestUCI1()
        {
            List<string> expected = new List<string> { "e2e4","e7e5" };
            string jsScript = "whateverWordsPutHere bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai "+"\"uci\":\"e7e5\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript,
                OnlineChessGameStateDecoder.MoveNotation.uci);
            CollectionAssert.AreEqual(expected, actual);

        }
        [Fact]
        public void DecodeLichessMoveTestUCI2()
        {
            List<string> expected = new List<string> { "e2e4", "e7e5" };
            string jsScript = "whateverWordsPutHere \"uci\":null bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai " + "\"uci\":\"e7e5\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript,
                OnlineChessGameStateDecoder.MoveNotation.uci);
            CollectionAssert.AreEqual(expected, actual);

        }
        [Fact]
        public void DecodeLichessMoveTestFEN0()
        {
            List<string> expected = new List<string> { "rnbqkb1r/pppp1ppp/5n2/4p3/2B1P3/8/PPPP1PPP/RNBQK1NR w KQkq - 2 3" };
            string jsScript = "whateverWordsPutHere \"uci\":null bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai " + "\"fen\":\"rnbqkb1r/pppp1ppp/5n2/4p3/2B1P3/8/PPPP1PPP/RNBQK1NR w KQkq - 2 3\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMoveFromJavaScript(jsScript,
                OnlineChessGameStateDecoder.MoveNotation.fen);
            CollectionAssert.AreEqual(expected, actual);

        }
        [Theory]
        [InlineData("whatever  things i hate \"player\":\"white\"", ChessProperty.PieceColor.white)]
        [InlineData("whatever \"player\":\"black\" things i hate ", ChessProperty.PieceColor.black)]
        [InlineData("whatever Dirty Data bla bla bla< \"player\":\"black\"> things i hate ", ChessProperty.PieceColor.black)]
        [InlineData("<\"player\":\"white\">whatever Dirty Data bla bla bla things i hate ", ChessProperty.PieceColor.white)]
        public void DecodeLichessPlayerColor1(string jsScript,ChessProperty.PieceColor expected)
        {

            

            ChessProperty.PieceColor actual = OnlineChessGameStateDecoder.DecodeLichessPlayerColorFromJavaScript(jsScript);
            

            Xunit.Assert.Equal(expected, actual);

        }

    }
}
