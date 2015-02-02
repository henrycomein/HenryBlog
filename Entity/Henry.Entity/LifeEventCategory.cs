using System;
namespace Henry.Entity
{
    public class LifeEventCategory:BaseModel
    {
        public LifeEventCategory()
        {
           LEC_Sort = -1;
        }
        public virtual Int32 LEC_ID{get;set;}
        
        public virtual String LEC_Name{get;set;}
        
        public virtual Int32 LEC_Sort{get;set;}
        
        public virtual Int32 LEC_Status{get;set;}
        
    }
}