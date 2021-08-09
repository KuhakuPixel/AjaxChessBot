
using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public class ChessLib
    {
        static readonly char[] piecesLetters = new char[] { 'K', 'Q', 'B', 'R', 'N', 'P' };
        ///note:if chess board class != board flipped then it is white
        public static string MoveListToFEN(List<string> moveList)
        {

            bool isPawnMove = false;
            FenBoard fenBoard = new FenBoard();
            string activeColor = "w";
            string castlingRights = "KQkq";
            string enPassantTargets = "-";
            int halfMoveClock = 0;
            int fullMoveNumber = 0;
            // active color
            if (moveList.Count % 2 == 0)
            {
                activeColor = "w";
            }
            else
            {
                activeColor = "b";
            }

            for (int i = 0; i < moveList.Count; i++)
            {
                string moveAlgebraicNotation = moveList[i];
                //pawnmoves
                if (moveList[i].Length == 2)
                {





                }
            }


            return "";


        }


        public static ChessProperty.ChessPiece GetPieceFromAbbreviation(char abbreviatedName)
        {
            ChessProperty.PieceColor pieceColor = ChessProperty.PieceColor.white;
            ChessProperty.PieceName pieceName = ChessProperty.PieceName.pawn;
            if (char.IsUpper(abbreviatedName))
            {
                pieceColor = ChessProperty.PieceColor.white;
            }
            else
            {
                pieceColor = ChessProperty.PieceColor.black;
            }

            switch (char.ToLower(abbreviatedName))
            {
                case 'k':
                    pieceName = ChessProperty.PieceName.king;
                    break;
                case 'b':
                    pieceName = ChessProperty.PieceName.bishop;
                    break;
                case 'q':
                    pieceName = ChessProperty.PieceName.queen;
                    break;
                case 'p':
                    pieceName = ChessProperty.PieceName.pawn;
                    break;
                case 'r':
                    pieceName = ChessProperty.PieceName.rook;
                    break;
                case 'n':
                    pieceName = ChessProperty.PieceName.knight;
                    break;
                case '1':
                    pieceName = ChessProperty.PieceName.empty;
                    break;
                default:
                    throw new ArgumentException("invalid  abbreviatedName of the chess piece : " + abbreviatedName);

            }
            return new ChessProperty.ChessPiece(pieceColor, pieceName);
        }
        public static ChessProperty.PieceName GetPieceNameFromAbbreviation(char abbreviatedName)
        {

            ChessProperty.PieceName pieceName = ChessProperty.PieceName.pawn;

            switch (char.ToLower(abbreviatedName))
            {
                case 'k':
                    pieceName = ChessProperty.PieceName.king;
                    break;
                case 'b':
                    pieceName = ChessProperty.PieceName.bishop;
                    break;
                case 'q':
                    pieceName = ChessProperty.PieceName.queen;
                    break;
                case 'p':
                    pieceName = ChessProperty.PieceName.pawn;
                    break;
                case 'r':
                    pieceName = ChessProperty.PieceName.rook;
                    break;
                case 'n':
                    pieceName = ChessProperty.PieceName.knight;
                    break;
                case '1':
                    pieceName = ChessProperty.PieceName.empty;
                    break;
                default:
                    throw new ArgumentException("invalid  abbreviatedName of the chess piece : " + abbreviatedName);

            }
            return pieceName;
        }

        public static List<ChessProperty.PieceName> GetPieceToMoveFromAlgebraicNotation(string moveNotation)
        {
            List<ChessProperty.PieceName> pieceNameToMove = new List<ChessProperty.PieceName>();


            //remove + (for check notation) because it won't help to determine what piece will move
            if (moveNotation.Contains("+"))
            {
                moveNotation.Replace("+", "");
            }
            //when there is capture notation it is actually not important to determine the moving piece
            else if (moveNotation.Contains("x"))
            {
                if (moveNotation.Split('x').Length == 2)
                {
                    moveNotation = moveNotation.Split('x')[0];
                }
                else
                {
                    throw new ArgumentException("invalid move notation :" +moveNotation);
                }
             
                
            }
            //check if pieces move except castling and moveNotation that doesnt have 'P'
            for (int i = 0; i < piecesLetters.Length; i++)
            {
                if (moveNotation.Contains(piecesLetters[i].ToString()))
                {
                    pieceNameToMove.Add(GetPieceNameFromAbbreviation(piecesLetters[i]));
                    break;
                }
            }
            //check if a castle or a pawnMove that omit p from its notation(usual convention)
            if (pieceNameToMove.Count == 0)
            {
                //castling
                if (moveNotation.CountOccurance("0") >= 2 && moveNotation.CountOccurance("-") >= 1 ||
                    moveNotation.CountOccurance("O") >= 2 && moveNotation.CountOccurance("-") >= 1
                    )
                {
                    pieceNameToMove.Add(ChessProperty.PieceName.king);
                    pieceNameToMove.Add(ChessProperty.PieceName.rook);
                }
                else if(moveNotation.Length==2)
                {
                    if (char.IsLetter(moveNotation[0]) && char.IsNumber(moveNotation[1]))
                    {
                        pieceNameToMove.Add(ChessProperty.PieceName.pawn);
                    }
                  
                }
                else if (moveNotation.Length == 1)
                {
                    if ("abcdefgh".Contains(moveNotation))
                    {
                        pieceNameToMove.Add(ChessProperty.PieceName.pawn);
                    }
                }
            }

            if (pieceNameToMove.Count > 0)
            {
                return pieceNameToMove;
            }
            else
            {
                throw new ArgumentException("Invalid move notation : " + moveNotation);
            }
           

        }

    }
}
