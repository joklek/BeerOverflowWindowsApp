namespace WebApi.UnitTests.TestData
{
    public class BarProviderTestData
    {
        public static string FacebookValidDataResponse = @"{""data"":[{""location"":{""latitude"":54,""longitude"":25},""name"":""TestPlace1"",""category_list"":[{""name"":""Bar""}]},{""location"":{""latitude"":54,""longitude"":25,},""name"":""TestPlace2"",""category_list"":[{""name"":""Bar""}]}]}";
        public static string FourSquareValidDataResponse = @"{""response"":{""venues"":[{""name"":""TestPlace1"",""location"":{""lat"":54,""lng"":25,""distance"":0},""categories"":[{""id"":""4bf58dd8d48988d1c4941735"",""name"":""Restaurant""}]},{""name"":""TestPlace2"",""location"":{""lat"":54,""lng"":25,""distance"":0},""categories"":[{""id"":""4bf58dd8d48988d1c4941735"",""name"":""Restaurant""}]}]}}";
        public static string GoogleValidDataResponse = @"{""html_attributions"":[],""results"":[{""geometry"":{""location"":{""lat"":54,""lng"":25}},""name"":""TestPlace1""},{""geometry"":{""location"":{""lat"":54,""lng"":25}},""name"":""TestPlace2""}],}";
        public static string[] TripAdvisorValidDataResponse = {
            @"{""data"":[{""location_id"":""1"",""name"":""TestPlace1"",""distance"":""0"",},{""location_id"":""2"",""name"":""TestPlace2"",""distance"":""0"",}]}",
            @"{""data"":[]}",
            @"{""latitude"":""54"",""location_id"":""1"",""longitude"":""25"",""category"":{""name"":""restaurant""},""subcategory"":[{""name"":""sit_down""}]}",
            @"{""latitude"":""54"",""attraction_types"":[{""name"":""bar/ clubs""}],""location_id"":""2"",""longitude"":""25"",""groups"":[{""name"":""Nightlife"",""categories"":[{""name"":""Bars & Clubs""}]}],""category"":{""name"":""attraction""},""subcategory"":[{""name"":""nightlife""}]}"
        };

        public static object[] ValidDataCases =
        {
            new object[] { 0.0, 0.0, 0 },
            new object[] { -90.0, -180.0, 50 },
            new object[] { 90.0, 180.0, 50 },
            new object[] { -90.0, 180.0, 50 },
            new object[] { 90.0, -180.0, 50 },
        };

        public static readonly object[] InvalidDataCases =
        {
            new object[] { -91.0, 1.0, 1, "latitude" },
            new object[] { 91.0, 1.0, 1, "latitude" },
            new object[] { 1.0, -181.0, 1, "longitude" },
            new object[] { 1.0, 181.0, 1, "longitude" },
            new object[] { 1.0, 1, -1, "radius" },
            new object[] { 1.0, 1, 9999999, "radius" }
        };
    }
}