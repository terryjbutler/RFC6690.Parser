using System.Text.RegularExpressions;

namespace RFC6690.Parser
{
    public class RFC6690Parser
    {
        public static readonly Regex RegexParser = new Regex(@"<(?<link>[^>]+)>\s*;\s*(?<rest>[^<]+)", RegexOptions.Compiled | RegexOptions.Multiline);
        public static readonly Regex RegexKeyValueParser = new Regex(@"(?:(?<key>[a-z]+)=((?<value>\d+)|((?:"")(?<value>[^""]+)(?:""))))", RegexOptions.Compiled);
        public static readonly char[] SplitSemiColonDelimiterChars = { ';' };

        public RFC6690ParserResponse Parse(string input)
        {
            var ret = new RFC6690ParserResponse();

            var matches = RegexParser.Matches(input);

            foreach (Match match in matches)
            {
                var link = match.Groups["link"]?.Value?.Trim();
                var rest = match.Groups["rest"]?.Value?.Trim();

                var constrainedLink = new ConstrainedLink()
                {
                    Link = link
                };

                var sections = rest.Split(SplitSemiColonDelimiterChars);

                foreach (var section in sections)
                {
                    var keyValuePair = RegexKeyValueParser.Match(section?.Trim());

                    string key = keyValuePair.Groups["key"]?.Value?.Trim();
                    string value = keyValuePair.Groups["value"]?.Value?.Trim();

                    constrainedLink.Params.Add(key, value);
                }

                ret.ConstrainedLinks.Add(constrainedLink);
            }

            return ret;
        }
    }
}