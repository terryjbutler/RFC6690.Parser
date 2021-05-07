using System.Collections.Generic;

namespace RFC6690.Parser
{
    public class ConstrainedLink
    {
        public string Link { get; set; }

        public Dictionary<string, string> Params { get; private set; } = new Dictionary<string, string>();
    }
}
