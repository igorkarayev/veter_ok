using System;
using System.Web;
using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.StaticMethods
{
    public class ApiMethods
    {
        public static bool IsApiAuthRequest()
        {
            var userIdString = HttpContext.Current.Request.Headers["userid"];
            var apiKey = HttpContext.Current.Request.Headers["apikey"];
            //если в бекенде включен доступ к апи без ключа - пропускаем авторизацию
            if (BackendHelper.TagToValue("allow_unauth_api_request") == "true")
                return true;
            if (String.IsNullOrEmpty(userIdString))
                return false;
            if (String.IsNullOrEmpty(apiKey))
                return false;

            int userId;
            if (!Int32.TryParse(userIdString, out userId))
                return false;

            Users userById = new Users()
            {
                ID = userId
            };
            userById.GetById();

            if (userById.ApiKey != apiKey)
                return false;

            var user = new Users {ID = userId};
            user.GetById();
            //если клиенту закрыт доступ к апи - выставляем false
            if (user.AllowApi == 0)
                return false;
            //если клиент заблокирован - выставляем false
            if (user.Status == 1 || user.Status == 3)
                return false;
            return user.ApiKey == apiKey;
        }

        public static void ReturnNotAuth()
        {
            HttpContext.Current.Response.ContentType = "application/json; charset=UTF-8";
            HttpContext.Current.Response.Write("not authorized");
        }

        public static void LoggingRequest(string methodName, string apiName, string apiType, string incomingParameters, int responseBodyLenght, int userId, string apiKey)
        {
            if(BackendHelper.TagToValue("allow_unauth_api_request") == "true")
                return;
            var apiLog = new ApiLog
            {
                MethodName = methodName,
                ApiName = apiName,
                ApiType = apiType,
                IncomingParameters = incomingParameters,
                ResponseBodyLenght = responseBodyLenght,
                UserID = userId,                
                ApiKey = apiKey,
                UserIP = OtherMethods.GetIPAddress()
            };
            apiLog.Create();
        }
    }
}