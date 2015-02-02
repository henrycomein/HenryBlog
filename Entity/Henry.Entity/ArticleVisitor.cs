using System;
namespace Henry.Entity
{
    public class ArticleVisitor:BaseModel
    {
        public virtual Int32 AV_ID{get;set;}
        
        //访问者IP地址
        public virtual String AV_IpAddress{get;set;}
        
        //状态
        public virtual Int32 AV_Status{get;set;}
        
        public virtual DateTime AV_CreateTime{get;set;}
        
    }
}