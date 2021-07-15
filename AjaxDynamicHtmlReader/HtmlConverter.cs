using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    class HtmlConverter
    {
        public static List<string> SplitHtmlByBrackets(string htmlCode)
        {

            List<string> splittedHtml = new List<string> { };

            for (int i = 0; i < htmlCode.Length; i++)
            {

                if (htmlCode[i] == '<')
                {
                    string contentInBrackets = "";
                    //scan all content in bracket
                    for (int j = i; j < htmlCode.Length; j++)
                    {
                        i++;
                        if (htmlCode[j] != '<' && htmlCode[j] != '>')
                        {
                            contentInBrackets += htmlCode[j];
                        }
                        else if (htmlCode[j] == '>')
                        {
                            break;
                        }
                    }
                    splittedHtml.Add(contentInBrackets);

                }

            }
            return splittedHtml;
        }
    }
}
