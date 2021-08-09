using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public class FenBoard
    {

        List<string> fenPosition = new List<string>
            {
                //white
                "RNBQKBNR",
                "PPPPPPPP",
                "11111111",
                "11111111",
                "11111111",
                "11111111",
                //black
                "pppppppp",
                "rnbqkbnr",

            };


        public Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> GetPiecesLocation()
        {
            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesLocations = new Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>>
            {

            };
            //get where all of the pieces are at
            for (int i = 0; i < fenPosition.Count; i++)
            {
                for (int j = 0; j < fenPosition[i].Length; j++)
                {
                    char abbreviatedName = fenPosition[i][j];
                    ChessProperty.ChessPiece chessPiece = ChessLib.GetPieceFromAbbreviation(abbreviatedName);

                    if (chessPiece.PieceName != ChessProperty.PieceName.empty)
                    {
                        if (!piecesLocations.ContainsKey(chessPiece))
                        {
                            ChessProperty.SquareLocation squareIdentifier = new ChessProperty.SquareLocation(AjaxStringHelper.AlphabetIndexToChar(j), i + 1);
                            piecesLocations.Add(chessPiece, new List<ChessProperty.SquareLocation> { squareIdentifier });
                        }
                        else
                        {
                            ChessProperty.SquareLocation squareLocation = new ChessProperty.SquareLocation(AjaxStringHelper.AlphabetIndexToChar(j), i + 1);

                            piecesLocations[chessPiece].Add(squareLocation);
                        }
                    }

                }

            }
            return piecesLocations;
        }
        /// <summary>
        /// For example convert inexplicit moves like e4 to e2e4 ,qd4 to [queenCoordinates]d4
        /// </summary>
        /// <returns></returns>
        public ChessProperty.ExplicitMove ConvertMoveToExplicitAlgebraicNotation(string moveNotation, ChessProperty.PieceColor colorToMove)
        {
            List<char> nonPawnPiecesAlgbraicNotation = new List<char> { 'K', 'Q', 'B', 'R', 'N' };
            ChessProperty.ExplicitMove explicitMove=new ChessProperty.ExplicitMove(
                new ChessProperty.SquareLocation('e',2),
                new ChessProperty.SquareLocation('e', 4)
                );
            string fromPosition = "";
            string toPosition = "";
            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesLocations = GetPiecesLocation();


            //pawn move forward
            if (moveNotation.Length == 2)
            {

                ChessProperty.SquareLocation squareLocationTo = new ChessProperty.SquareLocation(moveNotation[0], moveNotation[1]);
               
                if (colorToMove == ChessProperty.PieceColor.white)
                {
                    if (squareLocationTo.RankNumber == 4)
                    {
                        ChessProperty.ChessPiece whitePawn = new ChessProperty.ChessPiece(ChessProperty.PieceColor.white, ChessProperty.PieceName.pawn);
                        List<ChessProperty.SquareLocation> whitePawnsLocations = piecesLocations[whitePawn];
                        

                        if (whitePawnsLocations.Contains(new ChessProperty.SquareLocation(squareLocationTo.FileName, 3)))
                        {
                            explicitMove = new ChessProperty.ExplicitMove(
                                new ChessProperty.SquareLocation(squareLocationTo.FileName, 3),
                                squareLocationTo);
                        }
                        else if (whitePawnsLocations.Contains(new ChessProperty.SquareLocation(squareLocationTo.FileName, 2)))
                        {
                            explicitMove = new ChessProperty.ExplicitMove(
                                new ChessProperty.SquareLocation(squareLocationTo.FileName, 2),
                                squareLocationTo);
                        }
                        else
                        {
                            throw new ArgumentException("No pawn to move: "+moveNotation);
                        }

                    }
                }
                else if (colorToMove == ChessProperty.PieceColor.black)
                {
                    ChessProperty.ChessPiece blackPawn = new ChessProperty.ChessPiece(ChessProperty.PieceColor.black, ChessProperty.PieceName.pawn);
                    if (squareLocationTo.RankNumber == 5)
                    {
                        List<ChessProperty.SquareLocation> blackPawnLocations = piecesLocations[blackPawn];


                        if (blackPawnLocations.Contains(new ChessProperty.SquareLocation(squareLocationTo.FileName, 6)))
                        {
                            explicitMove = new ChessProperty.ExplicitMove(
                                new ChessProperty.SquareLocation(squareLocationTo.FileName, 6),
                                squareLocationTo);
                        }
                        else if (blackPawnLocations.Contains(new ChessProperty.SquareLocation(squareLocationTo.FileName, 7)))
                        {
                            explicitMove = new ChessProperty.ExplicitMove(
                                new ChessProperty.SquareLocation(squareLocationTo.FileName, 7),
                                squareLocationTo);
                        }
                        else
                        {
                            throw new ArgumentException("No pawn to move: " + moveNotation);
                        }
                    }
                }
            }




            return explicitMove;
        }

        public void Move(string moveAlgebraicNotation)
        {


        }
    }

}
