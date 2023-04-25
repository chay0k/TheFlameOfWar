using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts;
public interface ISessionService
{
    Player SessionPlayer { get; set; }
    string LastInput { get; set; }
    long UserTelegramId { get; set; }
    object GetService(Type serviceType);
}
