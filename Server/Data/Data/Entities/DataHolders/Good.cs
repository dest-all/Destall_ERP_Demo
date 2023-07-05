namespace Data.Entities.DataHolders;

public partial class Good : DataHolder
{
    public string Name { get; set; } = "";

    public override string GetRepresentation() => Name;
}