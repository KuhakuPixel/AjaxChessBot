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
        static private UciChessEngineProcess uciChessEngineProcess;
        static void PrintWelcome()
        {
         
            Console.ForegroundColor = ConsoleColor.Blue;
                              
            Console.WriteLine("░█████╗░░░░░░██╗░█████╗░██╗░░██╗  ██████╗░░█████╗░████████╗");
            Console.WriteLine("██╔══██╗░░░░░██║██╔══██╗╚██╗██╔╝  ██╔══██╗██╔══██╗╚══██╔══╝");
            Console.WriteLine("███████║░░░░░██║███████║░╚███╔╝░  ██████╦╝██║░░██║░░░██║░░░");
            Console.WriteLine("██╔══██║██╗░░██║██╔══██║░██╔██╗░  ██╔══██╗██║░░██║░░░██║░░░");
            Console.WriteLine("██║░░██║╚█████╔╝██║░░██║██╔╝╚██╗  ██████╦╝╚█████╔╝░░░██║░░░");
            Console.WriteLine("╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝  ╚═════╝░░╚════╝░░░░╚═╝░░░");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   Created by NicholasPixel");

            Console.WriteLine("AjaxBot is open source ");
            Console.WriteLine("Contribute at:");
            Console.WriteLine("https://github.com/KuhakuPixel/AjaxChessBot");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DISCLAIMER!!!: Developers are not responsible if your account ");
            Console.WriteLine("               Is banned when using this program");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Type help For turtorial ");

            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {


            PrintWelcome();

           
            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingWhite = new Dictionary<string, MouseOperation.MousePoint>();
            Dictionary<string, MouseOperation.MousePoint> chessBoardCoordinatePlayingBlack = new Dictionary<string, MouseOperation.MousePoint>();
            while (true)
            {
                Console.Write("Command: ");
                string command = Console.ReadLine();
                if (command == "help")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("import     Import the chess engine             ");
                    Console.WriteLine("register   Register the board coordinate in the screen             ");
                    Console.WriteLine("play       Run the bot(need to use the register command first)     ");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else if (command == "import")
                {
                    Console.Write("Path to chess engine (Must be UCI compatable) : ");
                    string pathToEngine=Console.ReadLine();
                    uciChessEngineProcess = new UciChessEngineProcess(pathToEngine);
                }
                else if (command == "register")
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

                    
                    chessBoardCoordinatePlayingWhite = MapToIndividualChessCoordinates(bottomLeftCoordinates, topRightCoordinates,
                        ChessGameProperties.PieceColor.white);
                    chessBoardCoordinatePlayingBlack = MapToIndividualChessCoordinates(bottomLeftCoordinates, topRightCoordinates,
                        ChessGameProperties.PieceColor.black);

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
                else if (command == "play")
                {
                    if (uciChessEngineProcess == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chess Engine hasn't been imported  use \"import\" command to import the engine");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (chessBoardCoordinatePlayingWhite.Count == 0 || chessBoardCoordinatePlayingBlack.Count == 0)
                    {
                        Console.WriteLine("board Position not registered , use the \"register\" command to register the position of the baord");
                    }
                    //valid and ready
                    else if (chessBoardCoordinatePlayingWhite.Count == 64&& chessBoardCoordinatePlayingBlack.Count == 64)
                    {
                        Console.Write("Lichess Game Link");
                        string lichessGameLink = Console.ReadLine();
                        ChessGameState chessGameState = new ChessGameState(lichessGameLink);

                        string lastMoves = "";
                        while (true)
                        {
                            string currentMoves = chessGameState.GetCurrentMovesFen();
                            ChessGameProperties.PieceColor currentTurn = chessGameState.GetCurrentTurn(currentMoves);
                            //move 
                            if (currentTurn == chessGameState.PlayerColor && currentMoves != lastMoves)
                            {
                                string bestMove = uciChessEngineProcess.GetBestMove(currentMoves, 1000);

                                //split moves like e2e4 or h7h8q to separate moves
                                List<string> bestMoveSplitted = AjaxStringHelper.SplitStringToChunk(bestMove, 2, true);

                                //move  the piece
                                switch (chessGameState.PlayerColor)
                                {
                                    case ChessGameProperties.PieceColor.white:
                                        {
                                            MouseOperation.MousePoint fromPoint = chessBoardCoordinatePlayingWhite[bestMoveSplitted[0]];
                                            MouseOperation.MousePoint toPoint = chessBoardCoordinatePlayingWhite[bestMoveSplitted[1]];
                                            MouseOperation.DragMouseAcross(fromPoint, toPoint);
                                            break;
                                        }


                                    case ChessGameProperties.PieceColor.black:
                                        {
                                            MouseOperation.MousePoint fromPoint = chessBoardCoordinatePlayingBlack[bestMoveSplitted[0]];
                                            MouseOperation.MousePoint toPoint = chessBoardCoordinatePlayingBlack[bestMoveSplitted[1]];
                                            MouseOperation.DragMouseAcross(fromPoint, toPoint);
                                            break;
                                        }



                                }
                                //MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);
                                Console.WriteLine(currentTurn + "'s turn: " + bestMove);
                            }
                            lastMoves = currentMoves;


                        }
                    }
                   
                   
                }
                else if (command == "testclick")
                {
                    do
                    {
                        while (!Console.KeyAvailable)
                        {
                            MouseOperation.MousePoint currentCoordinate = MouseOperation.GetCursorPosition();
                            Console.WriteLine("Clicking at: " + currentCoordinate.X.ToString() + "," + currentCoordinate.Y.ToString());
                            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftDown);
                            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);
                            Console.WriteLine("test");
                        }
                    }
                    while (Console.ReadKey(true).Key != ConsoleKey.Escape);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command :" + command);
                    Console.WriteLine("Use \"help command to list all of possible command\"");
                    Console.ForegroundColor = ConsoleColor.White;
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

        public static Dictionary<string, MouseOperation.MousePoint> MapToIndividualChessCoordinates
            (MouseOperation.MousePoint bottomLeftPoint, MouseOperation.MousePoint topRightPoint, ChessGameProperties.PieceColor playerPieceColor)
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

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    string chessAlgebraicNotation = "";
                    switch (playerPieceColor)
                    {
                        case ChessGameProperties.PieceColor.white:
                            chessAlgebraicNotation = AjaxStringHelper.GetCharByAlphabetIndex(x) + (y + 1).ToString();
                            break;
                        case ChessGameProperties.PieceColor.black:
                            //reverse the order for black because bottom left when playing black is h8
                            chessAlgebraicNotation = AjaxStringHelper.GetCharByAlphabetIndex(7-x) + (8 - y).ToString();
                            break;
                    }
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

