using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Potato_Distro_HRM__Web_.model {
    public class DatabaseConnection {
        public static NpgsqlConnection GetConnection() {
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                   "10.71.34.236", "5432", "potato_admin",
                   ".netpotatodistro", "potato_db");
            return new NpgsqlConnection(connstring);
        }
    }
}