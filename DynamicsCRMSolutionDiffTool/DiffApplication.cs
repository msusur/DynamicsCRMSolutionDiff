using System;
using System.Linq;
using System.Reflection;
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
            ArgumentsModel arguments = null;
            try
            {
                Log.Message($"Solution Diff tool [Version {Assembly.GetExecutingAssembly().GetName().Version}]");
                Console.WriteLine();
                arguments = ArgumentBuilder.BuildArguments(args);
                if (string.IsNullOrEmpty(arguments.SourceFile) || string.IsNullOrEmpty(arguments.TargetFile))
                {
                    throw new Exception(Strings.ShowHelp);
                }

                if (string.IsNullOrEmpty(arguments.LogFile))
                {
                    Log.Warn("Log parameter not present. Console outputs won't be logged.");
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
                Log.Info("Done!");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                helper?.Clear();
                Log.Info("Temporary folders deleted successfully.");
            }

            Log.Flush(arguments?.LogFile);

            Console.ResetColor();
            Console.WriteLine("(Press any key to continue)");
            Console.ReadLine();
        }

        private static void LogChanges(FileInfoContainer change)
        {
            StringBuilder message = new StringBuilder();
            ConsoleColor color = ConsoleColor.White;
            switch (change.DiffType)
            {
                case FileDiffTypes.Changed:
                    color = ConsoleColor.Yellow;
                    message.Append("CHANGE: ");
                    break;
                case FileDiffTypes.New:
                    color = ConsoleColor.Blue;
                    message.Append("NEW FILE: ");
                    break;
            }
            message.Append($" {change.FileInfo.FullName}");
            Log.Message(message.ToString(), color);
        }
    }
}