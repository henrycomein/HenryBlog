using System;
namespace Henry.Entity
{
    public class Article:BaseModel
    {
        public virtual Int32 A_ID{get;set;}
        
        //文章标题
        public virtual String A_Title{get;set;}
        
        //文章类别ID
        public virtual Int32 A_CategoryID{get;set;}
        //文章类别名称
        public virtual String A_CategoryName { get; set; }
        //文章内容
        public virtual String A_Content{get;set;}
        
        //是否置顶
        public virtual Int32 A_IsTop{get;set;}
        
        //排序
        public virtual Int32 A_Sort{get;set;}
        
        //状态
        public virtual Int32 A_Status{get;set;}
        
        public virtual DateTime A_CreateTime{get;set;}
        
    }
}