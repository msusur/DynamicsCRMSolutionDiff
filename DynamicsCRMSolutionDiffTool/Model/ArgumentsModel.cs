namespace DiffTool.Model
{
    public class ArgumentsModel
    {
        public string SourceFile { get; private set; }
        public string TargetFile { get; private set; }
        public string LogFile { get; private set; }

        public ArgumentsModel(string source, string target, string logFile)
        {
            SourceFile = source;
            TargetFile = target;
            LogFile = logFile;
        }
    }
}
