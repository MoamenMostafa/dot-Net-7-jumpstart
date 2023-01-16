using System.Text.Json.Serialization;

namespace dot_Net_7_jumpstart.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1 ,
        Mage = 2 ,
        Cleric = 3
    }
}