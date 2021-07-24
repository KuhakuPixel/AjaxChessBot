using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Windows.Input;
using AjaxChessBotHelperLib;
namespace AjaxDynamicHtmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            MapToIndividualCoordinates(new List<MouseOperation.MousePoint>
            {
                new MouseOperation.MousePoint(0,0),
                new MouseOperation.MousePoint(600,600),
            }) ;
            /*
            List<MouseOperation.MousePoint> points=new List<MouseOperation.MousePoint>();
            //lets work on the system to save the coordinates
            for(int i = 0; i < 2; i++)
            {
                Console.WriteLine("Pos");
                if(Console.ReadKey(false).Key==ConsoleKey.Enter)
                {
                    points.Add(MouseOperation.GetCursorPosition());
                }
            }
            */
            /*
            do
            {
                while (!Console.KeyAvailable)
                {
                    MouseOperation.MousePoint point =MouseOperation.GetCursorPosition();
                    Console.WriteLine("Current Coordinate:("+point.X+","+point.Y+")");
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            */



        }
        public static  Dictionary<string,MouseOperation.MousePoint> MapToIndividualCoordinates
            (List<MouseOperation.MousePoint> coordinates)
        {

            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinates =
                new Dictionary<string, MouseOperation.MousePoint>();

            if (coordinates.Count != 2)
            {
                throw new ArgumentException("coordinates 's length must be exactly 2");
            }
            MouseOperation.MousePoint bottomLeftPoint=coordinates[0];
            MouseOperation.MousePoint topRightPoint = coordinates[1];

            //
            int boardLength=topRightPoint.X - bottomLeftPoint.X;
            int boardHeight = topRightPoint.Y - bottomLeftPoint.Y;

            //will start center of the bottom left square
            MouseOperation.MousePoint coordinateIterator = new MouseOperation.MousePoint(boardLength/16,boardHeight/16);
        
            for(int h = 0; h < 8; h++)
            {
                for(int w = 0;w < 8; w++)
                {
                    string chessAlgebraicNotation=AjaxStringHelper.GetCharByAlphabetIndex(w)+h.ToString();
                    chessBoardCoordinates.Add(chessAlgebraicNotation, coordinateIterator);
                    Console.WriteLine(chessAlgebraicNotation + ": " + coordinateIterator.X.ToString()+","+coordinateIterator.Y.ToString());
                    coordinateIterator.X += boardLength / 8;
                    
                }
                coordinateIterator.X = boardLength / 16;
                coordinateIterator.Y += boardHeight / 8;
            }
            return chessBoardCoordinates;
        }
        
        static void FindBestMoveFromLichessGame()
        {
            UciChessEngineProcess uciChessEngineProcess = new UciChessEngineProcess(
         @"C:\Users\Nicho\Downloads\StockFish\stockfish_14_win_x64_avx2\stockfish_14_x64_avx2.exe");
            Console.Write("Enter lichess link:");

            string lichessGameLink = Console.ReadLine();
            ChessGameState chessGameState = new ChessGameState(lichessGameLink);

            string lastMoves = "";
            while (true)
            {
                string currentMoves = chessGameState.GetCurrentMovesFen();
                ChessGameProperties.PieceColor currentTurn = chessGameState.GetCurrentTurn(currentMoves);
                if (currentTurn == chessGameState.PlayerColor && currentMoves != lastMoves)
                {
                    string bestMove = uciChessEngineProcess.GetBestMove(currentMoves, 1000);
                    Console.WriteLine(currentTurn + "'s turn: " + bestMove);
                }
                lastMoves = currentMoves;


            }
        }


    }
}

