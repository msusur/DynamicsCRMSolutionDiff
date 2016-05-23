using System.Collections.Generic;
using System.IO;
using DiffTool.Model;

namespace DiffTool.Services.Comparers
{
    public class ControlIfNewStrategy : ICompareStrategy
    {
        public void Compare(Dictionary<string, string> sourceHash, Dictionary<string, string> targetHashlist, FileInfo targetFile, CompareResult compareResult)
        {
            if (!sourceHash.ContainsKey(targetFile.FullName))
            {
                compareResult.AddNewAddedFile(targetFile);
            }
        }
    }
}