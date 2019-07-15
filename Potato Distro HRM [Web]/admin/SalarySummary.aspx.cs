using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_.admin
{
    public partial class SalarySummary : System.Web.UI.Page
    {
        Employee employee;
        protected void Page_Load(object sender, EventArgs e)
        {
            int empid = Convert.ToInt32(Request.QueryString["empid"]);
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from EMPLOYEE", conn);

        }
    }
}