using System;
using System.Linq;

namespace DiffTool.Model
{
    public static class ArgumentBuilder
    {
        public static ArgumentsModel BuildArguments(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ArgumentNullException(nameof(args), Strings.ShowHelp);
            }
            var parsedArgs = args.Select(s => s.Split('=')).ToDictionary(s => s[0], s => s[1]);
            if (parsedArgs.Count < 2)
            {
                throw new Exception(Strings.ShowHelp);
            }

            string source, target, logFile;

            parsedArgs.TryGetValue("source", out source);
            parsedArgs.TryGetValue("target", out target);
            parsedArgs.TryGetValue("log", out logFile);

            return new ArgumentsModel(source, target, logFile);
        }
    }
}