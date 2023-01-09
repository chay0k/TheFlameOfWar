namespace Contracts.Models;
public class Land
    {
        public Guid Id { get; set; }
        public int CardNumber { get; set; } = 0;
        public string Name { get; set; } = "";
        public int AccessLevel { get; set; } = 0;
        public Passabilities Passability { get; set; }
        public Guid PassabilityOption { get; set; }
        public int Steps { get; set; }
        public string Emoji { get; set; } = "";
}
