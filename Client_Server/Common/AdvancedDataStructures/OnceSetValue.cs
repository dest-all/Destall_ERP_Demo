using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AdvancedDataStructures
{
    public class OnceSetValue<TValue> where TValue : class
    {
        public OnceSetValue()
        {
        }

        public OnceSetValue(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; private set; }
        public void Set(TValue value)
        {
            if (Value == null)
            {
                Value = value;
            }
            else 
            {
                throw new ArgumentException("Value already set.");
            }
        }

        public static implicit operator TValue(OnceSetValue<TValue> onceSetValue)
        {
            return onceSetValue.Value;
        }

        public static implicit operator OnceSetValue<TValue>(TValue value)
        {
            return new(value);
        }
    }
}
