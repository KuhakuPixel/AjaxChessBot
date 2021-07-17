using System;
using System.Collections.Generic;
using System.Text;
using AjaxChessBotHelperLib;
namespace AjaxDynamicHtmlReader
{
    class OnlineChessGameStateDecoder
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
        /// Decodes move from a html source 
        /// </summary>
        /// <param name="htmlCodes"></param>
        /// <returns></returns>
        public static List<string> DecodeLichessMove(List<string> htmlCodes)
        {

            string jsScript = GetJavaScriptFromLichessHtmlCode(htmlCodes);
            List<string> uciMoves = new List<string>();


            //get element in string  after " "uci":" "
            //starts scanning for uci (Universal chess interface ) move
            for (int i = 0; i < jsScript.Length; i++)
            {
                //subString="uci":"
                string subString = "\"uci\":\"";
                if (i+subString.Length < jsScript.Length)
                {
                 
                    //found "uci":"exampleMove"
                    if (AjaxStringHelper.IsSubStringInTheFirstSubStringOfString(str:jsScript,startIndex:i,
                        subString:subString))
                    {

                        
                        //get element in string  after " "uci":"e2e4" " 
                        string uciMove = AjaxStringHelper.GetStringBetweenTwoChar(
                            str: jsScript.Substring(i+subString.Length-1, jsScript.Length - i),
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
            ChessGameProperties.PieceColor playerColor=ChessGameProperties.PieceColor.white;
            string jsScript = OnlineChessGameStateDecoder.GetJavaScriptFromLichessHtmlCode(htmlCodes);
            //the color of the player is found on ("player":"black") when loading javascript
            for(int i = 0; i < jsScript.Length; i++)
            {
                //extracting playerColor
                if (i < jsScript.Length - 6)
                {
                    //if(jsScript[i]=='p')
                }
            }
            return playerColor;
        }

    }

}

