using System;
namespace Henry.Entity
{
    public class PhotoCategory:BaseModel
    {
        public PhotoCategory()
        {
           PC_Status = -1;
        }
        public virtual Int32 PC_ID{get;set;}
        
        //图片类别
        public virtual string PC_Name{get;set;}
        
        //封面图片id
        public virtual Int32 PC_CoverPhotoID{get;set;}
        public virtual String PC_CoverPhotoFileName { get; set; }
        public virtual Int32 PC_Sort{get;set;}
        public virtual String PC_Password { get; set; }
        public virtual Int32 PC_NeedPassword { get; set; }
        public virtual Int32 PC_Show { get; set; }
        public virtual String PC_Desc{get;set;}
        
        public virtual Int32 PC_Status{get;set;}
        
        public virtual DateTime PC_CreateTime{get;set;}
        
    }
}