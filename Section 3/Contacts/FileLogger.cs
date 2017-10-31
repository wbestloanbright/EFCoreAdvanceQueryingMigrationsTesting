using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts
{
    public class FileLogger : ILogger
    {
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId.Name == "Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted")
            {
                var finalState = (IReadOnlyList<KeyValuePair<string, object>>)state;
                var total = decimal.Parse(finalState.FirstOrDefault(a => a.Key == "elapsed").Value.ToString());
                var comamnd = finalState.FirstOrDefault(a => a.Key == "commandText").Value;
                File.AppendAllText(@"C:\log.txt", $" {Environment.NewLine} query {comamnd} tool {total} ms to execute {Environment.NewLine}");
                File.AppendAllText(@"C:\log.txt", formatter(state, exception));
                Console.WriteLine(formatter(state, exception));
            }

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
