using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.DAL.DataBaseObjects;
using DeliveryNet.Data;
using Delivery.ViewModels.Shared;

namespace Delivery.ViewModels.Account
{
    public class AccountViewModel : LayoutViewModel
    {
        public Page PageInfo { get; set; }
        public Customer UserModel { get; set; }
    }
}