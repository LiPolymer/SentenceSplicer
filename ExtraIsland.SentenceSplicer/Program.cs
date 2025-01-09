using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace ExtraIsland.SentenceSplicer;

static class Program {
    static void Main(string[] args) {
        Console.WriteLine("Sentence Splicer");
        while (true) {
            Console.Write(">");
            Crossroad(ResolveArgs(Console.ReadLine()));
        }
        // ReSharper disable once FunctionNeverReturns
    }

    static void Crossroad(string[] cmd) {
        switch (cmd[0]) {
            case "hitokoto":
                if (!Directory.Exists("./sentences-bundle")) {
                    Console.WriteLine("No [sentences-bundle] folder found!");
                    break;
                }
                string[] bundleList = Directory.GetFiles("./sentences-bundle/sentences", "*.json");
                RhesisDataPack pack = new RhesisDataPack {
                    Name = "Hitokoto离线句子包",
                    IsSingleGroup = false,
                    Description = "由SentenceSplicer生成"
                };
                foreach (string catObj in bundleList) {
                    Console.WriteLine($"Parsing[{catObj}]");
                    List<HitokotoData>? data = JsonSerializer.Deserialize<List<HitokotoData>>(File.ReadAllText(catObj));
                    if (data == null) continue;
                    List<RhesisData> rhesisData = [];
                    rhesisData.AddRange(data.Select(hData => hData.ToRhesisData()));
                    RhesisDataGroup group = new RhesisDataGroup {
                        Name = HitokotoData.ConvertTypeToString(data[0].Type),
                        Description = data[0].Type,
                        Data = rhesisData
                    };
                    pack.RhesisDataGroups.Add(group);
                }
                JsonSerializerOptions options = new JsonSerializerOptions {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                Console.WriteLine($"[{pack.RhesisDataGroups.Sum(packDataGroup => packDataGroup.Data.Count)}] RhesisData in [{pack.RhesisDataGroups.Count}] Groups");
                Console.WriteLine($"Exporting[hitokoto.json]");
                File.WriteAllText("hitokoto.json",JsonSerializer.Serialize(pack,options));
                break;
            case "poem":
                
                break;
        }
    }

    static string[] ResolveArgs(string? st) {
        if (st == null) {
            return [];
        }
        return new Regex(@"""([^""]*)""|(\S+)").Matches(st)
            .Select(i => i.Value)
            .ToArray();
    }
}