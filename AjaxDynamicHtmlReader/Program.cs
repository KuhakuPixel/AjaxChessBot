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
            ReadProcessesedHtml("https://lichess.org/mhogedz2", @"C:\Users\Nicho\Desktop\Projects\AjaxChessBot\htmlCodeLichessFightChessBotInProgress.txt");
            Console.ReadLine();
        }
        static void ReadProcessesedHtml(string url,string outputPath)
        {
            string htmlCode= GetHtml2(url);
            List<string> splittedHtml = HtmlConverter.SplitHtmlByBrackets(htmlCode, true);

            using (TextWriter textWriter = new StreamWriter(outputPath))
            {
                foreach (String s in splittedHtml)
                    textWriter.WriteLine(s);
            }
            Console.WriteLine("Html sucessfuly read and is written to :" +outputPath);
        }
        static void OutputConvertedHtmlToTextFileTest0(string path)
        {
            //split string by <> to a data structure
            // Open the file to read from.
            string lichessGameHtml = File.ReadAllText(@"C:\Users\Nicho\Desktop\Projects\AjaxChessBot\htmlCode.txt");
            List<string> splittedHtml = HtmlConverter.SplitHtmlByBrackets(lichessGameHtml, true);

            using (TextWriter textWriter = new StreamWriter(path))
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
