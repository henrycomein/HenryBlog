using Manage.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manage.Controllers
{
    public class ImageUploadController : BaseController
    {
        //
        // GET: /ImageUpload/

        public NewtonJsonResult Index()
        {
            var postData = Request.Files["file"];
            var resultObj = new AjaxHandleDataResult() { success=0, message="图片上传失败！"};
            if (postData != null)
            { 
                var fileExtend= postData.FileName.Substring(postData.FileName.LastIndexOf("."));
                var todayDirectory = DateTime.Now.ToString("yyyyMMdd");
                var fileFullDirectoryName = string.Empty;
                var visiteUrl = string.Empty;
                if (Request.QueryString["photo"] == "1")
                {
                    fileFullDirectoryName = string.Concat(Henry.Common.SettingHelper.UploadPhotoPath(), todayDirectory, "\\");
                    visiteUrl = Henry.Common.SettingHelper.PhotoVisitePath();
                }
                else {
                    fileFullDirectoryName = string.Concat(Henry.Common.SettingHelper.UploadImagePath(), todayDirectory, "\\");
                    visiteUrl=Henry.Common.SettingHelper.ImageVisitePath();
                }
               
                
                if(!System.IO.Directory.Exists(fileFullDirectoryName))
                {
                    System.IO.Directory.CreateDirectory(fileFullDirectoryName);
                }
                var filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtend;
                postData.SaveAs(fileFullDirectoryName+filename);
                resultObj.success = 1;
                resultObj.message = "图片上传成功！";
                resultObj.url = string.Concat(visiteUrl, todayDirectory, "/", filename);
                resultObj.addtionnalData = string.Concat(todayDirectory, "/", filename);
            }
            return new NewtonJsonResult() { Data = resultObj };
        }

    }
}
