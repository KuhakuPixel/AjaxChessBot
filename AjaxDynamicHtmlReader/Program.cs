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
            Console.WriteLine("Enter lichess link:");
            string lichessGameLink = Console.ReadLine();
            int lastMoveCount = 0;
            while (true)
            {
                List<string> currentMoves = LichessMovesDecoder.DecodeLichessMove(lichessGameLink);
                if (currentMoves.Count > lastMoveCount)
                {
                    Console.Write("Current Move");
                    foreach (string move in currentMoves)
                    {
                        Console.WriteLine(move);
                    }

                    lastMoveCount = currentMoves.Count;
                }

            }
        }


    }
}
