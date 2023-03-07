using Contracts.Models;
namespace Data.Models;
public class ThingEntity
{
    public Guid Id { get; set; }
    public Guid ThingId { get; set; }
    public string Name { get; set; } = "";
    public int Count { get; set; } = 0;
    public ThingTypes ThingType { get; set; }
}
