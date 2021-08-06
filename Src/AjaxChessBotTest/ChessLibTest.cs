using System;
using Xunit;
using AjaxChessBotHelperLib;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjaxChessBotHelperLib.ChessLib;
namespace AjaxChessBotTest
{
    public class ChessLibTest
    {
        [Fact]
        
        public void TestConvertToExplicitAlgebraicNotation0()
        {
            FenBoard fenBoard = new FenBoard();
            fenBoard.ConvertToExplicitAlgebraicNotation("e4", ChessProperty.PieceColor.white);
            //Xunit.Assert.Equal(expected, contentBetweenChar);


        }
    }

}

