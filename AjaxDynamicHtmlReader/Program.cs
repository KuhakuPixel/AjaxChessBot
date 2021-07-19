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
            TestSendInputAndGetOutputFromProcess(@"C:\Users\Nicho\Downloads\StockFish\stockfish_14_win_x64_avx2\stockfish_14_x64_avx2.exe");
        }
        static void TestSendInputAndGetOutputFromProcess(string pathToExe)
        {
            Console.WriteLine("Starting process");

            // Start the Sort.exe process with redirected input.
            // Use the sort command to sort the input text.
            using (Process myProcess = new Process())
            {
                myProcess.StartInfo.FileName = pathToExe;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.Start();

                StreamWriter myStreamWriter = myProcess.StandardInput;

                // Prompt the user for input text lines to sort.
                // Write each line to the StandardInput stream of
                // the sort command.
                String command;
                Console.Write("Enter command:");
                command = Console.ReadLine();
                myStreamWriter.WriteLine(command);
                string standard_output="";
                while ((myProcess.StandardOutput.ReadLine()) != null)
                {
                    Console.WriteLine(myProcess.StandardOutput.ReadLine());
                }
                // End the input stream to the sort command.
                // When the stream closes, the sort command
                // writes the sorted text lines to the
                // console.
                myStreamWriter.Close();
                Console.WriteLine(standard_output);
             
                
               

                // Wait for the sort process to write the sorted text lines.
                myProcess.WaitForExit();
            }
         
        }
        static void ReadLichessMoveFromGame()
        {
            Console.Write("Enter lichess link:");
            string lichessGameLink = Console.ReadLine();
            ChessGameState chessGameState = new ChessGameState(lichessGameLink);
            int lastMoveCount = 0;
            while (true)
            {
                List<string> currentMoves = chessGameState.GetCurrentMoves();
                //if (currentMoves.Count > lastMoveCount)
                // {

                Console.WriteLine("Current Move");
                for (int i = 0; i < currentMoves.Count; i++)
                {
                    Console.WriteLine((i + 1).ToString() + ". " + currentMoves[i]);
                }

                lastMoveCount = currentMoves.Count;
                //   }

            }
        }
    }
}

