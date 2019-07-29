using Delivery.DAL.Attributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Feedback : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Feedback()
        {
            DM = new DataManager();
            this.TableName = "feedback";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SecureID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? UserID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? TypeID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? PriorityID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? StatusID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Title { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Body { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PhotoName { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? CreateDate { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? ChangeDate { get; set; }

        public void Delete(Int32 id, int curentUserId, string curentUserIp, string curentPageName)
        {
            this.ID = id;
            //удаление привязанных комментариев
            DM.QueryWithoutReturnData(null, "DELETE FROM feedbackcomments WHERE FeedbackID = " + id);
            DM.DeleteData(this, curentUserId, curentUserIp, curentPageName);
        }

        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Create()
        {
            DM.CreateData(this);
        }

        public DataSet GetAllItems()
        {
            return DM.GetAllData(this, null, null, null);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public DataSet GetAllItemsByUserID()
        {
            return DM.GetAllData(this, "ChangeDate", "Desc", "UserID");
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public dynamic GetBySecureID()
        {
            return DM.GetDataBy(this, "SecureID", null);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

        public void Update(int userId, string userIp, string pageName)
        {
            DM.UpdateDate(this, userId, userIp, pageName);
        }

        public static Dictionary<int, string> Types = new Dictionary<int, string>()
        {
            {0, "Предложение"},
            {1, "Жалоба"},
            {2, "Заявка на добавление наименования"},
            {3, "Обращение в тех. отдел"},
            {4, "Обращение в бухгалтерию"},
            {5, "Обращение к менеджерам"},
            {6, "Обращение по вопросам цен, отклонений"},

        };

        public static Dictionary<int, string> Priorities = new Dictionary<int, string>()
        {
            {0, "Не срочное"},
            {1, "Нормальное"},
            {2, "Неотложное"},
        };

        public static Dictionary<int, string> Statuses = new Dictionary<int, string>()
        {
            {0, "Открыто"},
            {1, "Закрыто"},
        };
    }
}

