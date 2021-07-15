using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
namespace AjaxDynamicHtmlReader
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(GetHtml("https://lichess.org/oTcd3aEnOMW3"));
            //Console.WriteLine(GetHtml("https://lichess.org/DKMH31T8yZW0"));
            // Console.WriteLine(GetHtml("https://www.chess.com/game/live/20004058603"));
            //Console.WriteLine(GetHtml2("https://www.chess.com/game/live/20004058603"));
            //Console.WriteLine(GetHtml3("https://lichess.org/LVCOwlWd"));



            //OutputHtmlToText("https://lichess.org/LVCOwlWd", @"C:\Users\Nicho\Desktop\Projects\AjaxChessBot\htmlCode.txt");
            //split string by <> to a data structure
            List<string> splittedHtml=HtmlConverter.SplitHtmlByBrackets("<Hi from the other> <Side Of The world>");

            Console.ReadLine();
        }
        static string GetHtml(string url)
        {

            string result = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        result = content.ReadAsStringAsync().Result;
                    }
                }
            }
            return result;
        }
        static string GetHtml2(string url)
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
        static string GetHtml3(string url)
        {

            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }
            return htmlCode;
        }
        static void OutputHtmlToText(string url,string pathToWriteTo)
        {
            string htmlCode = GetHtml2(url);
            File.WriteAllText(pathToWriteTo, htmlCode);
            Console.WriteLine("Output Html code sucsessfull");
        }
    }
}
