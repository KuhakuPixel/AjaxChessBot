﻿using System;
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
            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinate = new Dictionary<string, MouseOperation.MousePoint>();
            while (true)
            {
                Console.Write("Command: ");
                string command = Console.ReadLine();
                if (command == "register")
                {
                    MouseOperation.MousePoint bottomLeftCoordinates = new MouseOperation.MousePoint(0, 0);
                    MouseOperation.MousePoint topRightCoordinates = new MouseOperation.MousePoint(0, 0);
                    Console.WriteLine("bottom Left of the board:");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        bottomLeftCoordinates = MouseOperation.GetCursorPosition();
                        Console.WriteLine("Registered sucsessfully");
                    }

                    Console.WriteLine("top right off the board:");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        topRightCoordinates = MouseOperation.GetCursorPosition();
                        Console.WriteLine("Registered sucsessfully");
                    }
                    chessBoardCoordinate = MapToIndividualCoordinates(bottomLeftCoordinates, topRightCoordinates);

                }
                else if (command == "scan")
                {
                    do
                    {
                        while (!Console.KeyAvailable)
                        {
                            MouseOperation.MousePoint point = MouseOperation.GetCursorPosition();
                            Console.WriteLine("Current Coordinate:(" + point.X + "," + point.Y + ")");
                        }
                    } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
                }
            }

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

        public static Dictionary<string, MouseOperation.MousePoint> MapToIndividualCoordinates
            (MouseOperation.MousePoint bottomLeftPoint, MouseOperation.MousePoint topRightPoint)
        {

            //note the center is at top left and y is max at bottom and 0 at top

            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinates =
                new Dictionary<string, MouseOperation.MousePoint>();




            int boardLength = Math.Abs(topRightPoint.X - bottomLeftPoint.X);
            int boardHeight = Math.Abs(topRightPoint.Y - bottomLeftPoint.Y);

            //will start center of the bottom left square
            MouseOperation.MousePoint coordinateIteratorStart = new MouseOperation.MousePoint(bottomLeftPoint.X + boardLength / 16,
                bottomLeftPoint.Y - boardHeight / 16);

            MouseOperation.MousePoint coordinateIterator = coordinateIteratorStart;

            for (int h = 0; h < 8; h++)
            {
                for (int w = 0; w < 8; w++)
                {
                    string chessAlgebraicNotation = AjaxStringHelper.GetCharByAlphabetIndex(w) + h.ToString();
                    chessBoardCoordinates.Add(chessAlgebraicNotation, coordinateIterator);
                    Console.WriteLine(chessAlgebraicNotation + ": " + coordinateIterator.X.ToString() + "," + coordinateIterator.Y.ToString());
                    coordinateIterator.X += boardLength / 8;

                }
                coordinateIterator.X = coordinateIteratorStart.X;
                coordinateIterator.Y -= boardHeight / 8;
                Console.WriteLine("");
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

