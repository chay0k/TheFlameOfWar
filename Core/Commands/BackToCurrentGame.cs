﻿using Contracts;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands;
public class BackToCurrentGame : ICommand
{
    public async Task<string> ExecuteAsync()
    {
        return "";
    }
}
