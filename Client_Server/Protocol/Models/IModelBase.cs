using System.Linq;
using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using MemoryPack;
using System;

namespace Protocol.Models
{
    public interface IModelBase
    {
        int ComputeChecksum();
    }

    public interface IReferrableModel : IModelBase
    {
        IReference Reference { get; }
    }

    public interface IPackable
    {
        byte[] Pack();
    }

    public interface IPackable<T> : IPackable
    {
        static abstract T Unpack(byte[] bytes);
    }


    public static class ModelExtensions
    {
        public static int ComputeChecksum<TModel>(this IEnumerable<TModel> items)
            where TModel : IModelBase
        {
            unchecked
            {
                var result = items.Select(i => i.ComputeChecksum()).SumUnchecked();
                return result;
            }
        }

        public static int ComputeChecksum<TModel>(this TModel model)
            where TModel : IModelBase => model.ComputeChecksum();
    }
}