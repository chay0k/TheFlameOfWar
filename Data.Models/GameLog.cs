namespace Data.Models;
public class GameLog
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string Operation { get; set; } = "";
    public string Description { get; set; } = "";
    DateTime DateTime { get; set; }
}
