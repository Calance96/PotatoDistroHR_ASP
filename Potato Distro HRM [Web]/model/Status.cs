using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Potato_Distro_HRM__Web_
{
    public class Status
    {
        public enum Type
        {
            Pending = 1, Approved = 2, Rejected = 3, Cancelled = 4
        }

        public static bool ChangeLeaveStatus(Type status, int leaveId)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);
            conn.Open();

            NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE LEAVE SET Status = :status WHERE Id = :id";
            cmd.Parameters.Add(new NpgsqlParameter("status", (int)status));
            cmd.Parameters.Add(new NpgsqlParameter("id", leaveId));
            int success = cmd.ExecuteNonQuery();
            conn.Close();
            if (success > 0)
                return true;
            else
                return false;
        }
    }
}