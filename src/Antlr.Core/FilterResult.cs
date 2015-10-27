namespace Antlr.Core
{
    public class FilterResult
    {
        public int Level { get; set; }
        public FilterStatus Status { get; set; }
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
    }
}