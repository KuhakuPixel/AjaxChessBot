using System;
using System.Collections.Generic;
using AjaxChessBotHelperLib;
namespace AjaxChessBot
{
    class Program
    {
        static private UciChessEngineProcess uciChessEngineProcess;
        static private AjaxConfigFile ajaxConfigFile = new AjaxConfigFile();
        static private Random random = new Random();

        // TODO: don't make this a global var, it will be error prone and its ugly
        static private string previousChessMove = "";

        static void Main(string[] args)
        {
            RunProgram();
        }
        static void on_import_func()
        {

            Console.Write("Path to chess engine (Must be UCI compatable) : ");
            string pathToEngine = Console.ReadLine();
            ajaxConfigFile.pathToEngine = pathToEngine;
            uciChessEngineProcess = new UciChessEngineProcess(pathToEngine);
        }

        static void on_save_func()
        {
            ajaxConfigFile.Save();

        }
        static void on_register_func()
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
                ChessProperty.PieceColor.white);
            ajaxConfigFile.chessBoardCoordinatePlayingBlack = MapToIndividualChessCoordinates(bottomLeftCoordinates, topRightCoordinates,
                ChessProperty.PieceColor.black);
        }

        static void on_scan_func()
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
        static void run_chess_bot(ChessGameState chessGameState)
        {

            string currentMoves = chessGameState.GetCurrentMovesFen();
            ChessProperty.PieceColor currentTurn = chessGameState.GetCurrentTurn(currentMoves);
            if (currentTurn == chessGameState.PlayerColor && currentMoves != previousChessMove)
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
                MouseOperation.MousePoint fromPoint = new MouseOperation.MousePoint(0, 0);
                MouseOperation.MousePoint toPoint = new MouseOperation.MousePoint(0, 0);
                if (chessGameState.PlayerColor == ChessProperty.PieceColor.white)
                {

                    fromPoint = ajaxConfigFile.chessBoardCoordinatePlayingWhite[bestMoveSplitted[0]];
                    toPoint = ajaxConfigFile.chessBoardCoordinatePlayingWhite[bestMoveSplitted[1]];

                }
                else if (chessGameState.PlayerColor == ChessProperty.PieceColor.black)
                {

                    fromPoint = ajaxConfigFile.chessBoardCoordinatePlayingBlack[bestMoveSplitted[0]];
                    toPoint = ajaxConfigFile.chessBoardCoordinatePlayingBlack[bestMoveSplitted[1]];
                }

                        
                // in most chess websites. like lichess.org and chess.com
                // right click is used to draw arrow like left clicking e2 and e4 wil draw arrow betwee the two
                // left click is used to make a move between 2 square
                // this is hardcoded, but good enough for now
                if (ajaxConfigFile.advisorMode)
                {
                    MouseOperation.DragMouseRightClick(fromPoint, toPoint);
                }
                else
                {
                    MouseOperation.DragMouseLeftClick(fromPoint, toPoint);
                }
                //MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);
                Console.WriteLine(currentTurn + "'s turn: " + bestMove);
            }
            previousChessMove = currentMoves;
        }
        static void on_play_func()
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

                do
                {
                    while (!Console.KeyAvailable)
                    {
                        run_chess_bot(chessGameState);
                    }
                }
                while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            }



        }
        static void on_help_func()
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
            Console.WriteLine("toggle_advisor    enable/disable advisor mode (showing best move without moving them");
            Console.WriteLine("                  (EXPERIMENTAL FEAUTURES, MAY NOT WORK CORRECTLY)");

            Console.ForegroundColor = ConsoleColor.White;



        }

        static void on_set_time_normal_func()
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

        static void on_set_time_random_func()
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
                    return;
                }
                ajaxConfigFile.randomThinkTimeMin = timeMin;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input must be a whole number");
                Console.ForegroundColor = ConsoleColor.White;
                return;
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
            }

        }

        static void on_random_think_time_func()
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

        static void on_test_click_func()
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

        static void on_toggle_advisor_func()
        {
            if (ajaxConfigFile.advisorMode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Advisor mode is turned off");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Advisor mode is turned on");
                Console.ForegroundColor = ConsoleColor.White;

            }
            ajaxConfigFile.advisorMode = !ajaxConfigFile.advisorMode;

        }

        static void on_unknown_command_func(string command)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unknown command :" + command);
            Console.WriteLine("Use \"help\" command to list all of possible command");
            Console.ForegroundColor = ConsoleColor.White;

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
                    on_help_func();
                }
                else if (command == "save")
                {
                    on_save_func();
                }
                else if (command == "import")
                {
                    on_import_func();
                }
                else if (command == "register")
                {
                    on_register_func();

                }
                else if (command == "scan")
                {
                    on_scan_func();
                }
                else if (command == "play")
                {
                    on_play_func();
                }
                else if (command == "set_time_normal")
                {
                    on_set_time_normal_func();
                }
                else if (command == "set_time_random")
                {
                    on_set_time_random_func();
                }
                else if (command == "randomThinkTime")
                {
                    on_random_think_time_func();

                }
                else if (command == "testclick")

                {
                    on_test_click_func();
                }
                else if (command == "toggle_advisor")

                {
                    on_toggle_advisor_func();
                }
                else
                {
                    on_unknown_command_func(command);
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
            (MouseOperation.MousePoint bottomLeftPoint, MouseOperation.MousePoint topRightPoint, ChessProperty.PieceColor playerPieceColor)
        {

            //note the center is at top left and y is max at bottom and 0 at top
            //
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
                        case ChessProperty.PieceColor.white:
                            chessAlgebraicNotation = AjaxStringHelper.AlphabetIndexToChar(x) + (y + 1).ToString();
                            break;
                        case ChessProperty.PieceColor.black:
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

