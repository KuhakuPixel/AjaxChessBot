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
        
        [Fact]
        public void TestConvertToExplicitNotation()
        {
            FenBoard fenBoard = new FenBoard();
            List<ChessProperty.ExplicitMove> moves=fenBoard.ConvertMoveToExplicitAlgebraicNotation("Nc3", ChessProperty.PieceColor.white);
            Console.WriteLine(JsonConvert.SerializeObject(moves));
        }
        
        



        #region test get piece to move from algebraic notation
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation0()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.pawn)
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPiecesToMove("e4", ChessProperty.PieceColor.white);
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation1()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.black,ChessProperty.PieceName.knight)
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPiecesToMove("Nc6", ChessProperty.PieceColor.black);
            CollectionAssert.AreEquivalent(expected, actual);

        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation2()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.black,ChessProperty.PieceName.queen)
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPiecesToMove("Qxe6", ChessProperty.PieceColor.black);
            CollectionAssert.AreEquivalent(expected, actual);

        }

        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation3()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
                   new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.king),
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.rook),
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPiecesToMove("O-O-O", ChessProperty.PieceColor.white);
            CollectionAssert.AreEquivalent(expected, actual);
        }
        [Fact]
        public void TestGetPieceToMoveFromAlgebraicNotation4()
        {
            List<ChessProperty.ChessPiece> expected = new List<ChessProperty.ChessPiece> {
                 
              new ChessProperty.ChessPiece(ChessProperty.PieceColor.white,ChessProperty.PieceName.knight),
            };
            List<ChessProperty.ChessPiece> actual = ChessLib.GetPiecesToMove("Nge2", ChessProperty.PieceColor.white);
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
            List<ChessProperty.SquareLocation> actual=ChessLib.GetDestinationSquares("e4",ChessProperty.PieceColor.white);
            CollectionAssert.AreEqual(expected, actual);
        }


        [Fact]
        public void TestGetDestinationSquareFromNotation1()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                new ChessProperty.SquareLocation('c',3),
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquares("Nc3", ChessProperty.PieceColor.white);
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
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquares("O-O", ChessProperty.PieceColor.white);
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
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquares("O-O", ChessProperty.PieceColor.black);
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
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquares("O-O-O", ChessProperty.PieceColor.white);
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
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquares("O-O-O", ChessProperty.PieceColor.black);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Fact]
        public void TestGetDestinationSquareFromNotation6()
        {
            List<ChessProperty.SquareLocation> expected = new List<ChessProperty.SquareLocation>
            {
                 new ChessProperty.SquareLocation('f',4),
                 
            };
            List<ChessProperty.SquareLocation> actual = ChessLib.GetDestinationSquares("Qxf4", ChessProperty.PieceColor.black);
            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion
    }

}

