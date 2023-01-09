using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models;
public class Answer
{
    public bool IsCompleted { get; set; }
    public string Details { get; set; }

    public Answer(bool isCompleted = false, string details = "")
    {
        IsCompleted = isCompleted;
        Details = details;
    }
}
