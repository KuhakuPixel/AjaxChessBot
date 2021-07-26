using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using AjaxChessBotHelperLib;
namespace AjaxDynamicHtmlReader
{
    public class UciChessEngineProcess
    {
        public class CommandAndTime
        {
            public string command;
            public int time;
            public CommandAndTime(string command,int time)
            {
                this.command = command;
                this.time = time;
            }
        }
        ProcessStartInfo processStartInfo;
        public  UciChessEngineProcess(string filename)
        {
         
            
            processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = filename;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;

            List<string> output = this.SendCommandAndReceiveOutput(new List<CommandAndTime> {
                new CommandAndTime("uci",1000),
            }) ;
            if (output.Count > 0)
            {
                if (output[output.Count - 1] == "uciok")
                {
                    Console.WriteLine("Chess engine is compatable with the program");
                }
                else
                {
                    throw new Exception("Engine is not uci compatable");
                    
                }
            }

        }
        /// <summary>
        /// maxThinkingTime is in ms
        /// </summary>
        /// <param name="chessGameState"></param>
        /// <param name="maxThinkingTime"></param>
        /// <returns></returns>
        public string GetBestMove(string currenntFenPosition,int maxThinkingTime)
        {
            List<string> outputs = new List<string>();
          
            outputs = SendCommandAndReceiveOutput(new List<CommandAndTime> {
                new CommandAndTime("position" + " " + "fen" + " " + currenntFenPosition, 10),
                new CommandAndTime("go", maxThinkingTime),
                new CommandAndTime("stop",10),
            }
            ); 
        
            //when stop is sent , a uci enigne should send 
            //bestmove [exampleMove] ponder [exampleMove]
            return outputs[outputs.Count-1].Split(' ')[1];
        }
        /// <summary>
        /// timeout in milisecond
        /// </summary>
        /// <param name="command"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public List<string> SendCommandAndReceiveOutput(List<CommandAndTime> commandAndTimes)
        {
            int timeout = 10;

            for (int i = 0; i< commandAndTimes.Count; i++){
                timeout += commandAndTimes[i].time;
            }
            StringBuilder output = new StringBuilder();
            StringBuilder error = new StringBuilder();
            
            using (Process process = new Process())
            {
                process.StartInfo = this.processStartInfo;
              

                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            output.AppendLine(e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            error.AppendLine(e.Data);
                        }
                    };

                    process.Start();
                    for(int i = 0; i < commandAndTimes.Count; i++)
                    {
                        process.StandardInput.WriteLine(commandAndTimes[i].command);
                        Thread.Sleep(commandAndTimes[i].time);
                    }
                  
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    if (process.WaitForExit(timeout) &&
                        outputWaitHandle.WaitOne(timeout) &&
                        errorWaitHandle.WaitOne(timeout))
                    {
                        if (process.ExitCode == 0)
                        {
                            //Console.WriteLine("Process sucsessfull");
                            return output.ToList();
                        }
                        else
                        {
                            return error.ToList();
                        }
                     
                    }
                    else
                    {
                        //Console.WriteLine("Timeout ,output is :" + output.ToString());
                        return output.ToList();

                        // Timed out.
                    }
                }
            }
            
        }
        
    }
}
