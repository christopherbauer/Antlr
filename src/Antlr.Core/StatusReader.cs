namespace Antlr.Core
{
    public class StatusReader
    {
        private readonly IAntRegexGenerator _antRegexGenerator;

        public StatusReader(IAntRegexGenerator antRegexGenerator)
        {
            _antRegexGenerator = antRegexGenerator;
        }

        public FilterStatus GetFilterStatus(string directory, string filter, FilterStatus parentFilterStatus, string projectUri, bool filterRemoves = false)
        {
            if (parentFilterStatus != FilterStatus.Ignored && parentFilterStatus != FilterStatus.ParentIgnored)
            {
                filter = filter.Replace("\\", "\\\\");
                var regex = _antRegexGenerator.GetRegexForFilter(filter);
                var tempDirectory = directory.Remove(0, projectUri.Length);
                if (filterRemoves)
                {
                    return regex.IsMatch(tempDirectory) ? FilterStatus.Ignored : FilterStatus.Found;
                }
                else
                {
                    return regex.IsMatch(tempDirectory) ? FilterStatus.Found : FilterStatus.Ignored;
                }
            }
            else
            {
                return FilterStatus.ParentIgnored;
            }
        }
    }
}