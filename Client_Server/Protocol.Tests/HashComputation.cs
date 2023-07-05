using Newtonsoft.Json;
using Protocol.Models;
using Protocol.Models.DataHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Tests;

public class HashComputation
{
    const string _messageExample = @"[
        {
            ""Name"": ""Printer"",
            ""Reference"": {
                ""Id"": 1056406938666792,
                ""Representation"": ""Printer""
            },
            ""ResponsibleEmployees"": [
                {
                    ""Id"": 1010633375265623,
                    ""Representation"": ""Adrienne Aker""
                },
                {
                    ""Id"": 1010633375265683,
                    ""Representation"": ""Alexandra Gaineyeye""
                }
            ]
        }
    ]";

    [Test]
    public void ComputeHash_MustNotThrow()
    {
        var deserialized = JsonConvert.DeserializeObject<GoodModel[]>(_messageExample);

        for (int i = 0; i < deserialized.Length; i++)
        {
            var item = deserialized[i];
            var hash = item.ComputeChecksum();
        }
    }
}
