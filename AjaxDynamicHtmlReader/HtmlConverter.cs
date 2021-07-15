using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{
    
    class HtmlConverter
    {
        public static List<string> SplitHtmlByBrackets(string htmlCode, bool includeContentBetweenTag = false)
        {

            List<string> splittedHtml = new List<string> { };

            string contentBetweenTag = "";
            for (int i = 0; i < htmlCode.Length; i++)
            {

                if (includeContentBetweenTag&&htmlCode[i]!='<'&&htmlCode[i]!='>')
                {
                    contentBetweenTag += htmlCode[i];
                }
                else if (htmlCode[i] == '<')
                {
                    if (includeContentBetweenTag&&!string.IsNullOrEmpty(contentBetweenTag))
                    {
                        splittedHtml.Add(contentBetweenTag);
                        contentBetweenTag = "";
                    }
                    string contentInBrackets = "";
                    //scan all content in bracket
                    for (int j = i; j < htmlCode.Length; j++)
                    {

                        
                        if (htmlCode[j] != '<' && htmlCode[j] != '>')
                        {
                           
                            contentInBrackets += htmlCode[j];
                        }
                        else if (htmlCode[j] == '>')
                        {
                          
                            break;
                        }
                        i++;
                    }
                    splittedHtml.Add(contentInBrackets);

                }
               
            }
            return splittedHtml;
        }
    }
}
