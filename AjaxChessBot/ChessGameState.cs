using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    public class ChessGameState
    {
      
        private ChessGameProperties.PieceColor playerColor;
        public ChessGameProperties.PieceColor PlayerColor { get => playerColor; }
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

        public ChessGameProperties.PieceColor GetCurrentTurn(string fenPosition)
        {
            //example of fen string
            //rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1
            string turnCode = fenPosition.Split(' ')[1];
            if (turnCode == "b")
            {
                return ChessGameProperties.PieceColor.black;
            }
            else if (turnCode == "w")
            {
                return ChessGameProperties.PieceColor.white;
            }
            else
            {
                throw new ArgumentException("fenPosition is invalid,cannot get the current turn of the game\n" +
                    "fenPosition: "+fenPosition);
            }
         
     
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
