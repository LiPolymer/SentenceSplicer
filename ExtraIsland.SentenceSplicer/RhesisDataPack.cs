namespace ExtraIsland.SentenceSplicer;

public class RhesisDataPack {
    public string Name { get; set; } = "离线句子包";
    public string Description { get; set; } = string.Empty;
    public bool IsSingleGroup = true;
    public List<RhesisDataGroup> RhesisDataGroups { get; set; } = [];
}

public class RhesisDataGroup {
    public string Name { get; set; } = "root";
    public string Description { get; set; } = string.Empty;
    public List<RhesisData> Data { get; set; } = [];
}