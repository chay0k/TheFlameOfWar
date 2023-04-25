using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class BuildingEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCityBuilding { get; set; }
    public int PanteonId { get; set; }
    public PanteonEntity Panteon { get; set; }
}
