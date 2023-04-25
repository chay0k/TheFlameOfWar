using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models;
public class Enemy
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Count { get; set; }
    public string Info { get; set; }
    public List<Unit> Units { get; set; }
}
