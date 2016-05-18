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
            throw new System.NotImplementedException();
        }

        private string ExtractInternal(string filePath)
        {
            var temp = Path.GetTempPath();

            PackagerArguments packagerArgs = new PackagerArguments
            {
                PathToZipFile = filePath,
                Folder = temp,
                PackageType = SolutionPackageType.Both,
                Action = CommandAction.Extract
            };

            _log.Info($"Extracting the package '{filePath}' to temp location.");
            SolutionPackager packager = new SolutionPackager(packagerArgs);
            packager.Run();
            _log.Append("....Done.");

            return temp;
        }
    }
}