using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class GuardUnitListEntity
{
    public int Id { get; set; }
    public Guid GuardId { get; set; }
    public Guid UnitId { get; set; }
    public GuardEntity Guard { get; set; }
    public UnitEntity Unit { get; set; }
    public int Count { get; set; }
}
