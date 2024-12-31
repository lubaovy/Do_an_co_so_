using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace QLKHO
{
    [Serializable]
    public partial class Entities
    {
        private Entities(DbConnection connectionString, bool contextOwnsConnection = true)
            :base(connectionString, contextOwnsConnection) { }
        public static Entities CreateEntities(bool contextOwnsConnection = true)
        {
            //Doc file connect
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read);
            connect cp = (connect)bf.Deserialize(fs);

            //Decrypt noi dung
            string servername = Encryptor.Decrypt(cp.servername, "qweryuiop", true);
            string username = Encryptor.Decrypt(cp.username, "qweryuiop", true);
            string pass = Encryptor.Decrypt(cp.password, "qweryuiop", true);
            string database = Encryptor.Decrypt(cp.database, "qweryuiop", true);

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            SqlConnectionStringBuilder sqlConnectBuilder = new SqlConnectionStringBuilder();
            sqlConnectBuilder.DataSource = servername;
            sqlConnectBuilder.InitialCatalog = database;
            sqlConnectBuilder.UserID = username;
            sqlConnectBuilder.Password = pass;

            //string sqlConnectionString = sb.ConnectionString;
            string sqlConnectionString = sqlConnectBuilder.ConnectionString;


            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlConnectionString;

            entityBuilder.Metadata = @"res://*/KHOHANG.csdl|res://*/KHOHANG.ssdl|res://*/KHOHANG.msl";
            EntityConnection connection = new EntityConnection(entityBuilder.ConnectionString);

            fs.Close();
            return new Entities(connection);
        }
    }
}
