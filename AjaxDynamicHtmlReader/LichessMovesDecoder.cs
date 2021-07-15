using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    class LichessMovesDecoder
    {
        string GetJavaScriptFromHtmlCode(List<string> htmlCodes)
        {
            for (int i = 0; i < htmlCodes.Count; i++)
            {
                //found js
                if (htmlCodes[i].Contains("lichess.load.then"))
                {
                    return htmlCodes[i];
                }
            }
            throw new ArgumentException("No JavaScript found in the html codes");

        }
        void DecodeChessMove(List<string> htmlCodes)
        {

            string jsScript = GetJavaScriptFromHtmlCode(htmlCodes);
            //starts scanning for UCI(Universal chess interface ) and san
            for (int i = 0; i < jsScript.Length; i++)
            {
                //found uci ,scan for move
                if (jsScript[i] == 'u' && jsScript[i + 1] == 'c' && jsScript[i + 2] == 'i')
                {
                    //Example: "uci":"e2e4"
                    for (int j = i + 3; j < jsScript.Length; j++)
                    {
                        // find data in the string after semi colon
                        if (jsScript[j] == ':')
                        {
                            int numOfQuote = 0;
                            for (int k = j + 1; k < jsScript.Length; k++)
                            {
                                
                                if (jsScript[k] == '"')
                                {
                                    numOfQuote++;
                                }
                            }
                        }

                    }
                }
            }

        }


    }
}

