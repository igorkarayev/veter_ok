using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text.RegularExpressions;
using Delivery.DAL.DataBaseObjects;

namespace DeliverySite.BLL.StaticMethods
{
    public class CreateProfileDoc: System.Web.UI.Page
    {
        private UsersProfiles profile { get; set; }

        public MemoryStream Create(string id)
        {
            int ID;

            if(Int32.TryParse(id, out ID))
            {
                profile = new UsersProfiles { ID = ID };
                profile.GetById();

                switch (profile.TypeID)
                {
                    case 1:
                        return CreateDocFiz();
                    case 2:
                        return CreateDocYur();
                    case 3:
                        return CreateDocInt();
                }
            }      
            
            return null;
        }

        public MemoryStream CreateDocFiz()
        {
            using (WordprocessingDocument wordprocessingDocument =
            WordprocessingDocument.Open(Server.MapPath("~/OtherFiles/ProfileDocs/Ф-образец.docx"), true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordprocessingDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                Regex regexFullName = new Regex("CLIENT");
                docText = regexFullName.Replace(docText, profile.FirstName + " " + 
                    profile.LastName + " " +
                    profile.ThirdName);

                Regex regexPassport = new Regex("PASS");
                docText = regexPassport.Replace(docText, profile.PassportSeria + " " +
                    profile.PassportNumber + " ");

                Regex regexNumber = new Regex("NUM");
                docText = regexNumber.Replace(docText, profile.AgreementNumber.ToUpper());

                Regex regexDay = new Regex("DAY");
                docText = regexDay.Replace(docText, profile.AgreementDate.Value.Day.ToString());

                Regex regexMonth = new Regex("MONTH");
                switch (profile.AgreementDate.Value.Month)
                {
                    case 1:
                        docText = regexMonth.Replace(docText, "Января");
                        break;
                    case 2:
                        docText = regexMonth.Replace(docText, "Февраля");
                        break;
                    case 3:
                        docText = regexMonth.Replace(docText, "Марта");
                        break;
                    case 4:
                        docText = regexMonth.Replace(docText, "Апреля");
                        break;
                    case 5:
                        docText = regexMonth.Replace(docText, "Мая");
                        break;
                    case 6:
                        docText = regexMonth.Replace(docText, "Июня");
                        break;
                    case 7:
                        docText = regexMonth.Replace(docText, "Июля");
                        break;
                    case 8:
                        docText = regexMonth.Replace(docText, "Августа");
                        break;
                    case 9:
                        docText = regexMonth.Replace(docText, "Сентября");
                        break;
                    case 10:
                        docText = regexMonth.Replace(docText, "Октября");
                        break;
                    case 11:
                        docText = regexMonth.Replace(docText, "Ноября");
                        break;
                    case 12:
                        docText = regexMonth.Replace(docText, "Декабря");
                        break;
                }                

                Regex regexYear = new Regex("YEAR");
                docText = regexYear.Replace(docText, profile.AgreementDate.Value.Year.ToString());

                Regex regexPassportVidan = new Regex("VIDAN");
                docText = regexPassportVidan.Replace(docText, profile.PassportData);

                Regex regexPassportDate = new Regex("WHEN");
                docText = regexPassportDate.Replace(docText, profile.PassportDate.ToString());

                Regex regexAddress = new Regex("ADDRESS");
                docText = regexAddress.Replace(docText, profile.Address);

                string[] telefons = profile.ContactPhoneNumbers.Split(';');

                Regex regexTelefon1 = new Regex("TEL");
                if (telefons.Length > 0)
                    docText = regexTelefon1.Replace(docText, telefons[0]);
                else
                    docText = regexTelefon1.Replace(docText, string.Empty);

                Regex regexTelefon2 = new Regex("DOP");
                if (telefons.Length > 1)
                    docText = regexTelefon2.Replace(docText, telefons[1]);
                else
                    docText = regexTelefon2.Replace(docText, string.Empty);

                Regex regexInitName = new Regex("INIT");
                docText = regexInitName.Replace(docText, profile.FirstName + " " +
                    profile.LastName.First() + ". " +
                    profile.ThirdName.First() + ".");

                return CreateDoc(docText);
            }
        }

        public MemoryStream CreateDocYur()
        {
            using (WordprocessingDocument wordprocessingDocument =
            WordprocessingDocument.Open(Server.MapPath("~/OtherFiles/ProfileDocs/Ю-образец.docx"), true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordprocessingDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                Regex regexCompanyName = new Regex("COMPANY");
                if (profile.CompanyName != null)
                    docText = regexCompanyName.Replace(docText, profile.CompanyName);

                Regex regexFullName = new Regex("CLIENT");
                docText = regexFullName.Replace(docText, profile.FirstName + " " +
                    profile.LastName + " " +
                    profile.ThirdName);

                Regex regexNumber = new Regex("NUM");
                docText = regexNumber.Replace(docText, profile.AgreementNumber.ToUpper());

                Regex regexDay = new Regex("DAY");
                docText = regexDay.Replace(docText, profile.AgreementDate.Value.Day.ToString());

                Regex regexMonth = new Regex("MONTH");
                switch (profile.AgreementDate.Value.Month)
                {
                    case 1:
                        docText = regexMonth.Replace(docText, "Января");
                        break;
                    case 2:
                        docText = regexMonth.Replace(docText, "Февраля");
                        break;
                    case 3:
                        docText = regexMonth.Replace(docText, "Марта");
                        break;
                    case 4:
                        docText = regexMonth.Replace(docText, "Апреля");
                        break;
                    case 5:
                        docText = regexMonth.Replace(docText, "Мая");
                        break;
                    case 6:
                        docText = regexMonth.Replace(docText, "Июня");
                        break;
                    case 7:
                        docText = regexMonth.Replace(docText, "Июля");
                        break;
                    case 8:
                        docText = regexMonth.Replace(docText, "Августа");
                        break;
                    case 9:
                        docText = regexMonth.Replace(docText, "Сентября");
                        break;
                    case 10:
                        docText = regexMonth.Replace(docText, "Октября");
                        break;
                    case 11:
                        docText = regexMonth.Replace(docText, "Ноября");
                        break;
                    case 12:
                        docText = regexMonth.Replace(docText, "Декабря");
                        break;
                }

                Regex regexYear = new Regex("YEAR");
                docText = regexYear.Replace(docText, profile.AgreementDate.Value.Year.ToString());
                ///
                Regex regexYurAddr = new Regex("YURADR");
                docText = regexYurAddr.Replace(docText, profile.CompanyAddress);

                Regex regexUNP = new Regex("UNP");
                docText = regexUNP.Replace(docText, profile.UNP);

                Regex regexPostAddr = new Regex("POSTADR");
                docText = regexPostAddr.Replace(docText, profile.PostAddress);

                Regex regexRS = new Regex("EBAN");
                docText = regexRS.Replace(docText, profile.RasShet);

                Regex regexBankName = new Regex("BANK");
                docText = regexBankName.Replace(docText, profile.BankName);

                Regex regexBankAddress = new Regex("ADRBAN");
                docText = regexBankAddress.Replace(docText, profile.BankAddress);

                Regex regexBankCode = new Regex("BANCODE");
                docText = regexBankCode.Replace(docText, profile.BankCode);

                string[] telefons = profile.ContactPhoneNumbers.Split(';');

                Regex regexTelefon1 = new Regex("TEL");
                if (telefons.Length > 0)
                    docText = regexTelefon1.Replace(docText, telefons[0]);
                else
                    docText = regexTelefon1.Replace(docText, string.Empty);

                Regex regexTelefon2 = new Regex("DOP");
                if (telefons.Length > 1)
                    docText = regexTelefon2.Replace(docText, telefons[1]);
                else
                    docText = regexTelefon2.Replace(docText, string.Empty);

                Regex regexInitName = new Regex("INIT");
                docText = regexInitName.Replace(docText, profile.FirstName + " " +
                    profile.LastName.First() + ". " +
                    profile.ThirdName.First() + ".");

                return CreateDoc(docText);
            }
        }

        public MemoryStream CreateDocInt()
        {
            using (WordprocessingDocument wordprocessingDocument =
            WordprocessingDocument.Open(Server.MapPath("~/OtherFiles/ProfileDocs/П-образец.docx"), true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordprocessingDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                Regex regexCompanyName = new Regex("COMPANY");
                docText = regexCompanyName.Replace(docText, profile.CompanyName);

                Regex regexFullName = new Regex("CLIENT");
                docText = regexFullName.Replace(docText, profile.FirstName + " " +
                    profile.LastName + " " +
                    profile.ThirdName);

                Regex regexNumber = new Regex("NUM");
                docText = regexNumber.Replace(docText, profile.AgreementNumber.ToUpper());

                Regex regexDay = new Regex("DAY");
                docText = regexDay.Replace(docText, profile.AgreementDate.Value.Day.ToString());

                Regex regexMonth = new Regex("MONTH");
                switch (profile.AgreementDate.Value.Month)
                {
                    case 1:
                        docText = regexMonth.Replace(docText, "Января");
                        break;
                    case 2:
                        docText = regexMonth.Replace(docText, "Февраля");
                        break;
                    case 3:
                        docText = regexMonth.Replace(docText, "Марта");
                        break;
                    case 4:
                        docText = regexMonth.Replace(docText, "Апреля");
                        break;
                    case 5:
                        docText = regexMonth.Replace(docText, "Мая");
                        break;
                    case 6:
                        docText = regexMonth.Replace(docText, "Июня");
                        break;
                    case 7:
                        docText = regexMonth.Replace(docText, "Июля");
                        break;
                    case 8:
                        docText = regexMonth.Replace(docText, "Августа");
                        break;
                    case 9:
                        docText = regexMonth.Replace(docText, "Сентября");
                        break;
                    case 10:
                        docText = regexMonth.Replace(docText, "Октября");
                        break;
                    case 11:
                        docText = regexMonth.Replace(docText, "Ноября");
                        break;
                    case 12:
                        docText = regexMonth.Replace(docText, "Декабря");
                        break;
                }

                Regex regexYear = new Regex("YEAR");
                docText = regexYear.Replace(docText, profile.AgreementDate.Value.Year.ToString());
                ///
                Regex regexYurAddr = new Regex("YURADR");
                docText = regexYurAddr.Replace(docText, profile.CompanyAddress);

                Regex regexUNP = new Regex("UNP");
                docText = regexUNP.Replace(docText, profile.UNP);

                Regex regexPostAddr = new Regex("POSTADR");
                docText = regexPostAddr.Replace(docText, profile.PostAddress);

                Regex regexRS = new Regex("EBAN");
                docText = regexRS.Replace(docText, profile.RasShet);

                Regex regexBankName = new Regex("BANK");
                docText = regexBankName.Replace(docText, profile.BankName);

                Regex regexBankAddress = new Regex("ADRBAN");
                docText = regexBankAddress.Replace(docText, profile.BankAddress);

                Regex regexBankCode = new Regex("BANCODE");
                docText = regexBankCode.Replace(docText, profile.BankCode);

                string[] telefons = profile.ContactPhoneNumbers.Split(';');

                Regex regexTelefon1 = new Regex("TEL");
                if (telefons.Length > 0)
                    docText = regexTelefon1.Replace(docText, telefons[0]);
                else
                    docText = regexTelefon1.Replace(docText, string.Empty);

                Regex regexTelefon2 = new Regex("DOP");
                if (telefons.Length > 1)
                    docText = regexTelefon2.Replace(docText, telefons[1]);
                else
                    docText = regexTelefon2.Replace(docText, string.Empty);

                Regex regexInitName = new Regex("INIT");
                docText = regexInitName.Replace(docText, profile.FirstName + " " +
                    profile.LastName.First() + ". " +
                    profile.ThirdName.First() + ".");

                return CreateDoc(docText);
            }
        }

        public MemoryStream CreateDoc(string docText)
        {
            var stream = new MemoryStream();
            using (var doc = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
            {
                ParagraphProperties paragraphProperties = new ParagraphProperties(
                  new ParagraphStyleId() { Val = "No Spacing" },
                  new SpacingBetweenLines() { After = "0" }
                  );

                doc.AddMainDocumentPart();               

                using (StreamWriter sw = new StreamWriter(doc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }


                //doc.MainDocumentPart.Document.Body.Append(paragraphProperties);
                return stream;
            }
        }
    }
}