using System.Collections.Generic;

namespace RFC6690.Parser
{
    public class RFC6690ParserResponse
    {
        public List<ConstrainedLink> ConstrainedLinks { get; private set; } = new List<ConstrainedLink>();
    }
}