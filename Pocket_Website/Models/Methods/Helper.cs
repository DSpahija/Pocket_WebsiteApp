using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pocket_Website.Models
{
    public class Helper
    {
        string providerName = "System.Data.SqlClient";
        string serverName = ".";
        string databaseName = "Pocket_DatabaseEntities1";

        SqlConnectionStringBuilder sqlBuilder =
            new SqlConnectionStringBuilder();

        public EntityConnectionStringBuilder entityBuilder =
            new EntityConnectionStringBuilder();
        public void StringConnectionConvert()
        {
            sqlBuilder.DataSource = serverName;
            sqlBuilder.InitialCatalog = databaseName;
            sqlBuilder.IntegratedSecurity = true;
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.
            entityBuilder.Metadata = @"res://*/Models.PocketModel.csdl|res://*/Models.PocketModel.ssdl|res://*/Models.PocketModel.msl";
            Console.WriteLine(entityBuilder.ToString());
        }

    }
}