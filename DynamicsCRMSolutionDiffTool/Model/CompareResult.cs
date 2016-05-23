using System.Collections.Generic;
using System.IO;

namespace DiffTool.Model
{
    public class CompareResult
    {
        public List<FileInfoContainer> Files { get; } = new List<FileInfoContainer>();

        public void AddNewAddedFile(FileInfo file)
        {
            Files.Add(new FileInfoContainer(file, FileDiffTypes.New));
        }

        public void AddChangedFile(FileInfo file)
        {
            Files.Add(new FileInfoContainer(file, FileDiffTypes.Changed));
        }


    }

    public class FileInfoContainer
    {
        public FileDiffTypes DiffType { get; private set; }

        public FileInfo FileInfo { get; private set; }

        public FileInfoContainer(FileInfo file, FileDiffTypes type)
        {
            FileInfo = file;
            DiffType = type;
        }

        public override string ToString()
        {
            //I know...
            string delimiter = DiffType == FileDiffTypes.Changed ? "*" : "+";
            var fileInfo = FileInfo.FullName;
            return $"{delimiter} {fileInfo}";
        }
    }

    public enum FileDiffTypes
    {
        // Deleted/Removed should added.
        Changed, New
    }
}
