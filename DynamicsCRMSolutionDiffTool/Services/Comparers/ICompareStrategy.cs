using System.Collections.Generic;
using System.IO;
using DiffTool.Model;

namespace DiffTool.Services.Comparers
{
    public interface ICompareStrategy
    {
        void Compare(Dictionary<string, string> sourceHash, Dictionary<string, string> targetHashlist, FileInfo targetFile, CompareResult compareResult);
    }
}