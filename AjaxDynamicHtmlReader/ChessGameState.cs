using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    public class ChessGameState
    {
      
        private ChessGameProperties.PieceColor playerColor;
        private string gameLink = "";
        private List<string> allGameMovesFen = new List<string>();
        //todo: find all the properties from the link like the color of the player ,the moves and ect
        public ChessGameState(string gameLink)
        {
            //initializing moves
            this.gameLink = gameLink;
            List<string> gameHtmlCodes=AjaxHtmlReader.ReadAndProcessHtmlSource(gameLink, includeContentInsideTag: false);
            allGameMovesFen=OnlineChessGameStateDecoder.DecodeLichessMove(gameHtmlCodes,OnlineChessGameStateDecoder.MoveNotation.fen);
            playerColor = OnlineChessGameStateDecoder.DecodeLichessPlayerColor(gameHtmlCodes);
        }



        //funciton to update the gamestate
        private void UpdateGameState()
        {
            List<string> gameHtmlCodes = AjaxHtmlReader.ReadAndProcessHtmlSource(this.gameLink, includeContentInsideTag: false);
            allGameMovesFen = OnlineChessGameStateDecoder.DecodeLichessMove(gameHtmlCodes,OnlineChessGameStateDecoder.MoveNotation.fen);
        }

        public string GetCurrentMovesFen()
        {
            this.UpdateGameState();
            if (allGameMovesFen.Count > 0)
            {
                return allGameMovesFen[allGameMovesFen.Count - 1];
            }
            else
            {
               throw new Exception("allGameMovesFen is empty");
            }
          
        }

    }
}
