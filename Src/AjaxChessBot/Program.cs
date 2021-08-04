using System;
using System.Collections.Generic;
using AjaxChessBotHelperLib;
namespace AjaxChessBot
{
    class Program
    {
        static private UciChessEngineProcess uciChessEngineProcess;
        static private AjaxConfigFile ajaxConfigFile=new AjaxConfigFile();
        static private Random random = new Random();
       
        static void Main(string[] args)
        {
            RunProgram();
        }
        static void RunProgram()
        {
            PrintWelcome();
            LoadConfig();

            while (true)
            {
                Console.Write("Command: ");
                string command = Console.ReadLine();
                if (command == "help")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("play              Run the bot(need to use the register command first)     ");
                    Console.WriteLine("import            Import the chess engine             ");
                    Console.WriteLine("save              Save The current configuration of the program     ");
                    Console.WriteLine("register          Register the board coordinate in the screen             ");
                    Console.WriteLine("randomThinkTime   if set to 'y' then it will have a random thinking time that can be changed by the user ");
                    Console.WriteLine("                  if set to 'n' then it will have a fixed thinking time that can be changed by the user ");
                    Console.WriteLine("set_time_normal   set Consistent time move in milisecond (min time:250 ms)        ");
                    Console.WriteLine("set_time_random   Choose Time to make a move between a range of time in milisecond   ");

                    Console.ForegroundColor = ConsoleColor.White;

                }
                else if (command == "save")
                {
                    ajaxConfigFile.Save();
                }
                else if (command == "import")
                {
                    Console.Write("Path to chess engine (Must be UCI compatable) : ");
                    string pathToEngine = Console.ReadLine();
                    ajaxConfigFile.pathToEngine = pathToEngine;
                    uciChessEngineProcess = new UciChessEngineProcess(pathToEngine);
                }
                else if (command == "register")
                {
                    MouseOperation.MousePoint bottomLeftCoordinates = new MouseOperation.MousePoint(0, 0);
                    MouseOperation.MousePoint topRightCoordinates = new MouseOperation.MousePoint(0, 0);
                    Console.WriteLine("bottom Left of the board (Press enter to submit the position):");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        bottomLeftCoordinates = MouseOperation.GetCursorPosition();
                        Console.WriteLine("Registered sucsessfully");
                    }

                    Console.WriteLine("top right off the board (Press enter to submit the position):");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        topRightCoordinates = MouseOperation.GetCursorPosition();
                        Console.WriteLine("Registered sucsessfully");
                    }


                    ajaxConfigFile.chessBoardCoordinatePlayingWhite = MapToIndividualChessCoordinates(bottomLeftCoordinates, topRightCoordinates,
                        ChessGameProperties.PieceColor.white);
                    ajaxConfigFile.chessBoardCoordinatePlayingBlack = MapToIndividualChessCoordinates(bottomLeftCoordinates, topRightCoordinates,
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
                    else if (ajaxConfigFile.chessBoardCoordinatePlayingWhite.Count == 0 || ajaxConfigFile.chessBoardCoordinatePlayingBlack.Count == 0)
                    {
                        Console.WriteLine("board Position not registered , use the \"register\" command to register the position of the baord");
                    }
                    //valid and ready
                    else if (ajaxConfigFile.chessBoardCoordinatePlayingWhite.Count == 64 && ajaxConfigFile.chessBoardCoordinatePlayingBlack.Count == 64)
                    {
                        Console.Write("Lichess Game Link: ");
                        string lichessGameLink = Console.ReadLine();

                        ChessGameState chessGameState = new ChessGameState(lichessGameLink);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("the bot will now play the Game, press esc key to stop");
                        Console.ForegroundColor = ConsoleColor.White;

                        string lastMoves = "";
                        do
                        {
                            while (!Console.KeyAvailable)
                            {
                                string currentMoves = chessGameState.GetCurrentMovesFen();
                                ChessGameProperties.PieceColor currentTurn = chessGameState.GetCurrentTurn(currentMoves);
                                //move 
                                if (currentTurn == chessGameState.PlayerColor && currentMoves != lastMoves)
                                {


                                    string bestMove = "";
                                    if (ajaxConfigFile.randomizeThinkingTime)
                                    {
                                        int randomizedThinkingTime = random.Next(ajaxConfigFile.randomThinkTimeMin, ajaxConfigFile.randomThinkTimeMax);
                                        bestMove = uciChessEngineProcess.GetBestMove(currentMoves, randomizedThinkingTime);
                                    }
                                    else
                                    {
                                        bestMove = uciChessEngineProcess.GetBestMove(currentMoves, ajaxConfigFile.normalThinkTime);
                                    }

                                    //split moves like e2e4 or h7h8q to separate moves
                                    List<string> bestMoveSplitted = AjaxStringHelper.SplitStringToChunk(bestMove, 2, true);

                                    //move  the piece
                                    switch (chessGameState.PlayerColor)
                                    {
                                        case ChessGameProperties.PieceColor.white:
                                            {
                                                MouseOperation.MousePoint fromPoint = ajaxConfigFile.chessBoardCoordinatePlayingWhite[bestMoveSplitted[0]];
                                                MouseOperation.MousePoint toPoint = ajaxConfigFile.chessBoardCoordinatePlayingWhite[bestMoveSplitted[1]];

                                                MouseOperation.DragMouseAcross(fromPoint, toPoint);
                                                break;
                                            }


                                        case ChessGameProperties.PieceColor.black:
                                            {
                                                MouseOperation.MousePoint fromPoint = ajaxConfigFile.chessBoardCoordinatePlayingBlack[bestMoveSplitted[0]];
                                                MouseOperation.MousePoint toPoint = ajaxConfigFile.chessBoardCoordinatePlayingBlack[bestMoveSplitted[1]];
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
                        while (Console.ReadKey(true).Key != ConsoleKey.Escape);

                    }


                }
                else if (command == "set_time_normal")
                {
                    Console.Write("time: ");
                    string timeStr = Console.ReadLine();
                    int normalThinkTime;
                    if (int.TryParse(timeStr, out normalThinkTime))
                    {
                        ajaxConfigFile.normalThinkTime = normalThinkTime;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input must be a whole number");
                        Console.ForegroundColor = ConsoleColor.White;
                    }


                }
                else if (command == "set_time_random")
                {
                    Console.Write("time min(ms) (must at least be 250 ms): ");
                    string timeMinStr = Console.ReadLine();
                    int timeMin;
                    if (int.TryParse(timeMinStr, out timeMin))
                    {
                        if (timeMin < 250)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("timeMin must be bigger than 250 ms");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                        ajaxConfigFile.randomThinkTimeMin = timeMin;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input must be a whole number");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }


                    Console.Write("time max(ms): ");
                    string timeMaxStr = Console.ReadLine();
                    int timeMax;
                    if (int.TryParse(timeMaxStr, out timeMax))
                    {
                        if (timeMin > timeMax)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error:timeMin is bigger than timeMax!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        ajaxConfigFile.randomThinkTimeMax = timeMax;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input must be a whole number");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                }
                else if (command == "randomThinkTime")
                {
                    Console.Write("value(y/n):");
                    string value = Console.ReadLine();
                    if (value == "y")
                    {
                        ajaxConfigFile.randomizeThinkingTime = true;

                    }
                    else if (value == "n")
                    {
                        ajaxConfigFile.randomizeThinkingTime = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("value must be either y/n");
                        Console.ForegroundColor = ConsoleColor.White;
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
                    Console.WriteLine("Use \"help\" command to list all of possible command");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }
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
        static void LoadConfig()
        {
            try
            {
                ajaxConfigFile.Load();
                if (!string.IsNullOrEmpty(ajaxConfigFile.pathToEngine))
                {
                    uciChessEngineProcess = new UciChessEngineProcess(ajaxConfigFile.pathToEngine);

                }

            }
            catch (Exception e)
            {

            }
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
                            chessAlgebraicNotation = AjaxStringHelper.AlphabetIndexToChar(x) + (y + 1).ToString();
                            break;
                        case ChessGameProperties.PieceColor.black:
                            //reverse the order for black because bottom left when playing black is h8
                            chessAlgebraicNotation = AjaxStringHelper.AlphabetIndexToChar(7 - x) + (8 - y).ToString();
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

    }
}

