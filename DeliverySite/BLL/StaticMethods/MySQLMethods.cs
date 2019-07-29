using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.StaticMethods
{
    public class MySQLMethods
    {
        public static Boolean MySQLInjectionChecker(string value)
        {
            bool islegal = false;
            if (value.Length > 0)
            {
                char[] legalchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЫЪЬЮЯабвгдеёжзийклмнопрстуфхцчшщэъьыюя01234567890.,@_-()+=/\\{}[] ".ToCharArray();
                islegal = true;
                // посимвольно проверяем пришедший string
                for (int i = 0; i < value.Length; i++)
                {
                    // если символ в строке отсутсвет в массиве разрешенных, возвращаем false
                    if (value.LastIndexOfAny(legalchars, i, 1) < 0)
                    {
                        islegal = false;
                        break;
                    }
                }
            }
            return islegal;
        }
    }
}