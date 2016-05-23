using System;
using System.IO;

namespace DiffTool.Services
{
    public interface IFileHelper
    {
        void SaveToFile(string path, string content);
    }

    public class FileHelper : IFileHelper
    {
        public void SaveToFile(string path, string content)
        {
            if (File.Exists(path))
            {
                var directoryName = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(directoryName))
                {
                    directoryName = AppDomain.CurrentDomain.BaseDirectory;
                }

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                var extension = Path.GetExtension(path);

                path = Path.Combine(directoryName, $"{fileNameWithoutExtension}(1).{extension}");
            }
            File.WriteAllText(path, content);
        }
    }
}
