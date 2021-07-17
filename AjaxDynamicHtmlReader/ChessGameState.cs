using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    class ChessGameState
    {
      
        private ChessGameProperties.PieceColor playerColor;
        private string gameLink = "";
        private List<string> currentMoves = new List<string>();
        //todo: find all the properties from the link like the color of the player ,the moves and ect
        public ChessGameState(string gameLink)
        {
            //initializing moves
            this.gameLink = gameLink;
            List<string> gameHtmlCodes=AjaxHtmlReader.ReadAndProcessHtmlSource(gameLink, includeContentInsideTag: false);
            currentMoves=OnlineChessGameStateDecoder.DecodeLichessMove(gameHtmlCodes);
            playerColor = OnlineChessGameStateDecoder.DecodeLichessPlayerColor(gameHtmlCodes);
        }



        //funciton to update the gamestate
        private void UpdateGameState()
        {
            List<string> gameHtmlCodes = AjaxHtmlReader.ReadAndProcessHtmlSource(this.gameLink, includeContentInsideTag: false);
            currentMoves = OnlineChessGameStateDecoder.DecodeLichessMove(gameHtmlCodes);
        }

        public List<string> GetCurrentMoves()
        {
            this.UpdateGameState();
            return currentMoves;
        }

    }
}
