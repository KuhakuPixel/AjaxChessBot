
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

            List<string>actual=OnlineChessGameStateDecoder.DecodeLichessMove(jsScript);
            CollectionAssert.AreEqual(expected, actual);

        }

        [Fact]
        public void DecodeLichessMoveTest1()
        {
            List<string> expected = new List<string> { "e2e4","e7e5" };
            string jsScript = "whateverWordsPutHere bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai "+"\"uci\":\"e7e5\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMove(jsScript);
            CollectionAssert.AreEqual(expected, actual);

        }
        [Fact]
        public void DecodeLichessMoveTest2()
        {
            List<string> expected = new List<string> { "e2e4", "e7e5" };
            string jsScript = "whateverWordsPutHere \"uci\":null bla bla bla \"uci\":\"e2e4\"   Bla bla bla" +
                "Some Dirty Data ,Some Dirty Html what ever please dont take my life yamete kudasai " + "\"uci\":\"e7e5\"";

            List<string> actual = OnlineChessGameStateDecoder.DecodeLichessMove(jsScript);
            CollectionAssert.AreEqual(expected, actual);

        }
    }
}
