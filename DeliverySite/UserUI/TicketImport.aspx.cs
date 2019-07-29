using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using Delivery.WebServices.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.IO;
using ClosedXML.Excel;
using System.Data.SqlClient;

namespace Delivery.UserUI
{
    public partial class TicketImport : UserBasePage
    {
        DataTable selectProfilesDataSet;
        DataManager dm = new DataManager();
        List<string> RecipientStreetPrefixes;
        DataTable warehouses = new DataTable();
        string patternCost = @"^\d+,\d{2}$";
        string patternPhone = @"^\+\d{3} [(]{1}\d{2}[)] \d{3}-\d{2}-\d{2}$";
        string patternDate = @"\d{2}.\d{2}.\d{4}$";
        string patternTTNSeria = @"^[a-zA-Zа-яА-Я]{2}$";
        string patternTTNNumber = @"^\d{7}$";

        List<string> rowsError = new List<string>();
        List<string> rowsColor = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            selectProfilesDataSet = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `usersprofiles` WHERE `UserID` = {0} AND `StatusID` = 1", UserID)).Tables[0];
            warehouses = dm.QueryWithReturnDataSet("SELECT * FROM `warehouses`").Tables[0];
            RecipientStreetPrefixes = new List<string>()
            { "ул.", "аллея", "бул.", "дор.",
                "линия", "маг.", "мик-н", "наб.", "пер.",
                "пл.", "пр.", "пр-кт", "ряд", "тракт", "туп.", "ш." };
        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }

        protected void DownloadXLS_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/OCTET-STREAM";
            Response.AppendHeader("Content-Disposition", "attachment; filename=пример.xlsx");
            Response.TransmitFile(Server.MapPath(@"~/OtherFiles/пример.xlsx"));
            Response.End();
        }

        protected void Useless_Click(object sender, EventArgs e)
        {
            //   SELECT goodname = (SELECT Description FROM `goods` AS G WHERE G.TicketFullSecureID = T.FullSecureID) FROM `tickets` AS T
            //   SELECT goodname FROM `tickets` AS T JOIN `goods` AS G ON G.TicketFullSecureID = T.FullSecureID
            DataTable excelDt = new DataTable();
            for(int j = 0; j < 6; j++)
            {
                excelDt.Columns.Add(new DataColumn());
            }

            var tickets = dm.QueryWithReturnDataSet("SELECT T.CityID, T.CreateDate, T.RecipientPhone, T.RecipientPhoneTwo, G.Description, G.Model " +
                "FROM `tickets` AS T JOIN `goods` AS G ON G.TicketFullSecureID = T.FullSecureID");
            foreach(DataRow row in tickets.Tables[0].Rows)
            {
                DataRow newRow = excelDt.NewRow();

                var CreateDate = String.IsNullOrEmpty(row["CreateDate"].ToString()) ? "" : row["CreateDate"].ToString();
                var Phone = String.IsNullOrEmpty(row["RecipientPhone"].ToString()) ? "" : row["RecipientPhone"].ToString();
                var PhoneTwo = String.IsNullOrEmpty(row["RecipientPhoneTwo"].ToString()) ? "" : row["RecipientPhoneTwo"].ToString();
                var Desc = String.IsNullOrEmpty(row["Description"].ToString()) ? "" : row["Description"].ToString();
                var Model = String.IsNullOrEmpty(row["Model"].ToString()) ? "" : row["Model"].ToString();
                var City = CityHelper.CityIDToCityName(row["CityID"].ToString());

                newRow[0] = CreateDate;
                newRow[1] = Phone;
                newRow[2] = PhoneTwo;
                newRow[3] = Desc;
                newRow[4] = Model;
                newRow[5] = City;

                excelDt.Rows.Add(newRow);
            }

            /*string attachment = "attachment; filename=tickets.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            string tab = "";
            foreach (DataColumn dc in excelDt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in excelDt.Rows)
            {
                tab = "";
                for (i = 0; i < excelDt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();*/

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(excelDt, "Customers");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void ImportXLS_Click(object sender, EventArgs e)
        {
            DataSet result = new DataSet();
            string message;

            if (FileUploader.HasFile)
            {
                result = ReadXLSMethods.ReadXLS(FileUploader.PostedFile.InputStream);
                if(result.Tables.Count == 0)
                {
                    labelStatus.Text = "Неверный тип файла";
                    return;
                }

                var id = Page.Request.Params["id"];
                var userInSession = (Users)Session["userinsession"];
                string userDiscount;
                int userID;

                int rowsCount = 0;
                int rowsSuccessCount = 0;

                var user = new Users
                {
                    ID = UserID,
                    Login = userInSession.Login,
                    Email = userInSession.Email,
                    Name = userInSession.Name,
                    Family = userInSession.Family,
                    Role = userInSession.Role,
                    RussRole = UsersHelper.RoleToRuss(userInSession.Role),
                    IsCourse = userInSession.IsCourse
                };
                userID = user.ID;

                if (user.Role != Users.Roles.User.ToString())
                {
                    userDiscount = "0";
                    userID = 1;
                }
                else
                {
                    userDiscount = user.Discount.ToString();
                }

                int i = 0;
                result.Tables[0].Columns.Add(new DataColumn());
                result.Tables[0].Columns.Add(new DataColumn());
                foreach (DataColumn column in result.Tables[0].Columns)
                    column.ColumnName = i++.ToString();

                if (result.Tables[0].Columns.Count < 22)
                {
                    labelStatus.Text = "Недостаточное количество колонок в таблице";
                    return;
                }

                if (result.Tables[0].Rows.Count == 0)
                {
                    labelStatus.Text = "Пустая таблица";
                    return;
                }

                rowsCount = result.Tables[0].Rows.Count;

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    bool norm = true;
                    List<string> errors = new List<string>();
                    Tickets ticket = new Tickets();
                    int? profileType = null;

                    /// проверка профиля
                    foreach (DataRow rowProfile in selectProfilesDataSet.Rows)
                    {
                        string selectProfileName = String.IsNullOrEmpty(rowProfile["CompanyName"].ToString())
                            ? String.Format("{0} {1}", rowProfile["FirstName"], rowProfile["LastName"])
                            : rowProfile["CompanyName"].ToString();
                        if (selectProfileName == row.ItemArray[0].ToString())
                        {
                            ticket.UserProfileID = Convert.ToInt32(rowProfile["ID"].ToString());
                            ticket.UserID =
                                UsersHelper.GetUserIdByUserProfileId(Convert.ToInt32(ticket.UserProfileID));

                            profileType = Convert.ToInt32(rowProfile["TypeID"].ToString());
                            if (profileType == 2)
                            {
                                ticket.PrintNaklInMap = 1;
                                ticket.PrintNakl = 0;
                            }
                            else
                            {
                                ticket.PrintNaklInMap = 0;
                                ticket.PrintNakl = 1;
                            }

                            break;
                        }
                    }
                    if (ticket.UserProfileID == null)
                    {
                        norm = false;
                        errors.Add("неверный профиль");
                    }
                    /// проверка ID города
                    ///   
                    int cityId;
                    if (Int32.TryParse(row.ItemArray[1].ToString(), out cityId) != false)
                    {
                        if (CityHelper.CityIDToCityName(row.ItemArray[1].ToString()) != null)
                        {
                            ticket.CityID = Convert.ToInt32(row.ItemArray[1].ToString());
                        }
                        else
                        {
                            norm = false;
                            errors.Add("неверный ID города");
                        }
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный ID города");
                    }
                    ///
                    /// Проверка типа улицы
                    if (RecipientStreetPrefixes.Contains(row.ItemArray[2].ToString()))
                    {
                        ticket.RecipientStreetPrefix = row.ItemArray[2].ToString();
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный префикс улицы");
                    }

                    ///
                    /// Проверка района
                    if (row.ItemArray[3].ToString() != string.Empty)
                        ticket.RecipientStreet = row.ItemArray[3].ToString();
                    else
                    {
                        norm = false;
                        errors.Add("не введена улица");
                    }
                    ///
                    ///
                    int houseNumber;
                    if (Int32.TryParse(row.ItemArray[4].ToString(), out houseNumber) == true)
                    {
                        ticket.RecipientStreetNumber = houseNumber.ToString();
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный номер дома");
                    }
                    /// Заполнение корпуса и квартиры
                    if (row.ItemArray[5].ToString() != string.Empty)
                        ticket.RecipientKorpus = row.ItemArray[5].ToString();
                    if (row.ItemArray[6].ToString() != string.Empty)
                        ticket.RecipientKvartira = row.ItemArray[6].ToString();
                    ///
                    /// Проверка фамилии, имени и отчества
                    if (row.ItemArray[7].ToString().Length < 2 || row.ItemArray[8].ToString().Length < 2 || row.ItemArray[9].ToString().Length < 2)
                    {
                        norm = false;
                        errors.Add("неверные ФИО");
                    }
                    else
                    {
                        ticket.RecipientFirstName = row.ItemArray[7].ToString();
                        ticket.RecipientLastName = row.ItemArray[8].ToString();
                        ticket.RecipientThirdName = row.ItemArray[9].ToString();
                    }
                    ///
                    /// Проверка номеров телефона
                    string input1 = row.ItemArray[10].ToString();
                    string input2 = row.ItemArray[11].ToString();

                    int numMatches1 = 0;
                    foreach (Match m in Regex.Matches(input1, patternPhone))
                        numMatches1++;
                    int numMatches2 = 0;
                    foreach (Match m in Regex.Matches(input2, patternPhone))
                        numMatches2++;

                    if (row.ItemArray[10].ToString() == string.Empty || numMatches1 != 1)
                    {
                        norm = false;
                        errors.Add("неверный 1 номер телефона");
                    }
                    else
                    {
                        ticket.RecipientPhone = row.ItemArray[10].ToString();
                    }

                    if (row.ItemArray[11].ToString() != string.Empty && numMatches2 != 1)
                    {
                        norm = false;
                        errors.Add("неверный 2 номер телефона");
                    }
                    else
                    {
                        if (row.ItemArray[11].ToString() != string.Empty)
                            ticket.RecipientPhoneTwo = row.ItemArray[11].ToString();
                    }
                    ///
                    /// Проверка стоимости доставки для получателя
                    string patternCost = @"^\d+,\d{2}$";

                    if (row.ItemArray[12].ToString() != string.Empty)
                    {
                        //if (Regex.Matches(row.ItemArray[12].ToString(), patternCost).Count == 1)
                        try
                        {
                            ticket.DeliveryCost = Convert.ToDecimal(row.ItemArray[12].ToString());
                        }
                        catch
                        {
                            norm = false;
                            errors.Add("неверная стоимость для получателя");
                        }
                    }
                    else
                        ticket.DeliveryCost = 0;
                    ///
                    /// Проверка товаров
                    List<Goods> goods = new List<Goods>();
                    List<string> goodsStr = row.ItemArray[13].ToString().Split(';').ToList();
                    if (goodsStr.Count == 0)
                    {
                        norm = false;
                        errors.Add("не введены товары");
                    }
                    foreach (string goodStr in goodsStr)
                    {
                        List<string> goodParams = goodStr.Split('|').ToList();
                        if (goodParams.Count != 4)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары");
                            goods = new List<Goods>();
                            break;
                        }
                        string goodDesc = goodParams[0];
                        string goodModel = goodParams[1];
                        string goodCost = goodParams[2];
                        string goodCount = goodParams[3];
                        ///
                        var titleForCheck = new Titles();
                        titleForCheck.Name = goodDesc.Trim();
                        titleForCheck.GetByName();
                        if (titleForCheck.ID == 0)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары");
                            goods = new List<Goods>();
                            break;
                        }

                        int count;
                        if (Int32.TryParse(goodCount, out count) == false)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары");
                            goods = new List<Goods>();
                            break;
                        }

                        if (Regex.Matches(goodCost, patternCost).Count != 1)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары");
                            goods = new List<Goods>();
                            break;
                        }

                        Goods good = new Goods()
                        {
                            Description = OtherMethods.BeInUseReplace(goodDesc).Trim(),
                            Model = goodModel.Trim(),
                            Number = Convert.ToInt32(goodCount),
                            Cost = Convert.ToDecimal(goodCost)
                        };
                        goods.Add(good);
                    }
                    ///
                    /// Получение стоимости за товары
                    ticket.AssessedCost = 0;
                    foreach (Goods good in goods)
                    {
                        ticket.AssessedCost += good.Number * good.Cost;
                    }
                    ///
                    /// Проверка количества грузовых мест
                    int boxesNumber;
                    if (row.ItemArray[14].ToString() != string.Empty)
                    {
                        if (Int32.TryParse(row.ItemArray[14].ToString(), out boxesNumber) != false)
                            ticket.BoxesNumber = boxesNumber;
                        else
                        {
                            norm = false;
                            errors.Add("неверно введено количество коробок");
                        }
                    }
                    else
                    {
                        ticket.BoxesNumber = 1;
                    }
                    ///
                    /// Проверка даты отправки
                    //if (Regex.Matches(row.ItemArray[15].ToString(), patternDate).Count == 1)
                    //{
                    DateTime date1;
                    try
                    {
                        date1 = Convert.ToDateTime(Convert.ToDateTime(row.ItemArray[15].ToString()).ToString("dd-MM-yyyy"));

                        DateTime date2 = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));

                        if (ticket.CityID != null)
                        {
                            var allCity = Application["CityList"] as List<City>;
                            var city = allCity.First(u => u.ID == Convert.ToInt32(ticket.CityID));
                            var district = new Districts { ID = Convert.ToInt32(city.DistrictID) };
                            district.GetById();
                            var selectDayOfWeek = Convert.ToDateTime(Convert.ToDateTime(row.ItemArray[15].ToString()).ToString("dd-MM-yyyy")).DayOfWeek;
                            if ((selectDayOfWeek == DayOfWeek.Monday && district.Monday != 1)
                                || (selectDayOfWeek == DayOfWeek.Tuesday && district.Tuesday != 1)
                                || (selectDayOfWeek == DayOfWeek.Wednesday && district.Wednesday != 1)
                                || (selectDayOfWeek == DayOfWeek.Thursday && district.Thursday != 1)
                                || (selectDayOfWeek == DayOfWeek.Friday && district.Friday != 1)
                                || (selectDayOfWeek == DayOfWeek.Saturday && district.Saturday != 1)
                                || (selectDayOfWeek == DayOfWeek.Sunday && district.Sunday != 1))
                            {
                                norm = false;
                                errors.Add("отправка возможна только в дни отправки");
                            }

                            if (date1 < date2 && ticket.CityID != 11)
                            {
                                norm = false;
                                errors.Add("отправка в города отличные от Минска возможна только на следующий день после создания заявки!");
                            }

                            if (date1 < Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")) && ticket.CityID == 11)
                            {
                                norm = false;
                                errors.Add("доставка по Минску возможна только с текущего дня и далее!");
                            }

                            ticket.DeliveryDate = date1;
                        }
                    }
                    catch (Exception)
                    {
                        norm = false;
                        errors.Add("неверная дата");
                    }
                    //}
                    //else
                    //{
                        //norm = false;
                        //errors.Add("неверный формат даты отправки");
                    //}
                    ///
                    /// Комментарии
                    if (row.ItemArray[16].ToString() != string.Empty)
                        ticket.Note = row.ItemArray[16].ToString();
                    ///
                    /// Проверка данных документов
                    if (profileType == 2 || profileType == 3)
                    {
                        if (Regex.Matches(row.ItemArray[17].ToString(), patternTTNSeria).Count == 1)
                        {
                            ticket.TtnSeria = row.ItemArray[17].ToString();
                        }
                        else
                        {
                            norm = false;
                            errors.Add("неверно введена ТТН серия");
                        }

                        if (Regex.Matches(row.ItemArray[18].ToString(), patternTTNNumber).Count == 1)
                        {
                            ticket.TtnNumber = row.ItemArray[18].ToString();
                        }
                        else
                        {
                            norm = false;
                            errors.Add("неверно введен ТТН номер");
                        }
                    }
                    else
                    {
                        ticket.TtnNumber = string.Empty;
                        ticket.TtnSeria = string.Empty;                        
                    }

                    ticket.OtherDocuments = row.ItemArray[19].ToString();
                    ///
                    /// Проверка серии и номера паспорта
                    if (Regex.Matches(row.ItemArray[20].ToString(), patternTTNSeria).Count == 1)
                        ticket.PassportSeria = row.ItemArray[20].ToString();
                    else
                    {
                        norm = false;
                        errors.Add("неверно введена серия паспорта");
                    }

                    if (Regex.Matches(row.ItemArray[21].ToString(), patternTTNNumber).Count == 1)
                        ticket.PassportNumber = row.ItemArray[21].ToString();
                    else
                    {
                        norm = false;
                        errors.Add("неверно введен номер паспорта");
                    }
                    ///
                    /// Подсчет стоимости за услугу
                    List<GoodsFromAPI> goodsAPI = new List<GoodsFromAPI>();
                    foreach(Goods good in goods)
                    {
                        GoodsFromAPI goodAPI = new GoodsFromAPI()
                        {
                            Description = good.Description,
                            Number = good.Number
                        };
                        goodsAPI.Add(goodAPI);
                    }

                    if(goodsAPI.Count > 0)
                    {
                        var gruzobozCost = Calculator.Calculate(goodsAPI, Convert.ToInt32(ticket.CityID), userID, Convert.ToInt32(ticket.UserProfileID), profileType.ToString(), ticket.AssessedCost.ToString(), Convert.ToInt32(userDiscount));
                        //если стоимость за услугу не конвертируется в decimal - значит записываем 0
                        try
                        {
                            ticket.GruzobozCost = Decimal.Parse(gruzobozCost);
                        }
                        catch (Exception)
                        {
                            ticket.GruzobozCost = 0;
                        }
                        ///
                        /// Расчет веса
                        ticket.Weight = GoodsHelper.GoodsWeight(goodsAPI);
                    }
                    
                    ///
                    /// Заполнение полей склада
                    if(warehouses.Rows.Count == 0)
                    {
                        norm = false;
                        errors.Add("ошибка склада");
                    }
                    DataRow rowWareHouses = warehouses.Rows[0];

                    ticket.SenderApartmentNumber = rowWareHouses["ApartmentNumber"].ToString();
                    ticket.SenderCityID = Convert.ToInt32(rowWareHouses["CityID"].ToString());
                    ticket.SenderHousing = rowWareHouses["Housing"].ToString();
                    ticket.SenderStreetName = rowWareHouses["StreetName"].ToString(); 
                    ticket.SenderStreetNumber = rowWareHouses["StreetNumber"].ToString(); 
                    ticket.SenderStreetPrefix = rowWareHouses["StreetPrefix"].ToString();
                    ///
                    /// СОЗДАНИЕ ЗАЯВКИ
                    /// 

                    string error = string.Empty;
                    foreach(string errorMsg in errors)
                    {
                        error += errorMsg;
                        error += ", ";
                    }
                    rowsError.Add(error);

                    string color;
                    if(errors.Count != 0)
                    {
                        color = "redRow";
                    }
                    else
                    {
                        color = "greenRow";
                    }
                    rowsColor.Add(color);

                    if(norm == true)
                    {
                        if (id == null)
                        {
                            rowsSuccessCount++;

                            //записываем хеши для заявки
                            if (user.Role != Users.Roles.User.ToString())
                            {
                                ticket.SecureID = OtherMethods.CreateUniqId("1" + DateTime.Now.ToString("yyMdHms"));
                                ticket.FullSecureID = OtherMethods.CreateFullUniqId("1" + DateTime.Now.ToString("yyMdHms"));
                            }
                            else
                            {
                                ticket.SecureID = OtherMethods.CreateUniqId(user.ID + DateTime.Now.ToString("yyMdHms"));
                                ticket.FullSecureID = OtherMethods.CreateFullUniqId(user.ID + DateTime.Now.ToString("yyMdHms"));
                            }

                            for(int j = 0; j < goods.Count; j++) //сохраняем заявки
                            {
                                goods[j].TicketFullSecureID = ticket.FullSecureID;
                                goods[j].Create();
                            }
                            ticket.Create();
                            #region Отправка емейла логистам

                            var logistRb = BackendHelper.TagToValue("logist_rb_email");
                            var logistMinsk = BackendHelper.TagToValue("logist_minsk_email");
                            var isMinskRegion = BackendHelper.TagToValue("logist_minsk_region");
                            var senderCity = new City { ID = Convert.ToInt32(ticket.SenderCityID) };
                            senderCity.GetById();

                            if (isMinskRegion != "true")
                            {
                                if (!string.IsNullOrEmpty(logistRb) && senderCity.DistrictID != 10)
                                {
                                    try
                                    {

                                        EmailMethods.MailSend("Создана новая заявка с забором в РБ",
                                        String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistRb, true);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(logistMinsk) && !(ticket.SenderCityID == 11 && ticket.SenderStreetName.ToLower() == BackendHelper.TagToValue("loading_point_street").ToLower() &&
                                        ticket.SenderStreetNumber == BackendHelper.TagToValue("loading_point_street_number")))
                                    {
                                        try
                                        {
                                            EmailMethods.MailSend("Создана новая заявка с забором в Минске и районе",
                                            String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistMinsk, true);
                                        }
                                        catch (Exception)
                                        {
                                            // ignored
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(logistRb) && senderCity.RegionID != 1)
                                {
                                    try
                                    {
                                        EmailMethods.MailSend("Создана новая заявка с забором в РБ",
                                        String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistRb, true);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(logistMinsk) && !(ticket.SenderCityID == 11 && ticket.SenderStreetName.ToLower() == BackendHelper.TagToValue("loading_point_street") &&
                                        ticket.SenderStreetNumber == BackendHelper.TagToValue("loading_point_street_number")))
                                    {
                                        try
                                        {
                                            EmailMethods.MailSend("Создана новая заявка с забором в Минске и области",
                                            String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistMinsk, true);
                                        }
                                        catch (Exception)
                                        {
                                            // ignored
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }

                for(int j = 0; j < result.Tables[0].Rows.Count; j++)
                {
                    result.Tables[0].Rows[j][22] = rowsError[j];
                    result.Tables[0].Rows[j][23] = rowsColor[j];
                }

                labelStatus.Text = "Успешно импортировано " + rowsSuccessCount.ToString() +
                    " из " + rowsCount.ToString() + " заявок";
                ListXML.DataSource = result.Tables[0];
                ListXML.DataBind();
            }
        }
    }
}