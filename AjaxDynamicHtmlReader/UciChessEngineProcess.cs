using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace AjaxDynamicHtmlReader
{
    public class UciChessEngineProcess
    {
        public static void StartUciChessEngine(string pathToEngineExe)
        {
            Process process = new Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = false;
            
            // Setup executable and parameters
            process.StartInfo.FileName = pathToEngineExe;
            //process.StartInfo.Arguments = "--test";

            // Go
            process.Start();
            process.StandardInput.WriteLine("uci");
            process.StandardInput.Flush();

            string output=process.StandardOutput.ReadLine();


        }
    }
}
