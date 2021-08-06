using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public class ChessProperty
    {
        public class SquareLocation
        {
            private int rankNumber;
            private char fileName;

            public SquareLocation(char fileName, int rankNumber)
            {
                if (rankNumber > 8 || rankNumber < 1)
                {
                    throw new ArgumentException("rankNumber is invalid:" + rankNumber.ToString());
                }
                if (AjaxStringHelper.CharToAlphabetIndex(fileName) > 8 || AjaxStringHelper.CharToAlphabetIndex(fileName) < 1)
                {
                    throw new ArgumentException("fileName is invalid:" + rankNumber.ToString());
                }
                this.rankNumber = rankNumber;
                this.fileName = fileName;
            }
            public string GetString()
            {
                return fileName.ToString() + rankNumber.ToString();
            }
        }
        
        public class ChessPiece
        {
            PieceColor pieceColor;
            public PieceColor PieceColor { get => pieceColor; }
        
            PieceName pieceName;
            public PieceName PieceName { get => pieceName}
            public ChessPiece(PieceColor pieceColor, PieceName pieceName)
            {
                this.pieceColor = pieceColor;
                this.pieceName = pieceName;
            }

          
        }

        public enum PieceColor
        {
            white,
            black
        }

        public enum PieceName
        {
            rook,
            knight,
            bishop,
            king,
            queen,
            pawn,
            empty
              
        }
    }
}
