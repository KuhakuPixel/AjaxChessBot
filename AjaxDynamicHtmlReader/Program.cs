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
           
            Console.ReadLine();
        }
        static void OutputConvertedHtmlToTextFileTest0(string path)
        {
            //split string by <> to a data structure
            // Open the file to read from.
            string lichessGameHtml = File.ReadAllText(@"C:\Users\Nicho\Desktop\Projects\AjaxChessBot\htmlCode.txt");
            List<string> splittedHtml = HtmlConverter.SplitHtmlByBrackets(lichessGameHtml, true);

            using (TextWriter textWriter = new StreamWriter("SavedList.txt"))
            {
                foreach (String s in splittedHtml)
                    textWriter.WriteLine(s);
            }
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
