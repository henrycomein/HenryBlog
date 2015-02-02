using System;
namespace Henry.Entity
{
    public class Message:BaseModel
    {
        public Message()
        {
           M_Status = -1;
        }
        public virtual Int32 M_ID{get;set;}
        
        //消息类型（0,提醒;1,短消息）
        public virtual Int32 M_Type{get;set;}
        
        //是否已阅读（0未阅读，1已阅读）
        public virtual Int32 M_Read{get;set;}
        
        //状态
        public virtual Int32 M_Status{get;set;}
        
        public virtual DateTime M_CreateTime{get;set;}
        
    }
}