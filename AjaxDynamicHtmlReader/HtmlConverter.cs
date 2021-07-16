using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxDynamicHtmlReader
{

    public class HtmlConverter
    {
       
        public static List<string> SplitHtmlByBrackets(string htmlCode, bool includeContentInsideTag = false)
        {
            List<string> splittedHtml = new List<string>();
        
            for (int i = 0; i < htmlCode.Length; i++)
            {
             
                //check element between ,after or before tag
                if (htmlCode[i]!='<'&&htmlCode[i]!='>')
                {
                    string contentBetweenTag = "";
                    for (int j = i; j < htmlCode.Length; j++)
                    {
                        
                        if (htmlCode[j] == '<')
                        {
                            i = j;
                           break;
                        }
                        else
                        {
                            contentBetweenTag += htmlCode[j];
                        }
                     
                    }
                    if (!string.IsNullOrEmpty(contentBetweenTag))
                        splittedHtml.Add(contentBetweenTag);
                    
                  
                }
                //encounter a tag
                if (htmlCode[i] == '<')
                {
                   
                    string contentInsideTag = "";
                    //scan all content inside  tag like </html> will return /html 
                    for (int j = i+1; j < htmlCode.Length; j++)
                    {

                        if (j < htmlCode.Length)
                        {
                            if (htmlCode[j] == '>')
                            {
                          
                                i = j;
                                break;
                            }
                            else
                            {
                                contentInsideTag += htmlCode[j];
                            }
                        }
                        
                    }

                    if (includeContentInsideTag&&!string.IsNullOrEmpty(contentInsideTag))
                        splittedHtml.Add(contentInsideTag);
                   
                }
            }
            
            return splittedHtml;
        }
    }
}
