
using System;

namespace AjaxChessBotHelperLib
{
    public class ChessProperty
    {
        public class ExplicitMove
        {
            public SquareLocation from;
            public SquareLocation to;

            public ExplicitMove(SquareLocation from, SquareLocation to)
            {
                this.from = from;
                this.to = to;
            }

           
        }
        public class SquareLocation:IEquatable<SquareLocation>
        {
            private int rankNumber;
            private char fileName;
            public int RankNumber { get => rankNumber;  }
            public char FileName { get => fileName; }
            public SquareLocation(char file, int rank)
            {
                if (rank > 8 || rank < 1)
                {
                    throw new ArgumentException("rankNumber is invalid:" + rank.ToString());
                }
                if (AjaxStringHelper.CharToAlphabetIndex(file) > 8 || AjaxStringHelper.CharToAlphabetIndex(file) < 1)
                {
                    throw new ArgumentException("fileName is invalid:" + rank.ToString());
                }
                this.rankNumber = rank;
                this.fileName = file;
            }

            public SquareLocation(char file, char rank)
            {
                if (!char.IsDigit(rank))
                {
                    throw new ArgumentException("rankNumber is not a number:" + rank.ToString());
                }
                int rankNumber = int.Parse(rank.ToString());
                if (rankNumber > 8 || rankNumber < 1)
                {
                    throw new ArgumentException("rankNumber is invalid:" + rankNumber.ToString());
                }
                if (AjaxStringHelper.CharToAlphabetIndex(file) > 8 || AjaxStringHelper.CharToAlphabetIndex(file) < 1)
                {
                    throw new ArgumentException("fileName is invalid:" + rankNumber.ToString());
                }

                this.rankNumber = rankNumber;
                this.fileName = file;
            }

           

            public string GetString()
            {
                return fileName.ToString() + rankNumber.ToString();
            }

            public override int GetHashCode()
            {
                return rankNumber.GetHashCode()+fileName.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                return Equals(obj as SquareLocation);
            }
            public bool Equals(SquareLocation other)
            {
                return other.fileName == this.fileName && other.rankNumber == this.rankNumber;

            }
        }
        
        public class ChessPiece:IEquatable<ChessPiece>
        {
            private PieceColor pieceColor;


            private PieceName pieceName;

            public PieceColor PieceColor { get => pieceColor; }
            public PieceName PieceName { get => pieceName; }

            public ChessPiece(PieceColor pieceColor, PieceName pieceName)
            {
                this.pieceColor = pieceColor;
                this.pieceName = pieceName;
            }
            public override int GetHashCode()
            {
                return pieceColor.GetHashCode()+pieceName.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                return Equals(obj as ChessPiece);
            }
            public bool Equals(ChessPiece other)
            {
                return other.pieceColor == this.pieceColor && other.pieceName == this.pieceName;
                
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
