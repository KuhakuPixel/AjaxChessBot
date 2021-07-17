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
            ChessGameState chessGameState=new ChessGameState(lichessGameLink);
            int lastMoveCount = 0;
            while (true)
            {
                List<string> currentMoves =chessGameState.GetCurrentMoves();
                //if (currentMoves.Count > lastMoveCount)
                // {
                   
                    Console.WriteLine("Current Move");
                    for(int i=0;i<currentMoves.Count;i++)
                    {
                        Console.WriteLine((i+1).ToString()+". "+currentMoves[i]);
                    }

                    lastMoveCount = currentMoves.Count;
             //   }

            }
        }


    }
}
