using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class CityEntity
{
    public int Id { get; set; }
    public PanteonEntity Panteon { get; set; }
    public virtual ICollection<PlayerConditionEntity> PlayerConditions { get; set; }

}
