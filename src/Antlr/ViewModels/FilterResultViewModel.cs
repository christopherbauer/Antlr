namespace Antlr.ViewModels
{
    using Antlr.Core;

    public class FilterResultViewModel
    {
        public int Level { get; set; }
        public FilterStatus Status { get; set; }
        public string FullPath { get; set; }
        public string RelativePath { get; set; }

        public decimal GetIndent {
            get
            {
                return this.Level * 25; 
            }
        }
    }
}