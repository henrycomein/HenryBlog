using System;
namespace Henry.Entity
{
    public class Photo:BaseModel
    {
        public Photo()
        {
           P_Status = -1;
        }
        public virtual Int32 P_ID{get;set;}
        
        //图片名称
        public virtual String P_FileName{get;set;}
        
        //图片备注
        public virtual String P_Desc{get;set;}
        
        //图片标签ID
        public virtual Int32 P_TagID{get;set;}
        //图片标签名称
        public virtual String P_TagName { get; set; }
        //图片类别id
        public virtual Int32 P_CategoryID{get;set;}

        public virtual Int32 P_Status{get;set;}
        
        public virtual DateTime P_CreateTime{get;set;}
        
    }
}