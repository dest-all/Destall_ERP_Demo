using System;

namespace Data.Entities;

public struct EntityStatus
{
    public required ushort Index { get; init; }
    public required string Name { get; init; }
    public required Type Entity { get; init; }
}
