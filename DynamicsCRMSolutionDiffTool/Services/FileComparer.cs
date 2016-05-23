using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DiffTool.Model;
using DiffTool.Services.Comparers;

namespace DiffTool.Services
{
    public class FileComparer
    {
        private readonly LogService _log;
        private readonly string _sourceDir;
        private readonly string _targetDir;
        private readonly Dictionary<string, string> _fileHashList = new Dictionary<string, string>();
        private readonly List<ICompareStrategy> _compareStrategies = new List<ICompareStrategy>
        {
            new ControlIfNewStrategy(),
            new ControlIfChangedStrategy()
        };

        public FileComparer(FileInfo[] sourceFiles, LogService log, string sourceDir, string targetDir)
        {
            _log = log;
            _sourceDir = sourceDir;
            _targetDir = targetDir;
            Initialize(sourceFiles);
        }

        private void Initialize(FileInfo[] sourceFiles)
        {
            _log.Info("Creating file index.");
            _fileHashList.Clear();
            CreateIndex(sourceFiles, _fileHashList, _sourceDir); // create index for source
            _log.Info("Done.");
        }

        private void CreateIndex(FileInfo[] sourceFiles, Dictionary<string, string> hashList, string folderRoot)
        {
            using (var md5 = MD5.Create())
            {
                foreach (var sourceFile in sourceFiles)
                {
                    using (var fileStream = File.OpenRead(sourceFile.FullName))
                    {
                        var hash = md5.ComputeHash(fileStream);
                        hashList.Add(sourceFile.FullName.Replace(folderRoot, string.Empty), Encoding.Default.GetString(hash));
                    }
                }
            }
        }

        public CompareResult With(FileInfo[] targetFiles)
        {
            var targetHashlist = new Dictionary<string, string>();
            CreateIndex(targetFiles, targetHashlist, _targetDir); //create index for target.
            var compareResult = new CompareResult();
            foreach (var targetFile in targetFiles)
            {
                _compareStrategies.ForEach(s => s.Compare(_fileHashList, targetHashlist, targetFile, compareResult, 
                    //this is what happens when you don't use your own file reference and use FileInfo instead.
                    _sourceDir, _targetDir)); 
            }
            return compareResult;
        }
    }
}