using BusinessLayer.Contracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Services.Logger
{
    public class FileLogger : IAppLogger
    {
        private readonly ILogger? _logger;

        public FileLogger()
        {
            try
            {
                _logger = Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("Logger/ApplicationEvents.txt").CreateLogger();
            }
            catch
            {
                _logger = null;
            }
        }

        ~FileLogger()
        {
            if (_logger != null)
            {
                Log.CloseAndFlush();
            }
        }

        public void Error(string error)
        {
            if (_logger != null)
            {
                _logger.Error(error);
                Log.CloseAndFlush();
            }
        }

        public void Info(string info)
        {
            if (_logger != null)
            {
                _logger.Information(info);
                Log.CloseAndFlush();
            }
        }

        public void Warning(string warning)
        {
            if (_logger != null)
            {
                _logger.Warning(warning);
                Log.CloseAndFlush();
            }
        }
    }
}
