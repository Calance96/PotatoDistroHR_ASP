using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Potato_Distro_HRM__Web_.model
{
    public class Department
    {
        private static string QUERY_DEPT_NAME = "SELECT name from department where id=:id";
        private static String QUERY_MANAGER_NAME = "SELECT fname, lname from employee where id =:id";
        private static String QUERY_MANAGER_ID = "SELECT id from employee where fname = :fname and lname = :lname";
        public int id { get; set; }
        public String name { get; set; }
        public String hotline { get; set; }
        public String managerName { get; set; }

        public static String GetManagerName(int managerId)
        {
            if (managerId == 0)
                return "";

            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(QUERY_MANAGER_NAME, conn))
            {
                conn.Open();
                sqlCommand.Parameters.Add(new NpgsqlParameter("id", managerId));

                using (NpgsqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetString(0) + " " + reader.GetString(1);
                }
            }
        }

        public static int GetManagerId(String managerName)
        {
            int curPos = managerName.IndexOf(" ");

            while (curPos != -1)
            {
                String fname = managerName.Substring(0, curPos);
                String lname = managerName.Substring(curPos + 1);

                using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(QUERY_MANAGER_ID, conn))
                {
                    conn.Open();
                    sqlCommand.Parameters.Add(new NpgsqlParameter("fname", fname));
                    sqlCommand.Parameters.Add(new NpgsqlParameter("lname", lname));
                    using (NpgsqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                            return reader.GetInt32(0);
                    }
                }
                curPos = managerName.IndexOf(" ", curPos + 1);
            }
            return 0;
        }

        public static string GetDepartmentName(int deptId)
        {
            if (deptId == 0)
                return "";

            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(QUERY_DEPT_NAME, conn))
            {
                conn.Open();
                sqlCommand.Parameters.Add(new NpgsqlParameter("id", deptId));

                using (NpgsqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }
    }
}