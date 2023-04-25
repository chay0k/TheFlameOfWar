using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models;
public class God
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Panteon Panteon { get; set; }

}
