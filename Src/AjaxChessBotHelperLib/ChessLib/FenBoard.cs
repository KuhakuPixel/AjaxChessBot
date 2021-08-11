using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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


        public Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> GetEveryPiecesLocation()
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
        /// Assuming that move[Notation] is valid (not moving into square that already has piece on it )
        /// For example convert inexplicit moves like e4 to e2e4 ,qd4 to [queenCoordinates]d4
        /// </summary>
        /// <returns></returns>
        public List<ChessProperty.ExplicitMove> ConvertMoveToExplicitAlgebraicNotation(string moveNotation, ChessProperty.PieceColor colorToMove)
        {
            List<ChessProperty.ExplicitMove> explicitMoves = new List<ChessProperty.ExplicitMove>();

            Dictionary<ChessProperty.ChessPiece, List<ChessProperty.SquareLocation>> allPiecesAndLocations = GetEveryPiecesLocation();

            


            Dictionary<ChessProperty.ChessPiece, ChessProperty.SquareLocation> piecesToMoveAndDestinationSquare=
                ChessLib.GetPiecesToMoveAndDestinationSquare(moveNotation, colorToMove);

            //getting the original square of the piece
            if (piecesToMoveAndDestinationSquare.Count==1)
            {
                ChessProperty.ChessPiece pieceToMove = piecesToMoveAndDestinationSquare.ElementAt(0).Key;
                ChessProperty.SquareLocation destinationSquare = piecesToMoveAndDestinationSquare.ElementAt(0).Value;
                List <ChessProperty.SquareLocation> validPiecesToMoveOriginalSquare = ChessLib.GetValidPiecesToMoveOriginalSquare(
                    allPiecesAndLocations,
                    destinationSquare,
                    pieceToMove
                   
                    );
                if (validPiecesToMoveOriginalSquare.Count == 1)
                {
                    ChessProperty.ExplicitMove move = new ChessProperty.ExplicitMove(validPiecesToMoveOriginalSquare[0], destinationSquare);
                    explicitMoves.Add(move);
                }
                //remove move ambiguity
                else if (validPiecesToMoveOriginalSquare.Count > 1)
                {
                    //either a letter(file) or number(rank)
                    char ambiguityKey = '\0';
                    //example :if there are 2 rooks on a1 and a8 , 
                    //notation like Ra4 will not be enough so by convention R1a4 will be used instead
                    //or if rooks are on a1 and h1 Re1 will not be neough ,must be Rae1
                    for (int i = 0; i < moveNotation.Length; i++)
                    {
                        if (ChessLib.piecesLetters.Contains(moveNotation[i]) && i < moveNotation.Length - 1)
                        {
                            ambiguityKey = moveNotation[i + 1];
                            break;
                        }
                    }
                    if (char.IsNumber(ambiguityKey))
                    {
                        int rankNumber = int.Parse(ambiguityKey.ToString());
                        for(int i = 0; i < validPiecesToMoveOriginalSquare.Count; i++)
                        {
                            if (validPiecesToMoveOriginalSquare[i].RankNumber == rankNumber)
                            {
                                ChessProperty.ExplicitMove move = new ChessProperty.ExplicitMove(validPiecesToMoveOriginalSquare[i], destinationSquare);
                                explicitMoves.Add(move);
                            }
                          
                        }
                    }
                    else if(char.IsLetter(ambiguityKey))
                    {
                        int chessFileName = ambiguityKey;
                        for (int i = 0; i < validPiecesToMoveOriginalSquare.Count; i++)
                        {
                            if (validPiecesToMoveOriginalSquare[i].FileName == chessFileName)
                            {
                                ChessProperty.ExplicitMove move = new ChessProperty.ExplicitMove(validPiecesToMoveOriginalSquare[i], destinationSquare);
                                explicitMoves.Add(move);
                            }

                        }
                    }
                }
             

            }
            //castle
            else if (piecesToMoveAndDestinationSquare.Count==2)
            {
                ChessProperty.ChessPiece king = piecesToMoveAndDestinationSquare.ElementAt(0).Key;
                ChessProperty.SquareLocation kingDestinationSquare = piecesToMoveAndDestinationSquare.ElementAt(0).Value;
                ChessProperty.ChessPiece rook = piecesToMoveAndDestinationSquare.ElementAt(1).Key;
                ChessProperty.SquareLocation rookDestinationSquare = piecesToMoveAndDestinationSquare.ElementAt(1).Value;

                List<ChessProperty.SquareLocation> kingOriginalSquare = ChessLib.GetValidPiecesToMoveOriginalSquare(
                    allPiecesAndLocations,
                    kingDestinationSquare,
                    king

                    );
                List<ChessProperty.SquareLocation> rookOriginalSquare = ChessLib.GetValidPiecesToMoveOriginalSquare(
                    allPiecesAndLocations,
                    kingDestinationSquare,
                    rook

                    );
                if (kingOriginalSquare.Count == rookOriginalSquare.Count && kingOriginalSquare.Count == 1)
                {
                    ChessProperty.ExplicitMove kingMove = new ChessProperty.ExplicitMove(kingOriginalSquare[0],kingDestinationSquare);
                    ChessProperty.ExplicitMove rookMove = new ChessProperty.ExplicitMove(rookOriginalSquare[0], rookDestinationSquare);
                    explicitMoves.Add(kingMove);
                    explicitMoves.Add(rookMove);
                }
                else
                {
                    throw new ArgumentException("Cannot convert , move is invalid : "+moveNotation);
                }

            }





            return explicitMoves;
        }

        public void Move(string moveAlgebraicNotation)
        {


        }
    }

}
