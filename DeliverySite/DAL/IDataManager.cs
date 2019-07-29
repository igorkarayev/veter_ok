using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Delivery.DAL
{
    public interface IDataManager
    {
        Int32 ID { get; set; }
        String TableName { get; set; }
        void Delete(Int32 id);
        void Create();
        DataSet GetAllItems();
        DataSet GetAllItems(string orderBy, string direction, string whereField);
        dynamic GetById();
        void Update();
    }
}