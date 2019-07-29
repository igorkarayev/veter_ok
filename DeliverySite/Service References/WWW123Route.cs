﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Delivery.WWW123Route
{
    using System.Runtime.Serialization;
    using System;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Result", Namespace = "http://schemas.datacontract.org/2004/07/SR.PublicAPI")]
    [System.SerializableAttribute()]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthTokenField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] DetailsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorLevelField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AuthToken
        {
            get
            {
                return this.AuthTokenField;
            }
            set
            {
                if ((object.ReferenceEquals(this.AuthTokenField, value) != true))
                {
                    this.AuthTokenField = value;
                    this.RaisePropertyChanged("AuthToken");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                if ((object.ReferenceEquals(this.DataField, value) != true))
                {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Details
        {
            get
            {
                return this.DetailsField;
            }
            set
            {
                if ((object.ReferenceEquals(this.DetailsField, value) != true))
                {
                    this.DetailsField = value;
                    this.RaisePropertyChanged("Details");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorLevel
        {
            get
            {
                return this.ErrorLevelField;
            }
            set
            {
                if ((object.ReferenceEquals(this.ErrorLevelField, value) != true))
                {
                    this.ErrorLevelField = value;
                    this.RaisePropertyChanged("ErrorLevel");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                if ((object.ReferenceEquals(this.MessageField, value) != true))
                {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "GetDataOptions", Namespace = "http://schemas.datacontract.org/2004/07/SR.PublicAPI")]
    [System.SerializableAttribute()]
    public partial class GetDataOptions : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ClientsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateFromField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateToField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OrderCategoriesField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderCategoryExchangeCodeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TimezoneOffsetField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int VariantField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool VariantsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool VariantsDataField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Clients
        {
            get
            {
                return this.ClientsField;
            }
            set
            {
                if ((this.ClientsField.Equals(value) != true))
                {
                    this.ClientsField = value;
                    this.RaisePropertyChanged("Clients");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DateFrom
        {
            get
            {
                return this.DateFromField;
            }
            set
            {
                if ((object.ReferenceEquals(this.DateFromField, value) != true))
                {
                    this.DateFromField = value;
                    this.RaisePropertyChanged("DateFrom");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DateTo
        {
            get
            {
                return this.DateToField;
            }
            set
            {
                if ((object.ReferenceEquals(this.DateToField, value) != true))
                {
                    this.DateToField = value;
                    this.RaisePropertyChanged("DateTo");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool OrderCategories
        {
            get
            {
                return this.OrderCategoriesField;
            }
            set
            {
                if ((this.OrderCategoriesField.Equals(value) != true))
                {
                    this.OrderCategoriesField = value;
                    this.RaisePropertyChanged("OrderCategories");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderCategoryExchangeCode
        {
            get
            {
                return this.OrderCategoryExchangeCodeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.OrderCategoryExchangeCodeField, value) != true))
                {
                    this.OrderCategoryExchangeCodeField = value;
                    this.RaisePropertyChanged("OrderCategoryExchangeCode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TimezoneOffset
        {
            get
            {
                return this.TimezoneOffsetField;
            }
            set
            {
                if ((this.TimezoneOffsetField.Equals(value) != true))
                {
                    this.TimezoneOffsetField = value;
                    this.RaisePropertyChanged("TimezoneOffset");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Variant
        {
            get
            {
                return this.VariantField;
            }
            set
            {
                if ((this.VariantField.Equals(value) != true))
                {
                    this.VariantField = value;
                    this.RaisePropertyChanged("Variant");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Variants
        {
            get
            {
                return this.VariantsField;
            }
            set
            {
                if ((this.VariantsField.Equals(value) != true))
                {
                    this.VariantsField = value;
                    this.RaisePropertyChanged("Variants");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool VariantsData
        {
            get
            {
                return this.VariantsDataField;
            }
            set
            {
                if ((this.VariantsDataField.Equals(value) != true))
                {
                    this.VariantsDataField = value;
                    this.RaisePropertyChanged("VariantsData");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "urn:SmartRoutes/API", ConfigurationName = "WWW123Route.IAPI")]
    public interface IAPI
    {

        [System.ServiceModel.OperationContractAttribute(Action = "urn:SmartRoutes/API/IAPI/Login", ReplyAction = "urn:SmartRoutes/API/IAPI/LoginResponse")]
        Delivery.WWW123Route.Result Login(string userName, string password);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:SmartRoutes/API/IAPI/SetData", ReplyAction = "urn:SmartRoutes/API/IAPI/SetDataResponse")]
        Delivery.WWW123Route.Result SetData(string xml, string authToken);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:SmartRoutes/API/IAPI/GetData", ReplyAction = "urn:SmartRoutes/API/IAPI/GetDataResponse")]
        Delivery.WWW123Route.Result GetData(Delivery.WWW123Route.GetDataOptions options, string authToken);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:SmartRoutes/API/IAPI/SetVariantForExecution", ReplyAction = "urn:SmartRoutes/API/IAPI/SetVariantForExecutionResponse")]
        Delivery.WWW123Route.Result SetVariantForExecution(string orderCategoryExchangeCode, string variantDate, int variant, string authToken);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAPIChannel : Delivery.WWW123Route.IAPI, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class APIClient : System.ServiceModel.ClientBase<Delivery.WWW123Route.IAPI>, Delivery.WWW123Route.IAPI
    {

        public APIClient()
        {
        }

        public APIClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public APIClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public APIClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public APIClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public Delivery.WWW123Route.Result Login(string userName, string password)
        {
            return base.Channel.Login(userName, password);
        }

        public Delivery.WWW123Route.Result SetData(string xml, string authToken)
        {
            return base.Channel.SetData(xml, authToken);
        }

        public Delivery.WWW123Route.Result GetData(Delivery.WWW123Route.GetDataOptions options, string authToken)
        {
            return base.Channel.GetData(options, authToken);
        }

        public Delivery.WWW123Route.Result SetVariantForExecution(string orderCategoryExchangeCode, string variantDate, int variant, string authToken)
        {
            return base.Channel.SetVariantForExecution(orderCategoryExchangeCode, variantDate, variant, authToken);
        }
    }
}
