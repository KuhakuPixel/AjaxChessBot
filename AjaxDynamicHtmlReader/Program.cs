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
            List<string> moves=LichessMovesDecoder.DecodeLichessMove("https://lichess.org/Dr9oJXcrsbHy");
            Console.ReadLine();
        }
       
     
    }
}
