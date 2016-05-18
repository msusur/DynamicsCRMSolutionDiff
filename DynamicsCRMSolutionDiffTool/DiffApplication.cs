using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DiffTool.Model;
using DiffTool.Services;

namespace DiffTool
{
    public static class DiffApplication
    {
        private static readonly LogService Log = new LogService();
        public static void Main(string[] args)
        {
            try
            {
                Log.Message($"Solution Diff tool v.{Assembly.GetExecutingAssembly().GetName().Version}");
                var arguments = ArgumentBuilder.BuildArguments(args);
                if (string.IsNullOrEmpty(arguments.SourceFile) || string.IsNullOrEmpty(arguments.TargetFile))
                {
                    throw new Exception(Strings.ShowHelp);
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            Console.ReadLine();
        }
    }

    public static class ArgumentBuilder
    {
        public static ArgumentsModel BuildArguments(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ArgumentNullException(nameof(args));
            }
            var parsedArgs = args.Select(s => s.Split(new[] { ':' }, 1)).ToDictionary(s => s[0], s => s[1]);
            if (parsedArgs.Count < 2)
            {
                throw new Exception(Strings.ShowHelp);
            }

            string source = parsedArgs["s"];
            string target = parsedArgs["t"];

            return new ArgumentsModel(source, target);
        }
    }

    public struct Strings
    {
        public const string ShowHelp = "Need to arguments to proceed. \r\nExample:\r\n SolutionDiff.exe s:[Source File Path] t:[Target File Path]";
    }
}
