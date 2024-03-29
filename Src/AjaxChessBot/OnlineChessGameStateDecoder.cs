﻿using System;
using System.Collections.Generic;
using System.Text;
using AjaxChessBotHelperLib;
namespace AjaxChessBot
{
    public static class OnlineChessGameStateDecoder
    {
        public enum MoveNotation
        {
            uci,
            fen
        }
        public static string GetJavaScriptFromLichessHtmlCode(List<string> htmlCodes)
        {
            for (int i = htmlCodes.Count - 1; i >= 0; i--)
            {
                //found js
                if (htmlCodes[i].Contains("lichess.load.then"))
                {
                    return htmlCodes[i];
                }
            }
            throw new ArgumentException("No JavaScript found in the html codes");

        }
        /// <summary>
        /// Decodes lichess game move from a html source 
        /// </summary>
        /// <param name="htmlCodes"></param>
        /// <returns></returns>
        public static List<string> DecodeLichessMove(List<string> htmlCodes, MoveNotation moveNotation)
        {

            string jsScript = GetJavaScriptFromLichessHtmlCode(htmlCodes);
            return DecodeLichessMoveFromJavaScript(jsScript,  moveNotation);

        }
        
        
        public static List<string> DecodeLichessMoveFromJavaScript(string jsScript,MoveNotation moveNotation)
        {

            
            List<string> moves = new List<string>();

             string key = "";
            switch (moveNotation)
            {
                case MoveNotation.uci:
                    key= "\"uci\":\"";
                    break;
                case MoveNotation.fen:
                    key = "\"fen\":\"";
                    break;
            }
            //get element in string  after " "uci":" "
            //starts scanning for uci (Universal chess interface ) move
            for (int i = 0; i < jsScript.Length; i++)
            {
                //subString="uci":"
               
                if (i + key.Length < jsScript.Length)
                {

                    //found "uci":"exampleMove"
                    if (AjaxStringHelper.IsSubStringInTheFirstSubStringOfString(str: jsScript, startIndex: i,
                        subString: key))
                    {
                        int startIndex = (i + key.Length) - 1;
                        
                        string str = jsScript.Substring(startIndex,jsScript.Length-startIndex);
                        //get element in string  after " "uci":"e2e4" " 
                        string uciMove = AjaxStringHelper.GetStringBetweenTwoChar(
                            str: str,
                            charStart: '"',
                            charEnd: '"',
                            isInclusive: false
                            );

                        moves.Add(uciMove);
                    }

                }
                else
                {
                    break;
                }
            }


            return moves;

        }
        public static ChessProperty.PieceColor DecodeLichessPlayerColor(List<string> htmlCodes)
        {
            
            string jsScript = OnlineChessGameStateDecoder.GetJavaScriptFromLichessHtmlCode(htmlCodes);
            return DecodeLichessPlayerColorFromJavaScript(jsScript);
        }
        public static ChessProperty.PieceColor DecodeLichessPlayerColorFromJavaScript(string jsScript)
        {
            ChessProperty.PieceColor playerColor=ChessProperty.PieceColor.white;
            //the color of the player is found on ("player":"black") when loading javascript
            for (int i = 0; i < jsScript.Length; i++)
            {
                //subString= "player":"
                string subString = "\"player\":\"";
                if (i + subString.Length < jsScript.Length)
                {
                    //found "player":"color"
                    if (AjaxStringHelper.IsSubStringInTheFirstSubStringOfString(str: jsScript, startIndex: i,
                        subString: subString))
                    {
                        int startIndex = i + subString.Length - 1;

                        string str = jsScript.Substring(startIndex, jsScript.Length - startIndex);
                        //get element in string  after " "uci":"e2e4" " 
                        string playerColorString = AjaxStringHelper.GetStringBetweenTwoChar(
                            str: str,
                            charStart: '"',
                            charEnd: '"',
                            isInclusive: false
                            );

                        if (playerColorString.ToLower() == "black")
                        {
                            playerColor = ChessProperty.PieceColor.black;
                        }
                        else if (playerColorString.ToLower() == "white")
                        {
                            playerColor = ChessProperty.PieceColor.white;
                        }
                        else
                        {
                            throw new ArgumentException("player's piece color is invalid: " + playerColor+" ,only black and white is available");
                        }
                    }
                }
            }
            return playerColor;
        }
    }

}

