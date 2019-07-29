using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using Delivery.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class SenderProfiles : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public SenderProfiles()
        {
            DM = new DataManager();
            this.TableName = "senderprofiles";            
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ProfileID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ProfileName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CitySelectedString { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CityID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CityCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CityDeliveryDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CityDeliveryTerms { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String AddressPrefix { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String AddressStreet { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String AddressHouseNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String AddressKorpus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String AddressKvartira { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientPhone1 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientPhone2 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String FirstName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String LastName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ThirdName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SendDate { get; set; }

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

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }        

        public string GetProfileNameById(Int32 id)
        {
            ID = id;
            return ProfileName;
        }
    }
}