﻿using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class StepBack : ICommand
{
    public async Task<string> ExecuteAsync(ISessionService session)
    {
        return "";
    }
}
