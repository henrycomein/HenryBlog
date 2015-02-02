using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class AboutMeDAL
    {
        public static DataTable Get()
        {
            var queryString = "SELECT top 1 * FROM [AboutMe] WHERE A_Status<>2";
            return MySqlHelper.ExecuteQueryList(queryString);
        }
 
        public static bool Update(AboutMe condition)
        {
            
            var cmdString ="UPDATE [AboutMe] SET A_Content=@A_Content WHERE A_ID=@A_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@A_ID",condition.A_ID);
            paramer[0].SqlDbType = SqlDbType.Int;
            paramer[1] = new SqlParameter("@A_Content",condition.A_Content);
            paramer[1].SqlDbType = SqlDbType.NVarChar;
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


