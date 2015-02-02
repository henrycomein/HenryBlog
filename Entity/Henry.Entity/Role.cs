using System;
namespace Henry.Entity
{
    public class Role:BaseModel
    {
        public Role()
        {
           R_Status = -1;
        }
        public virtual Int32 R_ID{get;set;}
        
        //角色名称
        public virtual String R_Name{get;set;}
        
        //角色状态
        public virtual Int32 R_Status{get;set;}
        
        public virtual DateTime R_CreateTime{get;set;}
        
    }
}