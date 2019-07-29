using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Delivery.WebServices.Objects
{
    [JsonObject]
    public class GoodsFromAPI
    {
        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("number")]
        public Int32? Number { get; set; }

        [JsonProperty("without_akciza")]
        public Int32? WithoutAkciza { get; set; }

        [JsonProperty("is_additional")]
        public Int32? IsAdditional { get; set; }

        [JsonProperty("coefficient")]
        public double? Coefficient { get; set; }

        [JsonProperty("additive_cost_without_akciza")]
        public double? AdditiveCostWithoutAkciza { get; set; }
    }

    [JsonObject]
    public class CalculatorObject
    {
        [JsonProperty("goods")]
        public List<GoodsFromAPI> Goods { get; set; }

        [JsonProperty("city_id")]
        public Int32? CityID { get; set; }

        [JsonProperty("profile_type")]
        public String ProfileType { get; set; }

        [JsonProperty("assessed_cost")]
        public String AssessedCost { get; set; }

        [JsonProperty("user_discount")]
        public Int32? UserDiscount { get; set; }

        [JsonProperty("user_id")]
        public Int32? UserID { get; set; }

        [JsonProperty("user_profile_id")]
        public Int32? UserProfileID { get; set; }

        [JsonProperty("iswharehouse")]
        public Boolean? IsWharehouse { get; set; }
    }
}