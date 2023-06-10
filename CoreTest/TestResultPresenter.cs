using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tests;
public class TestResultPresenter : IResultPresenter
{
    public void PresentResult(string result)
    {
        Console.WriteLine(result);
    }
}
