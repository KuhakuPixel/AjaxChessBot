using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public class ChessLib
    {
        public enum MoveType
        {
            pawnMove,
            castle,
            capture,
            pieceMove,
            kingMove

        }
        public static MoveType ParseAlgebraicMove(string moveAlgebraicNotation)
        {
            MoveType moveType = MoveType.pawnMove;
            //pawn moves
            if (moveAlgebraicNotation.Length == 2)
            {
                //notation validation
                if (char.IsLetter(moveAlgebraicNotation[0]) && char.IsDigit(moveAlgebraicNotation[1]))
                {
                    moveType = MoveType.pawnMove;
                }
                else
                {
                    throw new ArgumentException("algebraicNotation is invalid : " + moveAlgebraicNotation);
                }
            }
            return moveType;

        }
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
            public void Move(string moveAlgebraicNotation)
            {
                //make a move to target

                StringBuilder row = new StringBuilder(fenPosition[moveAlgebraicNotation[1] - 1]);
                row[AjaxStringHelper.CharToAlphabetIndex(moveAlgebraicNotation[0])] = 'p';
                //remove
            }
        }
        //note:if chess board class != board flipped then it is white
        public static string MoveListToFEN(List<string> moveList)
        {
            bool isPawnMove = false;
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
                string algebraicNotation = moveList[i];
                //pawnmoves
                if (moveList[i].Length == 2)
                {
                    MoveType moveType = ParseAlgebraicMove(algebraicNotation);
                    if (moveType == MoveType.pawnMove)
                    {
                        //check for possible en passant
                        if (i == moveList.Count - 1)
                        {
                            //possible en passant target
                            if (int.Parse(algebraicNotation[1].ToString()) == 4 && activeColor == "w")
                            {
                                enPassantTargets = algebraicNotation[0] + 3.ToString();
                            }
                            else if (7 - int.Parse(algebraicNotation[1].ToString()) == 5 && activeColor == "b")
                            {
                                enPassantTargets = algebraicNotation[0] + 6.ToString();
                            }

                        }


                    }






                }
            }


            return "";


        }
    }
}
