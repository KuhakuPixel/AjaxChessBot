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
            ChessProperty.ExplicitMove explicitMove = new ChessProperty.ExplicitMove(
                new ChessProperty.SquareLocation('e', 2),
                new ChessProperty.SquareLocation('e', 4)
                );
       
            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesLocations = GetPiecesLocation();

            List<ChessProperty.ChessPiece> piecesToMove = ChessLib.GetPieceToMove(moveNotation,colorToMove);

            if (piecesToMove.Count == 1)
            {
                if (piecesToMove[0].PieceName == ChessProperty.PieceName.pawn)
                {
                    ChessProperty.SquareLocation destinationSquare = new ChessProperty.SquareLocation(moveNotation[0], moveNotation[1]);

                    if (colorToMove == ChessProperty.PieceColor.white)
                    {
                        if (destinationSquare.RankNumber == 4)
                        {
                          
                            List<ChessProperty.SquareLocation> whitePawnsLocations = piecesLocations[piecesToMove[0]];


                            if (whitePawnsLocations.Contains(new ChessProperty.SquareLocation(destinationSquare.FileName, 3)))
                            {
                                explicitMove = new ChessProperty.ExplicitMove(
                                    new ChessProperty.SquareLocation(destinationSquare.FileName, 3),
                                    destinationSquare
                                    );
                            }
                            else if (whitePawnsLocations.Contains(new ChessProperty.SquareLocation(destinationSquare.FileName, 2)))
                            {
                                explicitMove = new ChessProperty.ExplicitMove(
                                    new ChessProperty.SquareLocation(destinationSquare.FileName, 2),
                                    destinationSquare);
                            }
                            else
                            {
                                throw new ArgumentException("No pawn to move: " + moveNotation);
                            }

                        }
                    }
                    else if (colorToMove == ChessProperty.PieceColor.black)
                    {
                        
                        if (destinationSquare.RankNumber == 5)
                        {
                            List<ChessProperty.SquareLocation> blackPawnLocations = piecesLocations[piecesToMove[0]];


                            if (blackPawnLocations.Contains(new ChessProperty.SquareLocation(destinationSquare.FileName, 6)))
                            {
                                explicitMove = new ChessProperty.ExplicitMove(
                                    new ChessProperty.SquareLocation(destinationSquare.FileName, 6),
                                    destinationSquare);
                            }
                            else if (blackPawnLocations.Contains(new ChessProperty.SquareLocation(destinationSquare.FileName, 7)))
                            {
                                explicitMove = new ChessProperty.ExplicitMove(
                                    new ChessProperty.SquareLocation(destinationSquare.FileName, 7),
                                    destinationSquare);
                            }
                            else
                            {
                                throw new ArgumentException("No pawn to move: " + moveNotation);
                            }
                        }
                    }
                }
                
                else if (piecesToMove[0].PieceName == ChessProperty.PieceName.knight)
                {

                }
            }
            //castle
            else if (piecesToMove.Count == 2)
            {

            }
           




            return explicitMove;
        }

        public void Move(string moveAlgebraicNotation)
        {


        }
    }

}
