using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class PanteonEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UnitEntity> Units { get; set; }
    public List<GodEntity> Gods { get; set; }
    public virtual ICollection<PlayerConditionEntity> PlayerConditions { get; set; }
}
