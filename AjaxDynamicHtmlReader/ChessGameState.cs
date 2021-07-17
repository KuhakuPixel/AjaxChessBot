using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    class ChessGameState
    {
        enum PieceColor
        {
            white,
            black
        }
        private PieceColor playerColor;
        private string gameLink = "";
        private List<string> currentMoves = new List<string>();
        //todo: find all the properties from the link like the color of the player ,the moves and ect
        public ChessGameState(string gameLink)
        {
            this.gameLink = gameLink;
            currentMoves=ChessMovesDecoder.DecodeLichessMove(gameLink);
        }



        //funciton to update the gamestate
        public void UpdateGameState()
        {
            currentMoves = ChessMovesDecoder.DecodeLichessMove(gameLink);
        }

    }
}
