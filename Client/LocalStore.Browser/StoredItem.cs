using DestallMaterials.WheelProtection.Extensions.Strings;

namespace LocalStore.Browser
{
    class StoredItem<TItem>
    {
        string _type;

        public string Type { get => _type.IsEmpty() ? Value.GetType().FullName : _type; set => _type = value; }
        public required TItem Value { get; init; }
        public StoredItem()
        {
        }

        public required int Order { get; init; }
    }
}