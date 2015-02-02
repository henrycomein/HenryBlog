using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.BusinessLayer
{
    public class AboutMeBLL
    {
        #region GetData

        
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static AboutMe Get()
        {
            return  AboutMeDAL.Get().ToEntity<AboutMe>().FirstOrDefault();
        }

        #endregion

        
    }
}

