using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class GuardEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Count { get; set; }
    public string Info { get; set; }
    public List<GuardUnitListEntity> GuardUnitLists { get; set; } // Властивість навігації до GuardUnitListEntity
    public List<UnitEntity> Units
    {
        get
        {
            // Повертаємо список юнітів, які мають відповідність у GuardUnitLists
            return GuardUnitLists?.Select(gu => gu.Unit).ToList();
        }
    }
}
