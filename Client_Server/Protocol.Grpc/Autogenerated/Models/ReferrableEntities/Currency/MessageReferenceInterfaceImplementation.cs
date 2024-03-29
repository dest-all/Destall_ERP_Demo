// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System;
using Protocol.Models;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using Protocol.Grpc.ProtoModels.Conversion;
using System.Collections;
using Protocol.Models.ReferrableEntities;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class CurrencyReference : Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference, ICopied<CurrencyReference>
{
    public static implicit operator CurrencyReferenceMessage(CurrencyReference message) => new() //
    {Message = message};
    public static implicit operator CurrencyReference(CurrencyReferenceMessage message) => message.Message;
    CurrencyReference ICopied<CurrencyReference>.Copy() => this.Clone();
    Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference ICopied<Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference>.Copy() => ((ICopied<CurrencyReference>)this).Copy();
}

public sealed partial class CurrencyReferenceList : IReadOnlyList<Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference>
{
    public CurrencyReferenceList(RepeatedField<CurrencyReference> itemsParameter)
    {
        message_ = itemsParameter;
        OnConstruction();
    }

    Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference IReadOnlyList<Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference>.this[int index] => message_[index];
    public int Count => message_.Count;
    IEnumerator<Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference> IEnumerable<Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference>.GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
}