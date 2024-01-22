namespace Flagzor
{
    public class FeatureFlag
    {
        public required string Feature { get; set; }

        public required bool Enabled { get; set; }

        public Dictionary<string, string> Attributes { get; set;} = [];
    }
}
