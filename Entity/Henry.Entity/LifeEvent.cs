using System;
namespace Henry.Entity
{
    public class LifeEvent:BaseModel
    {
        public LifeEvent()
        {
           LE_Status = -1;
        }
        public virtual Int32 LE_ID{get;set;}
        
        //发生日期
        public virtual DateTime LE_Date{get;set;}
        
        public virtual Int32 LE_CateogryID{get;set;}
        public virtual Int32 Month { get; set; }
        public virtual String LE_Title{get;set;}
        public virtual String LE_Images { get; set; }
        public virtual String LE_Desc{get;set;}
        
        public virtual Int32 LE_Status{get;set;}
        
        public virtual DateTime LE_CreateTime{get;set;}
        
    }
}