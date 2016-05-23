using System;
using System.Collections.Generic;
using System.IO;
using DiffTool.Model;

namespace DiffTool.Services.Comparers
{
    public class ControlIfNewStrategy : ICompareStrategy
    {
        public void Compare(Dictionary<string, string> sourceHash, Dictionary<string, string> targetHashlist, FileInfo targetFile, CompareResult compareResult, string sourceDir, string targetDir)
        {
            if (!sourceHash.ContainsKey(targetFile.FullName.Replace(targetDir, string.Empty)))
            {
                compareResult.AddNewAddedFile(targetFile);
            }
        }
    }
}