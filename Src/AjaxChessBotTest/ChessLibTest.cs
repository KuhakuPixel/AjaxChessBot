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




        #region test get piece to move from algebraic notation
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation0()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.pawn)
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPieceToMove("e4", ChessProperty.PieceColor.white);
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation1()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.black,ChessProperty.PieceName.knight)
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPieceToMove("Nc6", ChessProperty.PieceColor.black);
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation2()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.black,ChessProperty.PieceName.queen)
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPieceToMove("Qxe6", ChessProperty.PieceColor.black);
            CollectionAssert.AreEquivalent(expected, actual);

        }

        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation3()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
                   new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.king),
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.rook),
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPieceToMove("O-O-O", ChessProperty.PieceColor.white);
            CollectionAssert.AreEquivalent(expected, actual);
        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation4()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
                 
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.knight),
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPieceToMove("Nge2", ChessProperty.PieceColor.white);
            CollectionAssert.AreEquivalent(expected, actual);

        }
        #endregion


        #region Test get destination square from algebraic Notation
        [Fact]
        public void TestGetDestinationSquareFromNotation0()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                new ChessProperty.SquareLocation('e',4),
            };
            List<ChessProperty.SquareLocation> actual=ChessLib.GetDestinationSquare("e4",ChessProperty.PieceColor.white);
            CollectionAssert.AreEqual(expected, actual);
        }


        [Fact]
        public void TestGetDestinationSquareFromNotation1()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                new ChessProperty.SquareLocation('c',3),
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquare("Nc3", ChessProperty.PieceColor.white);
            CollectionAssert.AreEqual(expected, actual);
        }
        [Fact]
        public void TestGetDestinationSquareFromNotation2()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                 new ChessProperty.SquareLocation('g',1),
                 new ChessProperty.SquareLocation('f',1),
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquare("O-O", ChessProperty.PieceColor.white);
            CollectionAssert.AreEqual(expected, actual);
        }
        [Fact]
        public void TestGetDestinationSquareFromNotation3()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                 new ChessProperty.SquareLocation('g',8),
                 new ChessProperty.SquareLocation('f',8),
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquare("O-O", ChessProperty.PieceColor.black);
            CollectionAssert.AreEqual(expected, actual);
        }
        [Fact]
        public void TestGetDestinationSquareFromNotation4()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                 new ChessProperty.SquareLocation('c',1),
                 new ChessProperty.SquareLocation('d',1),
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquare("O-O-O", ChessProperty.PieceColor.white);
            CollectionAssert.AreEqual(expected, actual);
        }
        [Fact]
        public void TestGetDestinationSquareFromNotation5()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                 new ChessProperty.SquareLocation('c',8),
                 new ChessProperty.SquareLocation('d',8),
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquare("O-O-O", ChessProperty.PieceColor.black);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Fact]
        public void TestGetDestinationSquareFromNotation6()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                 new ChessProperty.SquareLocation('f',4),
                 
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquare("Qxf4", ChessProperty.PieceColor.black);
            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion
    }

}

