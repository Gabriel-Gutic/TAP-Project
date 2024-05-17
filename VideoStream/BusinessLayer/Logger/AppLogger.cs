using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logger
{
    public class AppLogger : IAppLogger
    {
        private readonly string _filePath;

        public AppLogger()
        {
            _filePath = "Logger/Log.txt";
        }

        public void Error(string error)
        {
            Log("ERROR", error);
        }

        public void Info(string info)
        {
            Log("INFO", info);
        }

        public void Warning(string warning)
        {
            Log("WARNING", warning);
        }

        private void Log(string logType, string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [{logType}] {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
