﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models;
public class InnerParametres
{
    public long ChatId { get; set; }
    public User User { get; set; }
    public string Text { get; set; }
}
