using System;
namespace Henry.Entity
{
    public class UserRolesRelation:BaseModel
    {
        public UserRolesRelation()
        {
           UR_Status = -1;
        }
        public virtual Int32 UR_ID{get;set;}
        
        //用户ID
        public virtual Int32 UR_UserID{get;set;}
        
        //角色ID
        public virtual Int32 UR_RoleID{get;set;}
        
        public virtual Int32 UR_Status{get;set;}
        
        public virtual DateTime UR_CreateTime{get;set;}
        
    }
}