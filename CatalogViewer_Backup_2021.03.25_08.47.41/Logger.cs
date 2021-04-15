using System;
using System.IO;

namespace CatalogViewer
{
    public class Logger
    {
        private string logPath;
        private bool logging;
        
        public Logger()
        {

            //string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "abc.txt");
            
            var currentPath = Directory.GetCurrentDirectory();
            logPath = string.Concat(currentPath, "\\logs\\", "CatalogViewerLog", DateTime.Now.ToString("_yyyy-MM-dd"), ".txt");


            //logPath =         @"~\logs\" + DateTime.Now.ToString("_yyyy-MM-dd") + ".txt";
            logging = true;
        }

        public Logger(string filePath)
        {
            logPath = filePath;
        }

        public void Log(string message)
        {
            WriteLogEntry(message);
        }

        private void WriteLogEntry(string message)
        {
            if (logging)
                {
                if (!File.Exists(logPath))
                {
                    CreateLogFle(logPath);
                }

                var dateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                var fullMessage = dateTime + ", " + message;
                //File.App(logPath, fullMessage);
                try
                {
                    StreamWriter sw = new StreamWriter(logPath, true);
                    sw.WriteLine(fullMessage);
                    sw.Flush();
                    sw.Close();
                }
                catch
                {
                    logging = false;
                }
            }
        }

        private void CreateLogFle(string path)
        {
            var fullPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(fullPath))
            {
                logging = false;
                Directory.CreateDirectory(fullPath);
            }
            else
            {
                logging = true;
            }
        }
    }

    public enum LogEntryType
    {
        Debug,
        Trace,
        Info,
        Warning,
        Error
    }
}
