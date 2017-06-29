using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Com.Weehong.Elearning.Files.Attachment
{
    public class Attachment
    {
        /// <summary>
        /// 保存附件
        /// </summary>
        public void SaveFile(HttpPostedFile file, string fileName)
        {
            //重命名
            string[] oldFileNameList = file.FileName.Split('.');
            //
            //string fileName = oldFileNameList[0] + "_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ff") + "." + oldFileNameList[1];
            //创建文件夹
            string pathUrl = "C:\\/Files";
            if (!Directory.Exists(pathUrl))
            {
                Directory.CreateDirectory(pathUrl);
            }
            //保存至指定目录
            file.SaveAs(pathUrl + "\\/" + fileName);
        }
        public void SaveUserFile(HttpPostedFile file, string fileName,string locapath)
        {
            //重命名
            string[] oldFileNameList = file.FileName.Split('.');
            //
            //string fileName = oldFileNameList[0] + "_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ff") + "." + oldFileNameList[1];
            //创建文件夹
             //string pathUrl = "E:\\/Files";

            string pathUrl = locapath; 
            if (!Directory.Exists(pathUrl))
            {
                Directory.CreateDirectory(pathUrl);
            }
            //保存至指定目录
            file.SaveAs(pathUrl + "\\/" + fileName);
        }




       
    }
}
