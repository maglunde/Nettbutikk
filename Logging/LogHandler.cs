using System;
using System.IO;

namespace Logging
{
    public class LogHandler
    {

        private const string LOG_PATH = "Logs";
        private const string LOG_FILE = "log.txt";
        private static string folderPath = AppDomain.CurrentDomain.BaseDirectory + LOG_PATH;
        private static string fullPath = folderPath + "\\" + LOG_FILE;

        static LogHandler()
        {

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

        }

        public static void WriteToLog(Exception e)
        {

            if (e == null)
            {
                WriteToLog("Error @ " + DateTime.Now.ToShortDateString().ToString() +
                " " + DateTime.Now.ToLongTimeString().ToString() + " --> NO EXCEPTION INFORMATION");
            }
            else
            {
                WriteToLog("Error @ " + DateTime.Now.ToShortDateString().ToString() +
                " " + DateTime.Now.ToLongTimeString().ToString() + " --> " + e);
            }

        }


        private static void WriteToLog(string str)
        {

            try
            {
                StreamWriter sw = new StreamWriter(fullPath, true);
                sw.WriteLine(str);
                sw.WriteLine();
                sw.WriteLine();

                sw.Flush();
                sw.Close();
            }
            catch (Exception) { }
        }


    }
}