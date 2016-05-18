namespace DiffTool.Model
{
    public class ArgumentsModel
    {
        public string SourceFile { get; private set; }
        public string TargetFile { get; private set; }

        public ArgumentsModel(string source, string target)
        {
            SourceFile = source;
            TargetFile = target;
        }
    }
}
