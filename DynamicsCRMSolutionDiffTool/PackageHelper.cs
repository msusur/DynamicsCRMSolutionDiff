using System.Diagnostics;
using System.IO;
using DiffTool.Model;
using DiffTool.Services;
using Microsoft.Crm.Tools.SolutionPackager;

namespace DiffTool
{
    public class PackageHelper
    {
        public ArgumentsModel Arguments { get; private set; }

        private string _sourceTemp = string.Empty;
        private string _targetTemp = string.Empty;
        private readonly LogService _log;

        public PackageHelper(ArgumentsModel arguments, LogService log)
        {
            Arguments = arguments;
            _log = log;
        }

        public void Extract()
        {
            _sourceTemp = ExtractInternal(Arguments.SourceFile);
            _targetTemp = ExtractInternal(Arguments.TargetFile);
        }

        public CompareResult Compare()
        {
            var sourceDir = new DirectoryInfo(_sourceTemp);
            var targetDir = new DirectoryInfo(_targetTemp);


            var sourceFiles = sourceDir.GetFiles("*.*", SearchOption.AllDirectories);
            var targetFiles = targetDir.GetFiles("*.*", SearchOption.AllDirectories);

            FileComparer compare = new FileComparer(sourceFiles, _log, sourceDir.FullName, targetDir.FullName);
            _log.Info("Comparing two folders for changes and newly added files.");
            var compareResult = compare.With(targetFiles);

            return compareResult;
        }

        private string ExtractInternal(string filePath)
        {
            var temp = Path.GetTempFileName();

            temp = temp.Replace(Path.GetExtension(temp), "");
            Directory.CreateDirectory(temp);

            PackagerArguments packagerArgs = new PackagerArguments
            {
                PathToZipFile = filePath,
                Folder = temp,
                PackageType = SolutionPackageType.Managed,
                Action = CommandAction.Extract,
                ErrorLevel = TraceLevel.Error,
                LogFile = "log.txt",
                SingleComponent = "NONE"
            };

            _log.Info($"Extracting '{Path.GetFileName(filePath)}' to temp location.");
            SolutionPackager packager = new SolutionPackager(packagerArgs);
            packager.Run();

            return temp;
        }

        public void Clear()
        {
            _log.Info("Deleting temp folders.");

            if (!string.IsNullOrEmpty(_sourceTemp) || !Directory.Exists(_sourceTemp))
            {
                Directory.Delete(_sourceTemp, true);
            }
            else
            {
                _log.Warn("Source temp folder not found. Skipping...");
            }

            if (!string.IsNullOrEmpty(_targetTemp) || !Directory.Exists(_targetTemp))
            {
                Directory.Delete(_targetTemp, true);
            }
            else
            {
                _log.Warn("Target temp folder not found. Skipping...");
            }
        }
    }
}