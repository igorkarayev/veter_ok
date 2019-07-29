using System;

namespace Delivery.DAL.Attributes
{
    public sealed class DataBaseGetAttribute : Attribute
    {
        public string Desc;
        public DataBaseGetAttribute() { }
        public DataBaseGetAttribute(string str)
        {
            Desc = str;
        }
    }
}