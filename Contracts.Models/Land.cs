namespace Contracts.Models;
public class Land
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsPassable { get; set; }
        public string Emoji { get; set; } = "";
    }
