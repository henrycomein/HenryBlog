using System;
namespace Henry.Entity
{
    public class LoginLog:BaseModel
    {
        public LoginLog()
        {
          
        }
        public virtual Int32 L_ID{get;set;}
        
        //用户ID
        public virtual Int32 L_UserID{get;set;}
        
        //登录时间
        public virtual DateTime L_LoginTime{get;set;}
        
        //登录的IP地址
        public virtual String L_IpAddress{get;set;}
        
        //浏览器信息
        public virtual String L_Browser{get;set;}
        
    }
}