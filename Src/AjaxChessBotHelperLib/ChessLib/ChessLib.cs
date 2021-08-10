﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public class ChessLib
    {
        static readonly char[] piecesLetters = new char[] { 'K', 'Q', 'B', 'R', 'N', 'P' };
        static readonly char[] specialNotationSymbol = new char[] { '+', '#', '!' };

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
        
        private static bool IsKingSideCastling(string moveNotation)
        {
            return (moveNotation.CountOccurance("0") == 2 && moveNotation.CountOccurance("-") == 1 ||
                   moveNotation.CountOccurance("O") == 2 && moveNotation.CountOccurance("-") == 1);
        }
        private static bool IsQueenSideCastling(string moveNotation)
        {
            return (moveNotation.CountOccurance("0") == 3 && moveNotation.CountOccurance("-") == 2 ||
                   moveNotation.CountOccurance("O") == 3 && moveNotation.CountOccurance("-") == 2);
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

        public static List<ChessProperty.ChessPiece> GetPieceToMove(string moveNotation,ChessProperty.PieceColor colorToMove)
        {
            List<ChessProperty.ChessPiece> piecesToMove = new List<ChessProperty.ChessPiece>();


            moveNotation = moveNotation.RemoveChars(specialNotationSymbol);
            //when there is capture notation it is actually not important to determine the moving piece
            if (moveNotation.Contains("x"))
            {
                if (moveNotation.Split('x').Length>=1)
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
                    ChessProperty.PieceName pieceName = GetPieceNameFromAbbreviation(piecesLetters[i]);
                    piecesToMove.Add(new ChessProperty.ChessPiece(colorToMove,pieceName));
                    break;
                }
            }
            //check if a castle or a pawnMove that omit p from its notation(usual convention)
            if (piecesToMove.Count == 0)
            {
                
                if (IsKingSideCastling(moveNotation)||IsQueenSideCastling(moveNotation))
                {
                    piecesToMove.Add(new ChessProperty.ChessPiece(colorToMove,ChessProperty.PieceName.king));
                    piecesToMove.Add(new ChessProperty.ChessPiece(colorToMove, ChessProperty.PieceName.rook));
                }
                else if(moveNotation.Length==2)
                {
                    if (char.IsLetter(moveNotation[0]) && char.IsNumber(moveNotation[1]))
                    {
                        piecesToMove.Add(new ChessProperty.ChessPiece(colorToMove, ChessProperty.PieceName.pawn));
                    }
                  
                }
                else if (moveNotation.Length == 1)
                {
                    if ("abcdefgh".Contains(moveNotation))
                    {
                        piecesToMove.Add(new ChessProperty.ChessPiece(colorToMove, ChessProperty.PieceName.pawn));
                    }
                }
            }

            if (piecesToMove.Count > 0)
            {
                return piecesToMove;
            }
            else
            {
                throw new ArgumentException("Invalid move notation : " + moveNotation);
            }
           

        }

        public static List<ChessProperty.SquareLocation> GetDestinationSquare(string moveNotation, ChessProperty.PieceColor colorToMove)
        {
            moveNotation =moveNotation.RemoveChars(specialNotationSymbol);
            
            List<ChessProperty.SquareLocation> destinationSquares = new List<ChessProperty.SquareLocation>();
            if (moveNotation.Length>=2)
            {

                if (char.IsLetter(moveNotation[moveNotation.Length-2])
                    &&char.IsNumber(moveNotation[moveNotation.Length -1]))
                {
                    //for all notation except castling the last 2 notation will always be the destination square
                    destinationSquares.Add(
                        new ChessProperty.SquareLocation(
                            moveNotation[moveNotation.Length - 2], 
                            moveNotation[moveNotation.Length - 1]));
                }
                else if (IsKingSideCastling(moveNotation))
                {
                    switch (colorToMove)
                    {
                        case ChessProperty.PieceColor.white:
                            {
                                destinationSquares.Add(new ChessProperty.SquareLocation('g',1));
                                destinationSquares.Add(new ChessProperty.SquareLocation('f', 1));
                                break;
                            }
                          
                        case ChessProperty.PieceColor.black:
                            destinationSquares.Add(new ChessProperty.SquareLocation('g', 8));
                            destinationSquares.Add(new ChessProperty.SquareLocation('f', 8));
                            break;
                    }
                }
                else if (IsQueenSideCastling(moveNotation))
                {
                    switch (colorToMove)
                    {
                        case ChessProperty.PieceColor.white:
                            {
                                destinationSquares.Add(new ChessProperty.SquareLocation('c', 1));
                                destinationSquares.Add(new ChessProperty.SquareLocation('d', 1));
                                break;
                            }

                        case ChessProperty.PieceColor.black:
                            destinationSquares.Add(new ChessProperty.SquareLocation('c', 8));
                            destinationSquares.Add(new ChessProperty.SquareLocation('d', 8));
                            break;
                    }
                }
            }
            return destinationSquares;
        }
    }
}