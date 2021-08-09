using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib.ChessLib
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
                    ChessProperty.ChessPiece chessPiece = ChessHelper.GetChessPieceFromAbbreviation(abbreviatedName);

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
        public string ConvertMoveToExplicitAlgebraicNotation(string moveNotation, ChessProperty.PieceColor colorToMove)
        {
            string fromPosition = "";
            string toPosition = "";
            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesLocations = GetPiecesLocation();


            //pawn move forward
            if (moveNotation.Length == 2)
            {

                ChessProperty.SquareLocation squareLocationTo = new ChessProperty.SquareLocation(moveNotation[0], moveNotation[1]);
                ChessProperty.ChessPiece pieceToMove = new ChessProperty.ChessPiece(ChessProperty.PieceColor.black, ChessProperty.PieceName.pawn);
                if (colorToMove == ChessProperty.PieceColor.white)
                {
                    if (squareLocationTo.RankNumber == 4)
                    {
                        List<ChessProperty.SquareLocation> pawnsLocations = piecesLocations[pieceToMove]; 

                        
                        //check if there is a pawn in rank 3 or rank 2

                    }
                }
                else if (colorToMove == ChessProperty.PieceColor.black)
                {
                    if (squareLocationTo.RankNumber == 5)
                    {

                    }
                }
            }




            return fromPosition + toPosition;
        }
        
        public void Move(string moveAlgebraicNotation)
        {


        }
    }

}
