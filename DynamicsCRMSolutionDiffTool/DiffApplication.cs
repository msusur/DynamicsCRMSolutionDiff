using System;
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

                Log.Info($"Comparing '{arguments.SourceFile}' with '{arguments.TargetFile}'");

                var helper = new PackageHelper(arguments, Log);
                helper.Extract();
                CompareResult result = helper.Compare();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            Console.ReadLine();
        }
    }
}