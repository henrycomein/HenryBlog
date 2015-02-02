using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Henry.Entity;
namespace FrontSite.Models
{
    public class Chapter
    {
        public ArticleCategory CategoryInfo { get; set; }
        public List<Article> ArticleItems { get; set; }
    }
}