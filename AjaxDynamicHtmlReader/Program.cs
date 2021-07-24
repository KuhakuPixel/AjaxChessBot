using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace AjaxDynamicHtmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSendInputAndGetOutputFromProcess(@"C:\Users\Nicho\Downloads\StockFish\stockfish_14_win_x64_avx2\stockfish_14_x64_avx2.exe");
            //FindBestMoveFromLichessGame();
            // Console.Write("Link");
            //string gamelink=Console.ReadLine();
            //ChessGameState gameState = new ChessGameState(gamelink);
            //FindBestMoveFromLichessGame();
            while (true)
            {
                MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);
                MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftDown);
            }
        }
        

        static void FindBestMoveFromLichessGame()
        {
            UciChessEngineProcess uciChessEngineProcess = new UciChessEngineProcess(
         @"C:\Users\Nicho\Downloads\StockFish\stockfish_14_win_x64_avx2\stockfish_14_x64_avx2.exe");
            Console.Write("Enter lichess link:");
        
            string lichessGameLink = Console.ReadLine();
            ChessGameState chessGameState = new ChessGameState(lichessGameLink);

            string lastMoves ="";
            while (true)
            {
                string currentMoves = chessGameState.GetCurrentMovesFen();
                ChessGameProperties.PieceColor currentTurn = chessGameState.GetCurrentTurn(currentMoves);
                if (currentTurn == chessGameState.PlayerColor&&currentMoves!=lastMoves)
                {
                    string bestMove = uciChessEngineProcess.GetBestMove(currentMoves, 1000);
                    Console.WriteLine(currentTurn + "'s turn: " + bestMove);
                }
                lastMoves = currentMoves;
               

            }
        }
        

    }
}

