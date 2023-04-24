using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models;
public class Panteon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Unit> Units { get; set; }
    public List<God> Gods { get; set; }
    //public 
}
