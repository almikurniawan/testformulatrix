namespace TestB.Entities;

public partial class Item
{
    public string ItemName { get; set; } = null!;

    public string? ItemContent { get; set; }

    public byte? ItemType { get; set; }
}
