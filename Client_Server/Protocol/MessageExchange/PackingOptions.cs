using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.MessageExchange
{

    public class ProtocolOptions
    {
        public ProtocolOptions()
        {
        }

        public bool Compress { get; set; } = true;
        public bool UseMemoryPack { get; set; } = true;
    }

    public struct PackingOptions
    {
        public bool MemoryPacked { get; init; }
        public bool Compressed { get; init; }
    }
}
