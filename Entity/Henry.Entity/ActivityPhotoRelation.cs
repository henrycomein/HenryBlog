using System;
namespace Henry.Entity
{
    public class ActivityPhotoRelation:BaseModel
    {
        public ActivityPhotoRelation()
        {
           APR_Status = -1;
        }
        public virtual Int32 APR_ID{get;set;}
        
        public virtual Int32 APR_ActivityID{get;set;}
        
        public virtual Int32 APR_PhotoID{get;set;}
        
        public virtual Int32 APR_Sort{get;set;}
        
        public virtual Int32 APR_Status{get;set;}
        
        public virtual DateTime APR_CreateTime{get;set;}
        
    }
}