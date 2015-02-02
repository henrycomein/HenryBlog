using System;
namespace Henry.Entity
{
    public class User:BaseModel
    {
        public User()
        {
           U_Status = -1;
        }
        public virtual Int32 U_ID{get;set;}
        
        //账号
        public virtual String U_Account{get;set;}
        
        //密码
        public virtual String U_Password{get;set;}
        
        public virtual Int32 U_Status{get;set;}
        
        public virtual DateTime U_CreateTime{get;set;}
        
    }
}