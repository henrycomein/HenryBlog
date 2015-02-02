using System;
namespace Henry.Entity
{
    public class SystemSetting:BaseModel
    {
        public SystemSetting()
        {
           
        }
        public virtual Int32 SS_ID{get;set;}

        public virtual String SS_Code { get; set; }
        
        public virtual String SS_Name{get;set;}
        
        public virtual String SS_Value{get;set;}
        
    }
}