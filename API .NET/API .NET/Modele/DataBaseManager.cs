using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class DataBaseManager
    {
        protected MySqlConnection connection;

        public DataBaseManager()
        {
            this.InitConnexion();
        }

        private void InitConnexion()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=business_leeroy;UID=root;PASSWORD=";
            this.connection = new MySqlConnection(connectionString);
        }
    }
}
