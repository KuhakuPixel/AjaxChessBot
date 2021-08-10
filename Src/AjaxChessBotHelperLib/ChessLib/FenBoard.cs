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
        public List<ChessProperty.ExplicitMove> ConvertMoveToExplicitAlgebraicNotation(string moveNotation, ChessProperty.PieceColor colorToMove)
        {
            List<ChessProperty.ExplicitMove> explicitMoves = new List<ChessProperty.ExplicitMove>();

            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> piecesAndLocations = GetPiecesLocation();

            List<ChessProperty.ChessPiece> piecesToMove = ChessLib.GetPieceToMove(moveNotation, colorToMove);


           
            List<ChessProperty.SquareLocation> destinationSquare = ChessLib.GetDestinationSquare(moveNotation, colorToMove);

            //getting the original square of the piece
            if (piecesToMove.Count == 1 && destinationSquare.Count == 1)
            {
                List<ChessProperty.SquareLocation> availableMovePiecesLocations = ChessLib.GetAvailablePieceToMove(
                    piecesAndLocations,
                    destinationSquare[0],
                    piecesToMove[0]);
                if (availableMovePiecesLocations.Count == 1)
                {
                    ChessProperty.ExplicitMove move = new ChessProperty.ExplicitMove(availableMovePiecesLocations[0], destinationSquare[0]);
                }
                //need to remove ambiguity
                else if (availableMovePiecesLocations.Count > 1)
                {

                }
                else
                {

                }
                
            }
            //castle
            else if (piecesToMove.Count == 2 && destinationSquare.Count == 2)
            {

            }





            return explicitMoves;
        }

        public void Move(string moveAlgebraicNotation)
        {


        }
    }

}
