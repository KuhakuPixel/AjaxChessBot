using System;
using System.Collections.Generic;
using System.Text;
using AjaxChessBotHelperLib;
namespace AjaxDynamicHtmlReader
{
    class ChessMovesDecoder
    {
        public static string GetJavaScriptFromLichessHtmlCode(List<string> htmlCodes)
        {
            for (int i = htmlCodes.Count-1; i>=0; i--)
            {
                //found js
                if (htmlCodes[i].Contains("lichess.load.then"))
                {
                    return htmlCodes[i];
                }
            }
            throw new ArgumentException("No JavaScript found in the html codes");

        }
        public static List<string> DecodeLichessMove(string lichessGameUrl)
        {
            List<string> htmlCodes = AjaxHtmlReader.ReadAndProcessHtmlSource(lichessGameUrl, includeContentInsideTag: false);
            string jsScript = GetJavaScriptFromLichessHtmlCode(htmlCodes);
            List<string> uciMoves = new List<string>();
            //starts scanning for UCI(Universal chess interface )
            for (int i = 0; i < jsScript.Length; i++)
            {

                if (i < jsScript.Length - 3)
                {
                    //found uci ,scan for move
                    if (jsScript[i] == 'u' && jsScript[i + 1] == 'c' && jsScript[i + 2] == 'i')
                    {
                        //extracting uci move
                        //Example: "uci":"e2e4"
                        for (int j = i + 3; j < jsScript.Length; j++)
                        {
                            // find data in the string after semi colon
                            if (jsScript[j] == ':')
                            {

                                string uciMove=AjaxStringHelper.GetStringBetweenTwoChar(
                                    str: jsScript.Substring(j, jsScript.Length - j),
                                    charStart: '"',
                                    charEnd: '"',
                                    isInclusive: false
                                    );
                                
                                uciMoves.Add(uciMove);
                                break;
                            }

                        }
                    }
                }

            }

            //always remove the first entry since in lichess it is always " play0 ,uci:null"
            if (uciMoves.Count > 0)
                uciMoves.RemoveAt(0);
            return uciMoves;

        }


    }
}

