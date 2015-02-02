using System;
namespace Henry.Entity
{
    public class Menus:BaseModel
    {
        public Menus(){
            M_Status = -1;
        }
        public virtual Int32 M_ID{get;set;}
        
        public virtual String M_Name{get;set;}
        
        public virtual String M_Url{get;set;}
        
        public virtual Int32 M_ParentID{get;set;}
        public virtual String M_ParentFullName { get; set; }
        public virtual Int32 M_OrderIndex{get;set;}
        
        public virtual Int32 M_Status{get;set;}
        
        public virtual DateTime M_CreateTime{get;set;}
        
    }
}