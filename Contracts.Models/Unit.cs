using Data.Models;
namespace Contracts.Models;
public class Unit
{
    public Guid Id { get; set; }
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int MaxHP { get; set; }
    public int EnergyCost { get; set; }
    public int Weight { get; set; }
    public UnitTypes UnitType { get; set; }
    public int ExstraEnergy { get; set; }
    public Panteon Panteon { get; set; }
}
