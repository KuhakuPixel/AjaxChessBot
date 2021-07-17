using System;
using System.Collections.Generic;
using System.Text;
using AjaxChessBotHelperLib;
namespace AjaxDynamicHtmlReader
{
    public static class OnlineChessGameStateDecoder
    {
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
        public static List<string> DecodeLichessMove(List<string> htmlCodes)
        {

            string jsScript = GetJavaScriptFromLichessHtmlCode(htmlCodes);
            return DecodeLichessMoveFromJavaScript(jsScript);

        }
        
        
        public static List<string> DecodeLichessMoveFromJavaScript(string jsScript)
        {

            
            List<string> uciMoves = new List<string>();


            //get element in string  after " "uci":" "
            //starts scanning for uci (Universal chess interface ) move
            for (int i = 0; i < jsScript.Length; i++)
            {
                //subString="uci":"
                string subString = "\"uci\":\"";
                if (i + subString.Length < jsScript.Length)
                {

                    //found "uci":"exampleMove"
                    if (AjaxStringHelper.IsSubStringInTheFirstSubStringOfString(str: jsScript, startIndex: i,
                        subString: subString))
                    {
                        int startIndex = i + subString.Length - 1;
                        
                        string str = jsScript.Substring(startIndex,jsScript.Length-startIndex);
                        //get element in string  after " "uci":"e2e4" " 
                        string uciMove = AjaxStringHelper.GetStringBetweenTwoChar(
                            str: str,
                            charStart: '"',
                            charEnd: '"',
                            isInclusive: false
                            );

                        uciMoves.Add(uciMove);
                    }

                }
                else
                {
                    break;
                }
            }


            return uciMoves;

        }
        public static ChessGameProperties.PieceColor DecodeLichessPlayerColor(List<string> htmlCodes)
        {
            
            string jsScript = OnlineChessGameStateDecoder.GetJavaScriptFromLichessHtmlCode(htmlCodes);
            return DecodeLichessPlayerColorFromJavaScript(jsScript);
        }
        public static ChessGameProperties.PieceColor DecodeLichessPlayerColorFromJavaScript(string jsScript)
        {
            ChessGameProperties.PieceColor playerColor=ChessGameProperties.PieceColor.white;
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
                            playerColor = ChessGameProperties.PieceColor.black;
                        }
                        else if (playerColorString.ToLower() == "white")
                        {
                            playerColor = ChessGameProperties.PieceColor.white;
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

