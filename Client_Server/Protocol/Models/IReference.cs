using DestallMaterials.WheelProtection.Copying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Protocol.Models;

public partial interface IReference : IModelBase, IEquatable<IReference>
{
    public long Id { get; set; }
    public System.String Representation { get; set; }
}

public partial interface IReference<TModel> : Protocol.Models.IReference
    where TModel : Protocol.Models.IModelBase
{
    bool IEquatable<IReference>.Equals(IReference other)
        => other is IReference<TModel> && other.Id == Id;
}

public static class ReferenceExtensions
{
    public static bool IsEmpty(this IReference reference)
        => reference is null || reference.Id < 1;
}