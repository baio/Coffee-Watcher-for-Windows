using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeWatcher
{
    //http://sandeep-aparajit.blogspot.com/2008/04/how-to-execute-command-in-c.html
    public class ExecuteCmd
    {

        public static string Sync(string Cmd, string arguments)
        {
            ExecuteCmd exe = new ExecuteCmd();
            
            return exe.ExecuteCommandSync(Cmd, arguments);
        }

        /// <summary>

        /// Executes a shell command synchronously.

        /// </summary>

        /// <param name="command">string command</param>

        /// <returns>string, as output of the command.</returns>

        public string ExecuteCommandSync(string command, string arguments)
        {


                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.

                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.

                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(command);

                procStartInfo.Arguments = arguments;

                // The following commands are needed to redirect the standard output.

                //This means that it will be redirected to the Process.StandardOutput StreamReader.

                procStartInfo.RedirectStandardOutput = true;

                procStartInfo.UseShellExecute = false;

                // Do not create the black window.

                procStartInfo.CreateNoWindow = true;

                // Now we create a process, assign its ProcessStartInfo and start it

                System.Diagnostics.Process proc = new System.Diagnostics.Process();

                proc.StartInfo = procStartInfo;

                proc.Start();

                // Get the output into a string

                string result = proc.StandardOutput.ReadToEnd();

                // Display the command output.

                return result;

        }
    }
}
