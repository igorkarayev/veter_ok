using System.Xml;
using System.Net;
using System.IO;
using System;
using System.Collections;
using System.Text;

namespace DeliverySite.BLL
{
    public class SOAPCost
    {
        public int cost = 0;
        public int type = 0;
    }

    public class SOAPGood
    {
        public string goodsId = "";
        public string name = "";
        public decimal? amount = 0;
        public string warehouse = "";
        public int? cost = 0;
        public decimal? discount = 0;
        public int? status = 0;
        public string comment = "";
        public int? hasDebt = 0;
    }

    public class SOAPPoint
    {
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public decimal? weight { get; set; }
        public decimal? volume { get; set; }
        public int? readyTime { get; set; }
        public int? dueTime { get; set; }
        public int? serviceTime { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string label { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }
        public string text3 { get; set; }
        public string orderNumber { get; set; }
        public string phoneNumbers { get; set; }
        public SOAPCost[] costs { get; set; }
        public string zoneId { get; set; }
        public int radius { get; set; }
        public DateTime? deliveryDate { get; set; }
        public int priority { get; set; }
        public int insertionPriority { get; set; }
        public SOAPGood goods { get; set; }
    }

    public class NavSOAP
    {
        public static string[] navTypes = new string[] { "vehicles", "drivers", "zones", "vrp" };
        public string CallWebService(string _action = "getCurrentPoints/", int type = 3, object o = null)
        {
            var _url = "http://gps.beltranssat.by/vrp-rs/ws/" + navTypes[type] + "?wsdl";

            string objString = "";
            if (o != null)
            {
                if (_action == "addPoint")
                {
                        objString += "<point>";
                        foreach (var prop in typeof(SOAPPoint).GetProperties())
                        {
                            var val = prop.GetValue(o, null);
                            if (val != null && val.ToString() != "")
                            {
                                objString += "<ns1:" + prop.Name + ">";
                                objString += prop.GetValue(o, null);
                                objString += "</ns1:" + prop.Name + ">";
                            }
                        }
                        objString += "</point>";
                    objString += "</ns1:" + _action + " >";
                }
            }
            //return null;
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(_action, objString);
            HttpWebRequest webRequest = CreateWebRequest(_url);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            var asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                var temp = webResponse.GetResponseStream();
                if (temp != null)
                {
                    using (StreamReader rd = new StreamReader(temp))
                    {
                        return rd.ReadToEnd();
                    }
                }
                else
                    return null;
            }
        }

        private static HttpWebRequest CreateWebRequest(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            string usernamePassword = "Grundex" + ":" + "111111";
            usernamePassword = Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword));
            webRequest.Headers.Add("Authorization", "Basic " + usernamePassword);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string _action, string obgString = "")
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            var soapRequest =
                @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns1=""http://ws.vrptwserver.beltranssat.by/""><SOAP-ENV:Body><ns1:" +_action + ">" + obgString
                + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
            soapEnvelopeDocument.LoadXml(soapRequest);
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

    }
}