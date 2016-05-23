using System;
using System.Collections.Generic;
using System.IO;
using DiffTool.Model;

namespace DiffTool.Services.Comparers
{
    public class ControlIfChangedStrategy : ICompareStrategy
    {
        public void Compare(Dictionary<string, string> sourceHash, Dictionary<string, string> targetHashlist, FileInfo targetFile, CompareResult compareResult)
        {
            if (!sourceHash.ContainsKey(targetFile.FullName))
            {
                return;
            }

            var source = sourceHash[targetFile.FullName];
            var target = targetHashlist[targetFile.FullName];

            if (!source.Equals(target, StringComparison.InvariantCultureIgnoreCase))
            {
                compareResult.AddChangedFile(targetFile);
            }
        }
    }
}