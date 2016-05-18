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
                throw new ArgumentNullException(nameof(args));
            }
            //var parsedArgs = args.Select(s => s.Split(new[] { ':' })).ToDictionary(s => s[0], s => s[1]);
            //if (parsedArgs.Count < 2)
            //{
            //    throw new Exception(Strings.ShowHelp);
            //}

            //string source = parsedArgs["s"];
            //string target = parsedArgs["t"];

            if (args.Length < 2)
            {
                throw new Exception(Strings.ShowHelp);
            }

            string source = args[0];
            string target = args[1];
            return new ArgumentsModel(source, target);
        }
    }
}