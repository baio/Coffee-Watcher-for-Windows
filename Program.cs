using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoffeeWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var watcher = new System.IO.FileSystemWatcher();

            
            watcher.Path = Directory.GetCurrentDirectory() + "\\";
            watcher.Filter = "*.coffee";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            watcher.Created += new FileSystemEventHandler(watcher_Created);

            watcher.EnableRaisingEvents = true;

            Log("Coffee-Watcher has started press q for exit");

            while (Console.Read() != 'q')
            { }
        }

        static bool _doubledFlag = false;

        private static void Log(string Message, bool Error = false)
        {
            if (Error)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(Message);

            Console.ResetColor();
        }

        private static void Compile(string FileName)
        {
            if (_doubledFlag)
            {
                _doubledFlag = false;

                return;
            }
            else
            {
                _doubledFlag = true;
            }


            string res = null;

            try
            {
                res = ExecuteCmd.Sync("coffee", "-c " + FileName);
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }

            if (string.IsNullOrEmpty(res))
                res = string.Format("File {0} successfully compiled", FileName);
            else
                res = string.Format("File {0} compilation failed:\n{1}", FileName, res);

            Log(res);
        }

        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Compile(e.FullPath);
        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Compile(e.FullPath);
        }

        static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
        }

        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
