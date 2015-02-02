using System;
namespace Henry.Entity
{
    public class BackgroundImage:BaseModel
    {
        public virtual Int32 BG_ID{get;set;}
        
        //背景图名称
        public virtual String BG_Name{get;set;}
        
        //背景图状态
        public virtual Int32 BG_Status{get;set;}
        
    }
}