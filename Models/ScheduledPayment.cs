using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace unwallet.Models{
    public class ScheduledPayment{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]        
        public string? _id {get; set;}

        [BsonElement("user_id")]
        [JsonPropertyName("user_id")]
        public int? UserId {get; set;}

        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string? Name {get; set;}

        [BsonElement("category_id")]
        [JsonPropertyName("category_id")]
        public int? CategoryId {get; set;}

        [BsonElement("account_id")]
        [JsonPropertyName("account_id")]
        public int? AccountId {get; set;}

        [BsonElement("payment_method")]
        [JsonPropertyName("payment_method")]
        public string? PaymentMethod {get; set;}
        
        [BsonElement("recipient")]
        [JsonPropertyName("recipient")]
        public string? Recipient {get; set;}

        [BsonElement("frequency")]
        [JsonPropertyName("frequency")]
        public string? Frequency {get; set;}

        [BsonElement("start_date")]
        [JsonPropertyName("start_date")]
        public DateTime? StartDate {get; set;}

        [BsonElement("notification_time")]
        [JsonPropertyName("notification_time")]
        public string? NotificationTime {get; set;}

        [BsonElement("periodicity_config")]
        [JsonPropertyName("periodicity_config")]
        public PeriodicityConfig? PeriodicityConfig {get; set;}

        //[JsonExtensionData]
        //[BsonExtraElements]
        //public Dictionary<string, object>? AdditionalData { get; set; }
    }

    public class PeriodicityConfig
    {
        [BsonElement("time_unit")]
        [JsonPropertyName("time_unit")]
        public string? TimeUnit {get; set;}

        [BsonElement("time_lapse")]
        [JsonPropertyName("time_lapse")]
        public int? TimeLapse {get; set;}

        [BsonElement("datys_of_week")]
        [JsonPropertyName("days_of_week")]
        public string[]? DaysOfWeek {get; set;}

        [BsonElement("until")]
        [JsonPropertyName("until")]
        public string? Until {get; set;}
    }
}