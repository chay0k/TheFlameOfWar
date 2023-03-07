using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class LastCommandEntity
{
    public long Id { get; set; }
    public Guid CommandId { get; set; }
    public CommandEntity Command { get; set; }
}
