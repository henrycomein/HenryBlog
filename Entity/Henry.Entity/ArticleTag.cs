using System;
namespace Henry.Entity
{
    public class ArticleTag:BaseModel
    {
        public ArticleTag()
        {
           AT_Status = -1;
        }
        public virtual Int32 AT_ID{get;set;}
        
        //标签名称
        public virtual String AT_Name{get;set;}
        
        //排序
        public virtual Int32 AT_Sort{get;set;}
        
        //状态
        public virtual Int32 AT_Status{get;set;}
        
        public virtual DateTime AT_CreateTime{get;set;}
        
    }
}