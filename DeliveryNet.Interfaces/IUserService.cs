using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryNet.Data;

namespace DeliveryNet.Interfaces
{
    public interface IUserService
    {
        string AddUser(Customer user);
    }
}
