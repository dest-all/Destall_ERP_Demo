using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Client.Communication;

namespace Client.Communication.Grpc
{
    public class GrpcCallConfiguration : CallConfiguration
    {
        public GrpcCallConfiguration(CallConfiguration other)
        {
            CancellationToken = other.CancellationToken;
            Deadline = other.Deadline;

            Metadata = other.Headers.ToGrpcMetadata();
        }

        public GrpcCallConfiguration()
        {
        }

        public CancellationToken CancellationToken { get; init; }

        public TimeSpan Deadline { get; init; }

        public Metadata Metadata { get; init; }
    }

    public static class GrpcMetadataExtensions
    {
        public static Metadata ToGrpcMetadata(this IEnumerable<KeyValuePair<string, string>> headers)
        {
            var metadata = new Metadata();
            if (headers is null)
            {
                return metadata;
            }
            foreach (var header in headers)
            {
                metadata.Add(header.Key, header.Value);
            }
            return metadata;
        }
    }
}
