using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using CodeKicker.BBCode;
using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using Delivery.WebServices.Objects;

namespace Delivery.BLL
{
    public class BbCode
    {
        //просчет без юзер ID
        public static String BBcodeToHtml(string bbCode)
        {
            var parser = new BBCodeParser(new[]
                {
                    new BBTag("b", "<b>", "</b>"), 
                    new BBTag("i", "<span style=\"font-style:italic;\">", "</span>"), 
                    new BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>"),
                    new BBTag("list", "<ul>", "</ul>"), 
                    new BBTag("*", "<li>", "</li>", true, false), 
                    new BBTag("url", "<a href=\"${href}\" target=\"_new\">", "</a>", new BBAttribute("href", ""), new BBAttribute("href", "href")), 
                });
            return parser.ToHtml(bbCode).Replace(Environment.NewLine, "<br />");
        }

    }
}