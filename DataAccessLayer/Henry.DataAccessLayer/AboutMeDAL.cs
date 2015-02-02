using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;

namespace Henry.DataAccessLayer
{
    public class AboutMeDAL
    {
        public static DataTable Get()
        {
            var queryString = "SELECT * FROM AboutMe WHERE A_Status=1" ;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        
    }
}


