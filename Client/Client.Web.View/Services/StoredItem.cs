namespace Client.Web.View.Services
{
    class StoredItem<TItem>
    {
        public string Type => Value.GetType().FullName;
        public TItem Value { get; }
        public StoredItem(TItem value)
        {
            Value = value;
        }

        public int Order { get; set; }
    }
}
