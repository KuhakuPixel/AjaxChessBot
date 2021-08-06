using AjaxChessBotHelperLib.ChessLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public class ChessHelper
    {

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
        

        public static ChessProperty.ChessPiece GetChessPieceFromAbbreviation(char abbreviatedName)
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
                    throw new ArgumentException("invalid  abbreviatedName of the chess piece : "+pieceName);
                 
            }
            return new ChessProperty.ChessPiece(pieceColor, pieceName);
        }
        

    }
}
