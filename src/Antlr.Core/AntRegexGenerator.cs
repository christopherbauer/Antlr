namespace Antlr.Core
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class AntRegexGenerator
    {
        public Regex GetRegexForFilter(string filter)
        {
            //Stuff ANT does:
            //* matches 0 or more characters
            //? matches one character
            //.. not recognized
            //pattern ends / or \ becomes **
            //** matches 0 or more directories
            var filterRegexBuilder = new StringBuilder("^");
            var tempBuilder = new StringBuilder();
            var recognizedCharacters = new List<string> { "*", "?" };
            for (var i = 0; i < filter.Length; i++)
            {
                if (i + 1 == filter.Length && (filter[i] == '/' || filter[i] == '\\'))
                {
                    filterRegexBuilder.Append(tempBuilder);
                    tempBuilder = new StringBuilder();

                    filterRegexBuilder.Append(@"[/a-zA-Z0-9\.\\]*");
                }
                else if (recognizedCharacters.Contains(filter[i].ToString()))
                {
                    filterRegexBuilder.Append(tempBuilder);
                    tempBuilder = new StringBuilder();

                    if (filter[i].Equals('*'))
                    {
                        if (filter.Length >= i + 2 && filter[i + 1].Equals('*'))
                        {
                            if (filter.Length >= i + 3 && filter[i + 2].Equals('/'))
                            {
                                i += 2;
                                filterRegexBuilder.Append(@"[/a-zA-Z0-9\.\\]*");
                            }
                            else
                            {
                                i++;
                                filterRegexBuilder.Append(@"[/a-zA-Z0-9\.\\]*");
                            }
                        }
                        else
                        {
                            filterRegexBuilder.Append(@"[a-zA-Z0-9\.]*(?!/)");
                        }
                    }
                    else if (filter[i].Equals('?'))
                    {
                        filterRegexBuilder.Append(@"[/a-zA-Z0-9\.\\]{1}");
                    }
                }
                else
                {
                    tempBuilder.Append(filter[i]);
                }
            }
            filterRegexBuilder.Append(tempBuilder);
            return new Regex(filterRegexBuilder + "$");
        }
    }
}