namespace Data.Entities;

public partial class Currency : ReferrableEntity
{
    public string Name { get; set; }

    public override string GetRepresentation()
        => Name;

    public bool Primary { get; set; }
}
