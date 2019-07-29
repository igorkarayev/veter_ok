using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Delivery.DAL
{
    public class LogErrors
    {
        public static void LogedError(Exception exception, string ip)
        {
            var dm = new DataManager();
            var errorMessage = "Message: " + exception.Message.Replace("'", "").Replace("\"", "") + "\nSource: " +
                               exception.Source.Replace("'", "").Replace("\"", "")
                               + "\n\n\nStackTrase: " + exception.ToString().Replace("'", "").Replace("\"", "");
            if (exception.InnerException != null)
            {
                errorMessage += "\n\n\n\n\n\n----------------------------------INNER EXCEPTION START ----------------------------------"
                                + "\n\n\nIE Message: " +
                                exception.InnerException.Message.Replace("'", "").Replace("\"", "") + "\nIE Source: " + exception.InnerException.Source.Replace("'", "").Replace("\"", "")
                                + "\n\n\nIE StackTrase: " +
                                exception.InnerException.ToString().Replace("'", "").Replace("\"", "")
                                + "\n\n\n----------------------------------INNER EXCEPTION END ----------------------------------";
            }

            dm.QueryWithoutReturnData(null, String.Format("INSERT INTO `errorslog` (`Date`, `StackTrase`, `ErrorType`, `IP`) VALUES('{0}', '{1}', '{2}', '{3}')",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), errorMessage, exception.GetType(), ip));
        }

        public static IPAddress GetIp(HttpRequest request)
        {
            string ipString;
            if (string.IsNullOrEmpty(request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                ipString = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                ipString =
                    request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(",".ToCharArray(),
                        StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault();
            }

            IPAddress result;
            if (!IPAddress.TryParse(ipString, out result))
            {
                result = IPAddress.None;
            }

            return result;
        }
    }
}