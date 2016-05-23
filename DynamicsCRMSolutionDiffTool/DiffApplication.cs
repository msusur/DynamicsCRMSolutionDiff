using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using DiffTool.Model;
using DiffTool.Services;

namespace DiffTool
{
    public static class DiffApplication
    {
        private static readonly LogService Log = new LogService();
        public static void Main(string[] args)
        {
            PackageHelper helper = null;

            try
            {
                Log.Message($"Solution Diff tool [Version {Assembly.GetExecutingAssembly().GetName().Version}]");
                Console.WriteLine();
                var arguments = ArgumentBuilder.BuildArguments(args);
                if (string.IsNullOrEmpty(arguments.SourceFile) || string.IsNullOrEmpty(arguments.TargetFile))
                {
                    throw new Exception(Strings.ShowHelp);
                }

                Log.Info($"Comparing solutions;\r\n\t'{arguments.SourceFile}'\r\n\t'{arguments.TargetFile}'");

                helper = new PackageHelper(arguments, Log);
                helper.Extract();
                CompareResult result = helper.Compare();
                if (result.Files.Count == 0)
                {
                    Log.Warn("No changes were found!!");
                }
                result.Files.OrderBy(c => c.DiffType).ToList().ForEach(LogChanges);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                helper?.Clear();
            }
            Log.Info("Done! (Press any key to continue)");
            Console.ReadLine();
        }

        private static void LogChanges(FileInfoContainer change)
        {
            StringBuilder message = new StringBuilder();
            switch (change.DiffType)
            {
                case FileDiffTypes.Changed:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    message.Append("*");
                    break;
                case FileDiffTypes.New:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    message.Append("+");
                    break;
            }
            message.Append($" {change.FileInfo.FullName}");
            Console.WriteLine(message);
        }
    }
}