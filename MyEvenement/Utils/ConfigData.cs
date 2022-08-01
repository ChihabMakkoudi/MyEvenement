using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyEvenement.Utils
{
    public class ConfigData
    {
        public int Current_Event_Id { get; set; }
        public string Current_Event_Name { get; set; }
        private static readonly string JSON_Path = @"Properties\ConfigData.json";

        public void SaveJson()
        {
            string json = JsonSerializer.Serialize(this,  new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JSON_Path, json);
        }
        public static ConfigData GetConfigData_Json()
        {
            string jsonString = File.ReadAllText(JSON_Path);
            ConfigData configData = JsonSerializer.Deserialize<ConfigData>(jsonString)!;
            return configData;
        }
    }

}
