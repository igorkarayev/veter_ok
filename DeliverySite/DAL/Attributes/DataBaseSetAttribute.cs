using System;

namespace Delivery.DAL.Attributes
{
    public sealed class DataBaseSetAttribute : Attribute
    {
        public string Desc;
        public DataBaseSetAttribute() { }
        public DataBaseSetAttribute(string str)
        {
            Desc = str;
        }
    }
}