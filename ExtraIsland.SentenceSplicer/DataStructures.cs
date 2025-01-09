using System.Text.Json.Serialization;

namespace ExtraIsland.SentenceSplicer;

public class RhesisData {
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Catalog { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public abstract class HitokotoData {
    [JsonPropertyName("id")] public int Id { get; set; } = 0;

    [JsonPropertyName("uuid")] public string Uuid { get; set; } = string.Empty;

    [JsonPropertyName("hitokoto")] public string Hitokoto { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("from")] public string From { get; set; } = string.Empty;

    [JsonPropertyName("from_who")] public string FromWho { get; set; } = string.Empty;

    [JsonPropertyName("creator")] public string Creator { get; set; } = string.Empty;

    [JsonPropertyName("creator_uid")] public int CreatorUid { get; set; } = 0;

    [JsonPropertyName("reviewer")] public int Reviewer { get; set; } = 0;

    [JsonPropertyName("commit_from")] public string CommitFrom { get; set; } = string.Empty;

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("length")] public int Length { get; set; } = 0;

    public RhesisData ToRhesisData() {
        return new RhesisData {
            Author = FromWho,
            Title = From,
            Content = Hitokoto,
            Source = "一言语句库",
            Catalog = ConvertTypeToString(Type),
        };
    }

    public static string ConvertTypeToString(string type) {
        string result = type switch {
            "a" => "动画",
            "b" => "漫画",
            "c" => "游戏",
            "d" => "文学",
            "e" => "原创",
            "f" => "网络",
            "g" => "其他",
            "h" => "影视",
            "i" => "诗词",
            "j" => "网易云",
            "k" => "哲学",
            "l" => "抖机灵",
            _ => string.Empty
        };
        return result;
    }
}