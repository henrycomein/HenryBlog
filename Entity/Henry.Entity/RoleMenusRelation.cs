using System;
namespace Henry.Entity
{
    public class RoleMenusRelation:BaseModel
    {
        public RoleMenusRelation()
        {
           RM_Status = -1;
        }
        public virtual Int32 RM_ID{get;set;}
        
        //角色ID
        public virtual Int32 RM_RoleID{get;set;}
        
        //菜单ID
        public virtual Int32 RM_MenuID{get;set;}
        
        public virtual Int32 RM_Status{get;set;}
        
        public virtual DateTime RM_CreateTime{get;set;}
        
    }
}