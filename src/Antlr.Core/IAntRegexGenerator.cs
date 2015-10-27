using System.Text.RegularExpressions;

namespace Antlr.Core
{
    public interface IAntRegexGenerator
    {
        Regex GetRegexForFilter(string filter);
    }
}