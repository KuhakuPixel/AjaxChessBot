using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace AjaxDynamicHtmlReader
{
    public class UciChessEngineProcess
    {
        ProcessStartInfo processStartInfo;
        public  UciChessEngineProcess(string filename)
        {
            processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = filename;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
           
        }
        /// <summary>
        /// timeout in milisecond
        /// </summary>
        /// <param name="command"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string SendCommandAndReceiveOutput(string command,int timeout)
        {
            
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
                    process.StandardInput.WriteLine(command);
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    if (process.WaitForExit(timeout) &&
                        outputWaitHandle.WaitOne(timeout) &&
                        errorWaitHandle.WaitOne(timeout))
                    {
                        if (process.ExitCode == 0)
                        {
                            //Console.WriteLine("Process sucsessfull");
                            return output.ToString();
                        }
                        return error.ToString();
                    }
                    else
                    {
                        //Console.WriteLine("Timeout ,output is :" + output.ToString());
                        return output.ToString();

                        // Timed out.
                    }
                }
            }
            
        }
    }
}
