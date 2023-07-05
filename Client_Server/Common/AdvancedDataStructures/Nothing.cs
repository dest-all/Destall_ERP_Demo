using System;

namespace Common.AdvancedDataStructures
{
    public readonly struct Nothing
    {
        [Obsolete("May not instantiate Nothing. Use Nothing.Instance instead.", true)]
        public Nothing()
        {

        }

        public static Nothing Instance { get; }
    }
}
