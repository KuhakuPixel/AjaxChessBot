using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AjaxChessBot
{
    
     /// </summary>
     /// Wrapper of the process class of .net because there is a bug in reading operation such as 
     /// StandardOutput.peek StandardOutput.readline and ect will
     /// sometimes hang on empty output buffer which is unacceptable since when trying 
     /// to communicate with a chess engine it will hang the main thread
     /// forever and it need to handle such cases when it hangs
     /// </summary>
    public class ProcessWrapper
    {
        Process process;
        StreamWriter processInputStream;
        public ProcessWrapper(string path_to_exe)
        {
            /*
                    initialize process while redirecting stdout and stdin in
                    and starts the process in another thread
            */
            if (!File.Exists(path_to_exe))
            {
                throw new FileNotFoundException("file : " + path_to_exe + " doesn't exist when initializing ProcessWrapper");
            }
            this.process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            //
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            //
            process.StartInfo.FileName = path_to_exe;

        }
        /// <summary>
        /// Start Process in another thread to avoid blocking thread on process.WaitForExit()
        /// </summary>
        public void Start()
        {
            process.Start();
            //assign an input stream after the process has been started
            this.processInputStream = process.StandardInput;
        }
        public void SendInputAndDelay(string input, int millisecondsDelay)
        {
            this.processInputStream.WriteLine(input);
            Thread.Sleep(millisecondsDelay);
        }
        public List<string> GetOutput()
        {
            // get output
            Func<List<string>> getOutputFunc = () =>
            {

                List<string> output = new List<string>();
                while (process.StandardOutput.Peek() > -1)
                {
                    string line = process.StandardOutput.ReadLine();
                    output.Add(line);
                }

                return output;
            };
            var output_task = Task.Run(getOutputFunc);
            if (output_task.Wait(TimeSpan.FromSeconds(1000000)))
                return output_task.Result;
            else
                throw new Exception("Process hangs when receiving output because of empty output bufer");

        }
        public void Stop()
        {
            this.process.Kill();
        }
    }
}
