using System;
using Xunit;
using AjaxChessBotHelperLib;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
namespace AjaxChessBotTest
{
    public class ChessLibTest
    {
        /*
        [Fact]
        public void TestGetPositions()
        {
            FenBoard fenBoard = new FenBoard();
            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesLocations = fenBoard.GetPiecesLocation();

            ChessProperty.ChessPiece pieceToFind = new ChessProperty.ChessPiece(ChessProperty.PieceColor.black, ChessProperty.PieceName.pawn);



            List<ChessProperty.SquareLocation> blackPawnLocations = piecesLocations[pieceToFind];
            ChessProperty.SquareLocation e7 = new ChessProperty.SquareLocation('e', 7);
            bool hasPawnOnE7 = blackPawnLocations.Contains(e7);
        }
        */
        [Fact]
        public void TestGetExplicitMoveWhitePawn()
        {

            FenBoard fenBoard = new FenBoard();
            ChessProperty.ExplicitMove expected = new ChessProperty.ExplicitMove(
                    new ChessProperty.SquareLocation('e', 2),
                    new ChessProperty.SquareLocation('e', 4)
                    );

            ChessProperty.ExplicitMove actual = fenBoard.ConvertMoveToExplicitAlgebraicNotation("e4", ChessProperty.PieceColor.white);

            Xunit.Assert.Equal(JsonConvert.SerializeObject(actual), JsonConvert.SerializeObject(expected));
        }
        [Fact]
        public void TestGetExplicitMoveBlackPawn()
        {

            FenBoard fenBoard = new FenBoard();
            ChessProperty.ExplicitMove expected = new ChessProperty.ExplicitMove(
                    new ChessProperty.SquareLocation('e', 7),
                    new ChessProperty.SquareLocation('e', 5)
                    );

            ChessProperty.ExplicitMove actual = fenBoard.ConvertMoveToExplicitAlgebraicNotation("e5", ChessProperty.PieceColor.black);

            Xunit.Assert.Equal(JsonConvert.SerializeObject(actual), JsonConvert.SerializeObject(expected));
        }
        




        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation0()
        {
            List<ChessProperty.PieceName> expected = new List<ChessProperty.PieceName> { ChessProperty.PieceName.pawn };
            List<ChessProperty.PieceName> actual = ChessLib.GetPieceToMoveFromAlgebraicNotation("e4");
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation1()
        {
            List<ChessProperty.PieceName> expected = new List<ChessProperty.PieceName> { ChessProperty.PieceName.knight };
            List<ChessProperty.PieceName> actual = ChessLib.GetPieceToMoveFromAlgebraicNotation("Nc6");
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation2()
        {
            List<ChessProperty.PieceName> expected = new List<ChessProperty.PieceName> { ChessProperty.PieceName.queen };
            List<ChessProperty.PieceName> actual = ChessLib.GetPieceToMoveFromAlgebraicNotation("Qxe6");
            CollectionAssert.AreEquivalent(expected, actual);

        }

        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation3()
        {
            List<ChessProperty.PieceName> expected = new List<ChessProperty.PieceName> { ChessProperty.PieceName.king,ChessProperty.PieceName.rook };
            List<ChessProperty.PieceName> actual = ChessLib.GetPieceToMoveFromAlgebraicNotation("O-O-O");
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation4()
        {
            List<ChessProperty.PieceName> expected = new List<ChessProperty.PieceName> { ChessProperty.PieceName.knight };
            List<ChessProperty.PieceName> actual = ChessLib.GetPieceToMoveFromAlgebraicNotation("Nge2");
            CollectionAssert.AreEquivalent(expected, actual);

        }
    }

}

