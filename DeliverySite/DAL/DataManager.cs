using Delivery.DAL.Attributes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Delivery.DAL
{
    public class DataManager
    {
        #region General Methods
        public void QueryWithoutReturnData(MySqlCommand commandIn, string sqlQuery)
        {
            var connectionString = Globals.Settings.DefaultConnectionStringName;
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = new MySqlCommand { CommandText = sqlQuery, Connection = connection };

            if (commandIn != null)
            {
                foreach (var parameters in commandIn.Parameters)
                {
                    command.Parameters.Add(parameters);
                }
            }
            command.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();

            #region статистика по количеству запросов за 10 минут
            try
            {
                var queryCount = HttpContext.Current.Application["Queries"];
                if (queryCount != null)
                    HttpContext.Current.Application["Queries"] = Convert.ToInt32(queryCount) + 1;
                if (queryCount == null)
                    HttpContext.Current.Application["Queries"] = 1;
                var time = HttpContext.Current.Application["QueriesDateTime"];
                if (time == null)
                    HttpContext.Current.Application["QueriesDateTime"] = DateTime.Now;
                if (time != null && DateTime.Now.Subtract(Convert.ToDateTime(time)).TotalSeconds >= 600)
                {
                    var writeString = HttpContext.Current.Application["QueriesDateTime"] + "," +
                                      HttpContext.Current.Application["Queries"] + "\n";
                    using (StreamWriter outfile = new StreamWriter(HttpContext.Current.Server.MapPath("~/query-counter.txt"), true))
                    {
                        outfile.Write(writeString);
                    }
                    HttpContext.Current.Application["QueriesDateTime"] = DateTime.Now;
                    HttpContext.Current.Application["Queries"] = 0;
                }
            }
            catch (Exception) { }
            #endregion;

        }

        public DataSet QueryWithReturnDataSet(string sqlQuery)
        {
            //try
            //{
                var connectionString = Globals.Settings.DefaultConnectionStringName;
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                var command = new MySqlCommand { CommandText = sqlQuery, Connection = connection };
                var adapter = new MySqlDataAdapter { SelectCommand = command };
                var dataset = new DataSet();
                adapter.Fill(dataset);
                connection.Close();
                connection.Dispose();

                #region статистика по количеству запросов за 10 минут
                try
                {
                    var queryCount = HttpContext.Current.Application["Queries"];
                    if (queryCount != null)
                        HttpContext.Current.Application["Queries"] = Convert.ToInt32(queryCount) + 1;
                    if (queryCount == null)
                        HttpContext.Current.Application["Queries"] = 1;
                    var time = HttpContext.Current.Application["QueriesDateTime"];
                    if (time == null)
                        HttpContext.Current.Application["QueriesDateTime"] = DateTime.Now;
                    if (time != null && DateTime.Now.Subtract(Convert.ToDateTime(time)).TotalSeconds >= 600)
                    {
                        var writeString = HttpContext.Current.Application["QueriesDateTime"] + "," +
                                          HttpContext.Current.Application["Queries"] + "\n";
                        using (StreamWriter outfile = new StreamWriter(HttpContext.Current.Server.MapPath("~/query-counter.txt"), true))
                        {
                            outfile.Write(writeString);
                        }
                        HttpContext.Current.Application["QueriesDateTime"] = DateTime.Now;
                        HttpContext.Current.Application["Queries"] = 0;
                    }
                }
                catch (Exception) { }
                #endregion

                return dataset;
            //}
            //catch(Exception ex)
            //{
                //return QueryWithReturnDataSet(sqlQuery);
                
            //}
        }
        #endregion

        #region Delete Method
        public void DeleteData<T>(T obj)
        {
            var type = obj.GetType();
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            var id = type.GetProperty("ID").GetValue(obj, null);
            var sql = String.Format("DELETE FROM {1} WHERE ID = {0}", id, tableName);
            this.QueryWithoutReturnData(null, sql);
        }
        #endregion

        #region Delete Method with Loging
        public void DeleteData<T>(T obj, int outedUserID, string userIP, string pageName)
        {
            var type = obj.GetType();
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            var id = type.GetProperty("ID").GetValue(obj, null);

            //поднимаем fullticketid для таблиц goods и tickets
            var ticketFullSecureID = String.Empty;
            var userIdInt = 0;
            if (tableName.ToString() == "tickets")
            {
                var oldObjRe = GetDataBy(obj, "ID", null);
                ticketFullSecureID = oldObjRe.GetType().GetProperty("FullSecureID").GetValue(oldObjRe, null);
                if (oldObjRe.GetType().GetProperty("UserID") != null && !String.IsNullOrEmpty(Convert.ToString(oldObjRe.GetType().GetProperty("UserID").GetValue(oldObjRe, null))))
                {
                    userIdInt = Convert.ToInt32(oldObjRe.GetType().GetProperty("UserID").GetValue(oldObjRe, null));
                }
            }

            if (tableName.ToString() == "goods")
            {
                var oldObjRe = GetDataBy(obj, "ID", null);
                ticketFullSecureID = oldObjRe.GetType().GetProperty("TicketFullSecureID").GetValue(oldObjRe, null);
            }

            if (tableName.ToString() == "usersprofiles")
            {
                var oldObjRe = GetDataBy(obj, "ID", null);
                if (oldObjRe.GetType().GetProperty("UserID") != null && !String.IsNullOrEmpty(Convert.ToString(oldObjRe.GetType().GetProperty("UserID").GetValue(oldObjRe, null))))
                {
                    userIdInt = Convert.ToInt32(oldObjRe.GetType().GetProperty("UserID").GetValue(oldObjRe, null));
                }
            }

            //логируем
            var sql2 = String.Format("INSERT INTO `userslog` ( `UserID`, `Method`, `TableName`, `PropertyName`, `FieldID`, `OldValue`, `NewValue`, `DateTime`, `UserIP`,`PageName`,`TicketFullSecureID`, `TicketUserID`) VALUES(\"{0}\",\"Delete\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", \"{10}\"); ",
                             outedUserID, tableName, null, id, null, null, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userIP, pageName, ticketFullSecureID, userIdInt);

            var sql = String.Format("DELETE FROM {1} WHERE ID = {0}; {2}", id, tableName, sql2);
            this.QueryWithoutReturnData(null, sql);
        }
        #endregion

        #region Create Method
        public void CreateData<T>(T obj)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties();
            var dateBaseSetAttribute = typeof(DataBaseSetAttribute);
            var fields = String.Empty;
            var values = String.Empty;
            var tableName = type.GetProperty("TableName").GetValue(obj, null).ToString();

            var command = new MySqlCommand();

            //создаем sql & command
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj, null);
                var field = property.Name;
                var attributes = property.GetCustomAttributes(dateBaseSetAttribute, true);
                if (!attributes.Any()) continue; //выбираем только те properties, которые помечены атрибутом DataBaseSetAttribute
                if(tableName != "ticket_statistic")
                    if (field == "ChangeDate" || value == null) continue; //не записываем дату изменения, так как только создаем объект
                command.Parameters.AddWithValue("@" + field, value);
                fields += field + ",";
                values += "@" + field + ",";
            }
            var sql = String.Format("INSERT INTO {0} ({1}) VALUES({2})", tableName, fields.Remove(fields.Length - 1), values.Remove(values.Length - 1));
            this.QueryWithoutReturnData(command, sql);

            if (tableName == "tickets")
            {
                var sql2 = String.Format("INSERT INTO `tickets_backup` ({0}) VALUES({1})", fields.Remove(fields.Length - 1), values.Remove(values.Length - 1));
                this.QueryWithoutReturnData(command, sql2);

                
            }

            if (tableName == "goods")
            {
                var sql2 = String.Format("INSERT INTO `goods_backup` ({0}) VALUES({1})", fields.Remove(fields.Length - 1), values.Remove(values.Length - 1));
                this.QueryWithoutReturnData(command, sql2);
            }
        }
        #endregion

        #region Update Method
        public void UpdateDate<T>(T obj) where T : new()
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            var dateBaseSetAttribute = typeof(DataBaseSetAttribute);
            var id = obj.GetType().GetProperty("ID").GetValue(obj, null);
            var seter = String.Empty;
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            var command = new MySqlCommand();

            var oldObj = new T();
            oldObj.GetType().GetProperty("ID").SetValue(oldObj, id, null);
            var oldObjRe = new T();
            var userIDField = obj.GetType().GetProperty("UserID");
            String userID = null;
            if (userIDField != null)
            {
                userID = Convert.ToString(obj.GetType().GetProperty("UserID").GetValue(obj, null));
            }
            if (userID != null)
            {
                var userId = Convert.ToInt32(obj.GetType().GetProperty("UserID").GetValue(obj, null));
                oldObj.GetType().GetProperty("UserID").SetValue(oldObj, userId, null);
                oldObjRe = GetDataBy(oldObj, "ID", "UserID");
            }
            else
            {
                oldObjRe = GetDataBy(oldObj, "ID", null);
            }

            //создаем sql & command
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj, null);
                var field = property.Name;
                var oldValue = oldObjRe.GetType().GetProperty(field).GetValue(oldObjRe, null) ?? String.Empty;
                var attributes = property.GetCustomAttributes(dateBaseSetAttribute, true);
                if (value != null && (oldValue.ToString() != value.ToString() || field == "ChangeDate") && field != "CreateDate" && attributes.Any())
                {
                    command.Parameters.AddWithValue("@" + field, value);
                    seter += field + "=@" + field + ",";
                } //если поле было проинициализировано && (если поля изменились || если поле ChangeDate || если поле AgreedCost || если поле DeliveryCost) && если поле не CreateDate && если помечено аттрибутом DateBaseSetAttribute

            }
            //баг 240
            if (seter.Length > 1)
            {
                var sql = String.Format("UPDATE {0} SET {1} WHERE ID ={2}", tableName, seter.Remove(seter.Length - 1), id);
                this.QueryWithoutReturnData(command, sql);
            }
        }
        #endregion

        #region Update Method with Loging
        public void UpdateDate<T>(T obj, int outedUserID, string userIP, string pageName) where T : new()
        {
            var sql2 = string.Empty;
            var type = obj.GetType();
            var properties = type.GetProperties();
            var dateBaseSetAttribute = typeof(DataBaseSetAttribute);
            var id = obj.GetType().GetProperty("ID").GetValue(obj, null);
            var seter = String.Empty;
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            var command = new MySqlCommand();

            var oldObj = new T();
            oldObj.GetType().GetProperty("ID").SetValue(oldObj, id, null);
            var oldObjRe = new T();

            var userIDField = obj.GetType().GetProperty("UserID");
            String userID = null;

            if (userIDField != null)
            {
                userID = Convert.ToString(obj.GetType().GetProperty("UserID").GetValue(obj, null));
            }

            if (!String.IsNullOrEmpty(userID) && userID != "0")
            {
                var userId = Convert.ToInt32(obj.GetType().GetProperty("UserID").GetValue(obj, null));
                oldObj.GetType().GetProperty("UserID").SetValue(oldObj, userId, null);
                oldObjRe = GetDataBy(oldObj, "ID", "UserID");
            }
            else
            {
                oldObjRe = GetDataBy(oldObj, "ID", null);
            }

            var userIdInt = 0;
            if (oldObjRe.GetType().GetProperty("UserID") != null && !String.IsNullOrEmpty(Convert.ToString(oldObjRe.GetType().GetProperty("UserID").GetValue(oldObjRe, null))))
            {
                userIdInt = Convert.ToInt32(oldObjRe.GetType().GetProperty("UserID").GetValue(oldObjRe, null));
            }

            //если таблица tickets, то записываем fullticketid
            var ticketFullSecureID = String.Empty;
            if (oldObjRe.GetType().GetProperty("TableName").GetValue(oldObjRe, null).ToString() == "tickets" && oldObjRe.GetType().GetProperty("FullSecureID").GetValue(oldObjRe, null) != null)
            {
                ticketFullSecureID = oldObjRe.GetType().GetProperty("FullSecureID").GetValue(oldObjRe, null).ToString();
            }

            if (oldObjRe.GetType().GetProperty("TableName").GetValue(oldObjRe, null).ToString() == "goods")
            {
                ticketFullSecureID = oldObjRe.GetType().GetProperty("TicketFullSecureID").GetValue(oldObjRe, null).ToString();
            }

            //создаем sql & command
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj, null);
                var field = property.Name;
                var oldValue = oldObjRe.GetType().GetProperty(field).GetValue(oldObjRe, null) ?? String.Empty;
                var attributes = property.GetCustomAttributes(dateBaseSetAttribute, true);
                if (value != null && (oldValue.ToString() != value.ToString() || field == "ChangeDate") && field != "CreateDate" && attributes.Any())
                {
                    command.Parameters.AddWithValue("@" + field, value);
                    seter += field + "=@" + field + ",";
                    if (field != "ChangeDate")
                    {
                        sql2 += String.Format("INSERT INTO `userslog` ( `UserID`, `Method`, `TableName`, `PropertyName`, `FieldID`, `OldValue`, `NewValue`, `DateTime`, `UserIP`,`PageName`,`TicketFullSecureID`, `TicketUserID`) VALUES(\"{0}\",\"Update\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", \"{10}\"); ",
                             outedUserID, tableName, field, id, oldValue, value, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userIP, pageName, ticketFullSecureID, userIdInt);
                    } //не логируем даты изменения
                } //если поле было проинициализировано && (если поля изменились || если поле ChangeDate || если поле AgreedCost || если поле DeliveryCost) && если поле не CreateDate && если помечено аттрибутом DateBaseSetAttribute

            }
            //баг 240
            if (seter.Length > 1)
            {
                var sql = String.Format("UPDATE {0} SET {1} WHERE ID ={2}; {3}", tableName, seter.Remove(seter.Length - 1), id, sql2);
                this.QueryWithoutReturnData(command, sql);


            }
        }
        #endregion

        #region GetAllData Method
        public DataSet GetAllData<T>(T obj, string orderBy, string direction, string whereField)
        {
            orderBy = orderBy ?? "ID";
            direction = direction ?? "DESC";

            var type = obj.GetType();
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            string sql;
            if (whereField != null)
            {
                var secondFieldValue = type.GetProperty(whereField).GetValue(obj, null);
                sql = String.Format("SELECT * FROM {0} WHERE {1} = '{2}' ORDER BY {3} {4}", tableName, whereField, secondFieldValue, orderBy, direction);

            }
            else
            {
                sql = String.Format("SELECT * FROM {0} ORDER BY {1} {2}", tableName, orderBy, direction);
            }
            return QueryWithReturnDataSet(sql);
        }

        public DataSet GetLastData<T>(T obj, int count)
        {
            var type = obj.GetType();
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            var sql = String.Format("SELECT * FROM {0} ORDER BY CreateDate DESC LIMIT {1}", tableName, count);
            return QueryWithReturnDataSet(sql);
        }

        public DataSet GetLastDataBy<T>(T obj, int count, string field)
        {
            var type = obj.GetType();
            var tableName = type.GetProperty("TableName").GetValue(obj, null);
            var fieldValue = type.GetProperty("TableName").GetValue(obj, null);
            var sql = String.Format("SELECT * FROM `{0}` WHERE `{2}` = '{3}' ORDER BY `CreateDate` DESC LIMIT {1}", tableName, count, field, fieldValue);
            return QueryWithReturnDataSet(sql);
        }
        #endregion

        #region GetDataBy Method
        public dynamic GetDataBy<T>(T obj, string fieldName, string secondFieldName)
        {
            var dateBaseGetAttribute = typeof(DataBaseGetAttribute);
            var fields = String.Empty;
            var result = obj.GetType();
            var properties = result.GetProperties();
            var searchField = String.Empty;
            var searchValue = String.Empty;
            var secondSearchField = String.Empty;
            var secondSearchValue = String.Empty;
            var tableName = result.GetProperty("TableName").GetValue(obj, null);
            var flagForSqlQuery = true;
            foreach (PropertyInfo property in properties)
            {
                var field = property.Name;
                var attributes = property.GetCustomAttributes(dateBaseGetAttribute, true);
                if (!attributes.Any()) continue; //выбираем только те properties, которые помечены атрибутом DataBaseGetAttribute
                fields += field + ",";
                if (field == fieldName)
                {
                    searchField = field;
                    searchValue = property.GetValue(obj, null).ToString();
                    if (searchValue.Contains('"'))
                    {
                        flagForSqlQuery = false;
                    }
                }
                if (secondFieldName != null)
                {
                    if (field == secondFieldName)
                    {
                        secondSearchField = field;
                        secondSearchValue = property.GetValue(obj, null).ToString();
                        if (secondSearchValue.Contains('"'))
                        {
                            flagForSqlQuery = false;
                        }
                    }
                }
            }
            string sql;
            if (flagForSqlQuery)
            {
                sql = secondFieldName != null ? String.Format("SELECT {1} FROM {3} WHERE {0} = \"{2}\" AND {4} = \"{5}\"", searchField, fields.Remove(fields.Length - 1), searchValue, tableName, secondSearchField, secondSearchValue)
                : String.Format("SELECT {1} FROM {3} WHERE {0} = \"{2}\" ", searchField, fields.Remove(fields.Length - 1), searchValue, tableName);
            }
            else
            {
                sql = secondFieldName != null ? String.Format("SELECT {1} FROM {3} WHERE {0} = '{2}' AND {4} = '{5}'", searchField, fields.Remove(fields.Length - 1), searchValue, tableName, secondSearchField, secondSearchValue)
                : String.Format("SELECT {1} FROM {3} WHERE {0} = '{2}' ", searchField, fields.Remove(fields.Length - 1), searchValue, tableName);
            }

            var connectionString = Globals.Settings.DefaultConnectionStringName;
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = new MySqlCommand { CommandText = sql, Connection = connection };
            var adapter = new MySqlDataAdapter { SelectCommand = command };
            var dataset = new DataSet();
            adapter.Fill(dataset);

            foreach (PropertyInfo property in properties)
            {
                var field = property.Name;
                var attributes = property.GetCustomAttributes(dateBaseGetAttribute, true);
                if (!attributes.Any()) continue; //выбираем только те properties, которые помечены атрибутом DataBaseGetAttribute
                if ((dataset.Tables.Count > 0) && (dataset.Tables[0].Rows.Count > 0) && dataset.Tables[0].Rows[0][field] != DBNull.Value)
                {
                    property.SetValue(obj, dataset.Tables[0].Rows[0][field], null);
                }
            }
            connection.Close();
            return obj;
        }
        #endregion
    }
}