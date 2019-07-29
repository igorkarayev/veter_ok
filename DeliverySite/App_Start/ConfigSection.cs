using System;
using System.Configuration;

namespace Delivery
{
    public class ConfigSection :ConfigurationSection
    {
        [ConfigurationProperty("defaultConnectionStringName")]
        public String DefaultConnectionStringName {
            get
            {
                return (String)base["defaultConnectionStringName"];
            }
            set
            {
                base["defaultConnectionStringName"] = value;
            } 
        }

        [ConfigurationProperty("appServiceSecureKey")]
        public String AppServiceSecureKey
        {
            get
            {
                return (String)base["appServiceSecureKey"];
            }
            set
            {
                base["appServiceSecureKey"] = value;
            }
        }

        [ConfigurationProperty("firstUserApiKey")]
        public String FirstUserApiKey
        {
            get
            {
                return (String)base["firstUserApiKey"];
            }
            set
            {
                base["firstUserApiKey"] = value;
            }
        }
    }
}