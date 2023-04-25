using Contracts.Models;

namespace Data.Models;
public class LandEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsPassable { get; set; }
        public string Emoji { get; set; } = "";
}
