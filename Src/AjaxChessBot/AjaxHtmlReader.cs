using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
namespace AjaxChessBot
{

    public class AjaxHtmlReader
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
                        
                        if (htmlCode[j] == '<'||j==htmlCode.Length-1)
                        {
                            if (htmlCode[j] != '<')
                            {
                                contentBetweenTag += htmlCode[j];
                            }
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
        

        public static List<string> ReadAndProcessHtmlSource(string url,bool includeContentInsideTag)
        {
            string htmlCode = GetHtml(url);
            List<string> processedHtml = AjaxHtmlReader.SplitHtmlByBrackets(htmlCode, includeContentInsideTag);
            return processedHtml;
        }

        public static string GetHtml(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream,
                        Encoding.GetEncoding(response.CharacterSet));
                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            return data;
        }
    }
}
