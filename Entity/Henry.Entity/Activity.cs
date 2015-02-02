using System;
namespace Henry.Entity
{
    public class Activity:BaseModel
    {
        public Activity()
        {
           A_Status = -1;
        }
        public virtual Int32 A_ID{get;set;}
        
        //发生日期
        public virtual DateTime A_Date{get;set;}
        
        public virtual String A_Title{get;set;}
        
        public virtual String A_Desc{get;set;}
        
        public virtual Int32 A_Status{get;set;}
        
        public virtual DateTime A_CreateTime{get;set;}
        
    }
}