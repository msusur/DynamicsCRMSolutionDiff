using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DiffTool.Model;

namespace DiffTool.Services
{
    public class FileComparer
    {
        private readonly LogService _log;
        private readonly Dictionary<string, string> _fileHashList = new Dictionary<string, string>();

        public FileComparer(FileInfo[] sourceFiles, LogService log)
        {
            _log = log;
            Initialize(sourceFiles);
        }

        private void Initialize(FileInfo[] sourceFiles)
        {
            _log.Info("Creating file index.");
            _fileHashList.Clear();
            CreateIndex(sourceFiles, _fileHashList);
            _log.Info("Done.");
        }

        private void CreateIndex(FileInfo[] sourceFiles, Dictionary<string, string> hashList)
        {
            using (var md5 = MD5.Create())
            {
                foreach (var sourceFile in sourceFiles)
                {
                    using (var fileStream = File.OpenRead(sourceFile.FullName))
                    {
                        var hash = md5.ComputeHash(fileStream);
                        hashList.Add(sourceFile.FullName, Encoding.Default.GetString(hash));
                    }
                }
            }
        }

        public CompareResult With(FileInfo[] targetFiles)
        {
            var targetHashlist = new Dictionary<string,string>();
            CreateIndex(targetFiles, targetHashlist);
            var compare = new CompareResult();
            foreach (var targetFile in targetFiles)
            {
                
            }
        }
    }
}