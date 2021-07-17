using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace AjaxDynamicHtmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter lichess link:");
            string lichessGameLink = Console.ReadLine();
            int lastMoveCount = 0;
            while (true)
            {
                List<string> currentMoves = ChessMovesDecoder.DecodeLichessMove(lichessGameLink);
                //if (currentMoves.Count > lastMoveCount)
                // {
                   
                    Console.WriteLine("Current Move");
                    for(int i=0;i<currentMoves.Count;i++)
                    {
                        Console.WriteLine(i.ToString()+". "+currentMoves[i]);
                    }

                    lastMoveCount = currentMoves.Count;
             //   }

            }
        }


    }
}
