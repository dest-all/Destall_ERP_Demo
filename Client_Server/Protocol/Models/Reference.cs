using System;
using System.Collections.Generic;

namespace Protocol.Models
{
    public abstract partial class Reference
    {
        public long Id { get; set; }
        public System.String Representation { get; set; }
        public override string ToString() => Id == 0 ? "-" : Representation;

        [JsonIgnore]
        [MemoryPack.MemoryPackIgnore]
        public bool Empty => Id < 1;
    }
}