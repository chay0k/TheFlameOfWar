﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class CityBuildingEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PanteonEntity Panteon { get; set; }
}
