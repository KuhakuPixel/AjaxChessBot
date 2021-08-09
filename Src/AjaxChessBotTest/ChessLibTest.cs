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
            fenBoard.ConvertMoveToExplicitAlgebraicNotation("e4", ChessProperty.PieceColor.white);
            //Xunit.Assert.Equal(expected, contentBetweenChar);


        }
        [Fact]
        public void TestGetPositions()
        {
            FenBoard fenBoard = new FenBoard();
            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesLocations = fenBoard.GetPiecesLocation();

            ChessProperty.ChessPiece pieceToFind = new ChessProperty.ChessPiece(ChessProperty.PieceColor.black, ChessProperty.PieceName.pawn);



            List<ChessProperty.SquareLocation> blackPawnLocations = piecesLocations[pieceToFind];
            
        }
    }

}

