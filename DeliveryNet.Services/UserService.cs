using System;
using DeliveryNet.Data;
using DeliveryNet.Data.Context;
using DeliveryNet.Interfaces;
using System.ComponentModel.Composition;
using System.Linq;

namespace DeliveryNet.Services
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IUserService))]
    public class UserService : IUserService
    {
        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public UserService(DeliveryNetContext ctx)
        {
            _context = ctx;
        }

        #endregion

        public string AddUser(Customer user)
        {
            string result = "0";
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                result = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    result = ex.Message;
                }
            }

            return result;
        }
    }
}
