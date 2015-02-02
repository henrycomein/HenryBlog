using System;
namespace Henry.Entity
{
    public class UserDetail:User
    {
        public UserDetail()
        {
          
        }
        public virtual Int32 UD_ID{get;set;}
        
        public virtual Int32 UD_UserID{get;set;}
        
        //昵称
        public virtual String UD_NickName{get;set;}
        
        //头像
        public virtual String UD_Avatar{get;set;}
        
        //Email
        public virtual String UD_Email{get;set;}
        
    }
}