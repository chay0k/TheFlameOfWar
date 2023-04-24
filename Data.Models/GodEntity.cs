using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class GodEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PanteonEntity Panteon { get; set; }
    public virtual ICollection<PlayerConditionEntity> PlayerConditions { get; set; }
}
