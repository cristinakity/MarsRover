using System;
using System.IO;
using System.Text;

namespace MarsRovers.Core
{
    public enum MessageType
    {
        Info = 0,
        Error =1,
        Warning = 2
    }

    public class Logger { 
        private static Object LogWriting { get; } = new Object();
        private bool _showLog;

        public Logger(bool showLog =false)
        {
            _showLog = showLog;
        }

        public void Log(String message, MessageType messageType = MessageType.Info )
        {
            if (_showLog)
            {
                //backup original color
                var originalColor = Console.ForegroundColor;
                switch (messageType)
                {
                    case MessageType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case MessageType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {message}");
                //Restore color
                Console.ForegroundColor = originalColor;
            }

            String logDirectory = "logs";
            string fileName = "Log.txt";
            String path = Path.Combine(logDirectory, fileName);

            StringBuilder log = new StringBuilder();
            log.AppendLine("Time   : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            log.AppendLine("Message: " + message);
            log.AppendLine();

            lock (LogWriting)
            {
                Directory.CreateDirectory(logDirectory);
                File.AppendAllText(path, log.ToString());
                                
                if (new FileInfo(path).Length >= 100000)
                {
                    var backFileName = Path.Combine(logDirectory, $"Log-{DateTime.Now.ToString("yyyyMMddHHmmss")}-{Guid.NewGuid()}.txt");
                    File.Move(path, backFileName);
                }
            }
        }

        public void Log(Exception exception)
        {
            while (exception.InnerException != null)
                exception = exception.InnerException;

            Log($"{exception.GetType()}: {exception.Message}{Environment.NewLine}{exception.StackTrace}", MessageType.Error);
        }

    }
}
