using System;
namespace Henry.Entity
{
    public class Tag:BaseModel
    {
        public virtual Int32 T_ID{get;set;}
        
        //标签名称
        public virtual String T_Name{get;set;}

        //标签是否为图片专属
        public virtual Int32 T_IsPhoto { get; set; }
        //排序
        public virtual Int32 T_Sort{get;set;}
        
        //状态
        public virtual Int32 T_Status{get;set;}
        
        public virtual DateTime T_CreateTime{get;set;}
        
    }
}