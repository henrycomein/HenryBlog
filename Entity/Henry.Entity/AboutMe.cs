using System;
namespace Henry.Entity
{
    public class AboutMe:BaseModel
    {
        public AboutMe()
        {
           
        }
        public virtual Int32 A_ID{get;set;}
        
        public virtual String A_Content{get;set;}
        
        public virtual Int32 A_Status{get;set;}
        
    }
}