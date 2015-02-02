using System;
namespace Henry.Entity
{
    public class ArticleCategory:BaseModel
    {
        public ArticleCategory() {
            AC_Status = -1;
        }
        public virtual Int32 AC_ID{get;set;}
        
        public virtual String AC_Name{get;set;}
        
        public virtual String AC_Code{get;set;}
        
        public virtual Int32 AC_ParentID{get;set;}
        public virtual String AC_ParentFullName { get; set; }
        public virtual Int32 AC_ShowFront{get;set;}
        
        public virtual String AC_Description{get;set;}
        
        public virtual Int32 AC_Sort{get;set;}
        
        public virtual String AC_PicName{get;set;}
        
        public virtual Int32 AC_ShowList{get;set;}
        
        public virtual Int32 AC_IsComplete{get;set;}
        
        public virtual Int32 AC_Status{get;set;}
        
        public virtual DateTime AC_CreateTime{get;set;}
        public virtual Int32 LevelNum { get; set; }
        public virtual Int32 TotalReadTimes { get; set; }

        public virtual Int32 TotalArticles { get; set; }

        public virtual DateTime LastPostTime { get; set; }
        
        
    }
}