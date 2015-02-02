using System;
namespace Henry.Entity
{
    public class WebLogs:BaseModel
    {
        public WebLogs()
        {
         
        }
        public virtual Int32 id{get;set;}
        
        public virtual Int32 LogLevel{get;set;}
        
        public virtual String ErrorMsg{get;set;}
        
        public virtual String Logger{get;set;}
        
        public virtual DateTime CreateTime{get;set;}
        
    }
}